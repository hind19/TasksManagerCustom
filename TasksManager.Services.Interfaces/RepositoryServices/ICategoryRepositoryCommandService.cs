using TasksManager.Services.Interfaces.DTOs;

namespace TasksManager.Services.Interfaces.RepositoryServices
{
    public interface ICategoryRepositoryCommandService
    {
        Task<int> CreateCategory(AddUpdateCategoryDto addUpdateCategoryDto);
    }
}
