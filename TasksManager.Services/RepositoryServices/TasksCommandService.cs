using TasksManager.PersistenceContracts;
using TasksManager.PersistenceContracts.Dtos;
using TasksManager.PersistenceContracts.Repositories;
using TasksManager.Services.Interfaces.DTOs;
using TasksManager.Services.Interfaces.RepositoryServices;

namespace TasksManager.Services.RepositoryServices
{
    public class TasksCommandService : ITaskCommandService
    {
        private readonly IDatabasePathProvider _pathProvider;

        public TasksCommandService(IDatabasePathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        public async Task<int> CreateTask(TaskDto model)
        {
            var repo = RepositoryFactory<ITaskRepository>.ResolveRepository(_pathProvider);
            var repoModel = new PersistenceTaskDto(
                0, model.TaskName, model.ProjectId, model.CategoryId,
                model.StartDate, model.EndDate, model.PriorityId,
                model.Status, model.PercentageOfCompletion);
            return await repo.CreateTask(repoModel);
        }

        public async Task<int> UpdateTaskProgress(TaskDto model)
        {
            var repo = RepositoryFactory<ITaskRepository>.ResolveRepository(_pathProvider);
            var repoModel = new PersistenceTaskDto(
                model.Id, model.TaskName, model.ProjectId, model.CategoryId,
                model.StartDate, model.EndDate, model.PriorityId,
                model.Status, model.PercentageOfCompletion);
            var result = await repo.UpdateTask(repoModel);
            if (result != 1)
                throw new InvalidOperationException("Database Error");
            return result;
        }
    }
}
