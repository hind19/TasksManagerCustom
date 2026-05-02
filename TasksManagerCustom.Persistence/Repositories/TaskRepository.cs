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
            var idList = ids.ToList();
            var placeholders = string.Join(",", Enumerable.Repeat("?", idList.Count));
            var filterClause = parentType == CategoryProjectEnum.Category
                ? string.Format(TasksQueries.CategoryFilterClause, placeholders)
                : string.Format(TasksQueries.ProjectFilterClause, placeholders);

            var result = await Connection.QueryAsync<TaskDomainModel>(
                TasksQueries.AllTasksQuery + filterClause,
                idList.Cast<object>().ToArray());

            return result.Select(ToDto).ToList().AsReadOnly();
        }

        public async Task<int> CreateTask(PersistenceTaskDto dto)
        {
            return await Connection.ExecuteAsync(
                TasksQueries.InsertTaskQuery,
                dto.TaskName,
                dto.ProjectId,
                dto.CategoryId,
                dto.StartDate,
                dto.EndDate,
                dto.Status,
                dto.PriorityId,
                dto.PercentageOfCompletion);
        }

        public async Task<int> UpdateTask(PersistenceTaskDto model)
        {
            return await Connection.UpdateAsync(ToEntity(model));
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
