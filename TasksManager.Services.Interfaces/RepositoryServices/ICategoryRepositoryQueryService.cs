using TasksManager.Services.Interfaces.DTOs;

namespace TasksManager.Services.Interfaces.RepositoryServices
{
    public interface ICategoryRepositoryQueryService
    {
        Task<IReadOnlyCollection<ShortCategoryDto>> GetAllCategories(bool shownInNavigatorOnly);
    }
}
