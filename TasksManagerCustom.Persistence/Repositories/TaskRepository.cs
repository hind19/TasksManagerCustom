using AutoMapper;
using SQLite;
using TasksManager.Persistence.DomainModels;
using TasksManager.Persistence.Queries;
using TasksManager.PersistenceContracts.Dtos;
using TasksManager.PersistenceContracts.Repositories;

namespace TasksManager.Persistence.Repositories
{
    public class TaskRepository : AbstarctRepository, ITaskRepository
    {
        private enum CategoryProjectEnum
        {
            Category = 1,
            Project = 2
        }

        public TaskRepository()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TaskDomainModel, PersistenceTaskDto>().
                ReverseMap();
            }));
        }

        public async Task<IReadOnlyCollection<PersistenceTaskDto>> GetTasksByCategoriesIds(IEnumerable<int> categoriesIds)
        {
            return await GetTasksByIds(categoriesIds, CategoryProjectEnum.Category);
        }

        public async Task<IReadOnlyCollection<PersistenceTaskDto>> GetTasksByProjectsIds(IEnumerable<int> projectsIds)
        {
            return await GetTasksByIds(projectsIds, CategoryProjectEnum.Project);
        }

        private async Task<IReadOnlyCollection<PersistenceTaskDto>> GetTasksByIds(IEnumerable<int> ids, CategoryProjectEnum parentType)
        {
            var connection = new SQLiteAsyncConnection(GetDatabasePath());
            var queryClause = parentType == CategoryProjectEnum.Category
                ? string.Format(TasksQueries.CategoryFilterClause, string.Join(',', ids))
                : string.Format(TasksQueries.ProjectFilterClause, string.Join(',', ids));

            var query = TasksQueries.AllTasksQuery + queryClause;

            var result = await GetItemsWithQuery<TaskDomainModel>(connection, query);
            await connection.CloseAsync();

            return _mapper.Map<IReadOnlyCollection<PersistenceTaskDto>>(result.ToList().AsReadOnly());
        }
    }
}
