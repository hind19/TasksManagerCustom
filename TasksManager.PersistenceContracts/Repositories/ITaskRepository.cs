using TasksManager.PersistenceContracts.Dtos;

namespace TasksManager.PersistenceContracts.Repositories
{
    public interface ITaskRepository : IRepository
    {
        Task<IReadOnlyCollection<PersistenceTaskDto>> GetTasksByCategoriesIds(IEnumerable<int> categoriesIds);
        Task<IReadOnlyCollection<PersistenceTaskDto>> GetTasksByProjectsIds(IEnumerable<int> projectsIds);
        Task<int> UpdateTask(PersistenceTaskDto model);
    }
}
