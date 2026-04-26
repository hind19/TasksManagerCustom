using AutoMapper;
using TasksManager.PersistenceContracts;
using TasksManager.PersistenceContracts.Repositories;
using TasksManager.Services.Interfaces.DTOs;
using TasksManager.Services.Interfaces.RepositoryServices;

namespace TasksManager.Services.RepositoryServices
{
    public class TasksQueryService : ITasksQueryService
    {
        private readonly IMapper _mapper;
        private readonly IDatabasePathProvider _pathProvider;

        public TasksQueryService(IMapper mapper, IDatabasePathProvider pathProvider)
        {
            _mapper = mapper;
            _pathProvider = pathProvider;
        }

        public async Task<IReadOnlyCollection<TaskDto>> GetTasksListForCategory(IEnumerable<int> categoryIds)
        {
            var repo = RepositoryFactory<ITaskRepository>.ResolveRepository(_pathProvider);
            var data = await repo.GetTasksByCategoriesIds(categoryIds);
            return _mapper.Map<IReadOnlyCollection<TaskDto>>(data);
        }

        public async Task<IReadOnlyCollection<TaskDto>> GetTasksListForProject(IEnumerable<int> projectIds)
        {
            var repo = RepositoryFactory<ITaskRepository>.ResolveRepository(_pathProvider);
            var data = await repo.GetTasksByProjectsIds(projectIds);
            return _mapper.Map<IReadOnlyCollection<TaskDto>>(data);
        }
    }
}
