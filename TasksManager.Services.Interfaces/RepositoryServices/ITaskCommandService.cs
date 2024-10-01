using TasksManager.Services.Interfaces.DTOs;

namespace TasksManager.Services.Interfaces.RepositoryServices
{
    public interface ITaskCommandService
    {

        Task<int> UpdateTaskProgress(TaskDto model);
    }
}
