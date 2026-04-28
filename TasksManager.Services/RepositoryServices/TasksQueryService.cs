using TasksManager.PersistenceContracts;
using TasksManager.PersistenceContracts.Repositories;
using TasksManager.Services.Interfaces.DTOs;
using TasksManager.Services.Interfaces.RepositoryServices;

namespace TasksManager.Services.RepositoryServices
{
    public class TasksQueryService : ITasksQueryService
    {
        private readonly IDatabasePathProvider _pathProvider;

        public TasksQueryService(IDatabasePathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        public async Task<IReadOnlyCollection<TaskDto>> GetTasksListForCategory(IEnumerable<int> categoryIds)
        {
            var repo = RepositoryFactory<ITaskRepository>.ResolveRepository(_pathProvider);
            var data = await repo.GetTasksByCategoriesIds(categoryIds);
            return ToTaskDtos(data);
        }

        public async Task<IReadOnlyCollection<TaskDto>> GetTasksListForProject(IEnumerable<int> projectIds)
        {
            var repo = RepositoryFactory<ITaskRepository>.ResolveRepository(_pathProvider);
            var data = await repo.GetTasksByProjectsIds(projectIds);
            return ToTaskDtos(data);
        }

        private static IReadOnlyCollection<TaskDto> ToTaskDtos(IReadOnlyCollection<PersistenceContracts.Dtos.PersistenceTaskDto> data) =>
            data.Select(t => new TaskDto(
                    t.Id, t.TaskName, t.ProjectId, t.CategoryId,
                    t.StartDate, t.EndDate, t.PriorityId,
                    t.Status, t.PercentageOfCompletion))
                .ToList().AsReadOnly();
    }
}
