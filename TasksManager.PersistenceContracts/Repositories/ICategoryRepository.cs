using TasksManager.PersistenceContracts.Dtos;

namespace TasksManager.PersistenceContracts.Repositories
{
    public interface ICategoryRepository : IRepository
    {
        Task<int> CreateCategory(PersistenceCategoryDto model);
        Task<IReadOnlyCollection<PersistenceCategoryDto>> GetAllCategories(bool showInNavigatorOnly);
    }
}
