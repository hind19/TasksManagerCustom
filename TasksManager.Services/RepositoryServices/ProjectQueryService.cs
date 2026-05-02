using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksManager.PersistenceContracts;
using TasksManager.PersistenceContracts.Repositories;
using TasksManager.Services.Interfaces.DTOs;
using TasksManager.Services.Interfaces.RepositoryServices;

namespace TasksManager.Services.RepositoryServices
{
    public class ProjectQueryService : IProjectQueryService
    {
        private readonly IDatabasePathProvider _pathProvider;

        public ProjectQueryService(IDatabasePathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        public async Task<IReadOnlyCollection<ShortProjectDto>> GetAllProjects()
        {
            var repo = RepositoryFactory<IProjectRepository>.ResolveRepository(_pathProvider);
            var data = await repo.GetAllProjects();
            return data.Select(p => new ShortProjectDto(p.Id, p.Name))
                       .ToList().AsReadOnly();
        }
    }
}
