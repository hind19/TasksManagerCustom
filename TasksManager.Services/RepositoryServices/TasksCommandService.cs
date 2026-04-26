using AutoMapper;
using TasksManager.PersistenceContracts;
using TasksManager.PersistenceContracts.Dtos;
using TasksManager.PersistenceContracts.Repositories;
using TasksManager.Services.Interfaces.DTOs;
using TasksManager.Services.Interfaces.RepositoryServices;

namespace TasksManager.Services.RepositoryServices
{
    public class TasksCommandService : ITaskCommandService
    {
        private readonly IMapper _mapper;
        private readonly IDatabasePathProvider _pathProvider;

        public TasksCommandService(IMapper mapper, IDatabasePathProvider pathProvider)
        {
            _mapper = mapper;
            _pathProvider = pathProvider;
        }

        public async Task<int> UpdateTaskProgress(TaskDto model)
        {
            var repo = RepositoryFactory<ITaskRepository>.ResolveRepository(_pathProvider);
            var repoModel = _mapper.Map<PersistenceTaskDto>(model);
            var result = await repo.UpdateTask(repoModel);
            if (result != 1)
            {
                //TODO: Create custom Exception
                throw new InvalidOperationException("Database Error");
            }

            return result;
            
        }
    }
}
