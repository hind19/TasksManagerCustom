using TasksManager.Services.Interfaces.DTOs;

namespace TasksManager.Services.Interfaces.RepositoryServices
{
    public interface ITasksQueryService
    {
        Task<IReadOnlyCollection<TaskDto>> GetTasksListForCategory(IEnumerable<int> categoryIds);

        Task<IReadOnlyCollection<TaskDto>> GetTasksListForProject(IEnumerable<int> projectIds);
    }
}
