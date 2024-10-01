using AutoMapper;
using TasksManager.PersistenceContracts.Repositories;
using TasksManager.Services.Interfaces.DTOs;
using TasksManager.Services.Interfaces.RepositoryServices;

namespace TasksManager.Services.RepositoryServices
{
    public class TasksQueryService : ITasksQueryService
    {
        private readonly IMapper _mapper;

        public TasksQueryService(IMapper mapper)
        {
            _mapper = mapper;
        }
      
        public async Task<IReadOnlyCollection<TaskDto>> GetTasksListForCategory(IEnumerable<int> categoryIds)
        {
            var repo = RepositoryFactory<ITaskRepository>.ResolveRepository();
            var data = await repo.GetTasksByCategoriesIds(categoryIds);
            return _mapper.Map<IReadOnlyCollection<TaskDto>>(data);
        }

        public  async Task<IReadOnlyCollection<TaskDto>> GetTasksListForProject(IEnumerable<int> projectIds)
        {
            var repo = RepositoryFactory<ITaskRepository>.ResolveRepository();
            var data = await repo.GetTasksByProjectsIds(projectIds);
            return _mapper.Map<IReadOnlyCollection<TaskDto>>(data);
        }
    }
}
