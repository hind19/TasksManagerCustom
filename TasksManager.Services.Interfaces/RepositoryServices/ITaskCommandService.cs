using TasksManager.Services.Interfaces.DTOs;

namespace TasksManager.Services.Interfaces.RepositoryServices
{
    public interface ITaskCommandService
    {
        Task<int> CreateTask(TaskDto model);
        Task<int> UpdateTaskProgress(TaskDto model);
    }
}
