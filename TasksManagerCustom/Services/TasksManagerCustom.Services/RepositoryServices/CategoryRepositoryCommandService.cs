using System.Threading.Tasks;
using TasksManager.Persistence.Repositories;
using TasksManager.PersistenceContracts.Dtos;
using TasksManager.Services.DTOs;
using TasksManager.Services.Interfaces.RepositoryServices;

namespace TasksManager.Services.RepositoryServices
{
    public class CategoryRepositoryCommandService : ICategoryRepositoryCommandService
    {
        public async Task CreateCategory(AddUpdateCategoryDto addUpdateCategoryDto)
        {
            var repo = (CategoryCommandRepository)RepositiryFactory.ResolveRepository(Repositories.CategoryRepository);
            var model = new CategoryDto
            {
                Name = addUpdateCategoryDto.Name,
                ColorRGB = addUpdateCategoryDto.ColorRGB,
                Comment = addUpdateCategoryDto.Comment,
                IsGroup = addUpdateCategoryDto.IsGroup,
                ParentId = addUpdateCategoryDto.ParentId,
                ShowInNavigator = addUpdateCategoryDto.ShowInNavigator,
            };
            await repo.CreateCategory(model);
        }
    }
}
