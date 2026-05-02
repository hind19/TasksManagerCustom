using System.Collections.Generic;
using System.Threading.Tasks;
using TasksManager.Services.Interfaces.DTOs;

namespace TasksManager.Services.Interfaces.RepositoryServices
{
    public interface IProjectQueryService
    {
        Task<IReadOnlyCollection<ShortProjectDto>> GetAllProjects();
    }
}
