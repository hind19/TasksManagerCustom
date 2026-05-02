using System.Collections.Generic;
using System.Threading.Tasks;
using TasksManager.PersistenceContracts.Dtos;

namespace TasksManager.PersistenceContracts.Repositories
{
    public interface IProjectRepository : IRepository
    {
        Task<IReadOnlyCollection<PersistenceProjectDto>> GetAllProjects();
    }
}
