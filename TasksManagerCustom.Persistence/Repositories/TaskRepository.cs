using SQLite;
using TasksManager.Persistence.DomainModels;
using TasksManager.Persistence.Queries;
using TasksManager.PersistenceContracts;
using TasksManager.PersistenceContracts.Dtos;
using TasksManager.PersistenceContracts.Repositories;

namespace TasksManager.Persistence.Repositories
{
    public class TaskRepository : AbstractRepository, ITaskRepository
    {
        private enum CategoryProjectEnum { Category = 1, Project = 2 }

        public TaskRepository(IDatabasePathProvider pathProvider) : base(pathProvider) { }

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

            var result = await GetItemsWithQuery<TaskDomainModel>(connection, TasksQueries.AllTasksQuery + queryClause);
            await connection.CloseAsync();

            return result.Select(ToDto).ToList().AsReadOnly();
        }

        public async Task<int> UpdateTask(PersistenceTaskDto model)
        {
            var connection = new SQLiteAsyncConnection(GetDatabasePath());
            var result = await connection.UpdateAsync(ToEntity(model));
            await connection.CloseAsync();
            return result;
        }

        private static PersistenceTaskDto ToDto(TaskDomainModel t) =>
            new(t.Id, t.TaskName, t.ProjectId, t.CategoryId,
                t.StartDate, t.EndDate, t.PriorityId, t.Status, t.PercentageOfCompletion);

        private static TaskDomainModel ToEntity(PersistenceTaskDto dto) => new()
        {
            Id = dto.Id,
            TaskName = dto.TaskName,
            ProjectId = dto.ProjectId,
            CategoryId = dto.CategoryId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            PriorityId = dto.PriorityId,
            Status = dto.Status,
            PercentageOfCompletion = dto.PercentageOfCompletion
        };
    }
}
