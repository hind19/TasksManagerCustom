using TasksManager.PersistenceContracts;
using TasksManager.PersistenceContracts.Dtos;
using TasksManager.PersistenceContracts.Repositories;
using TasksManager.Services.Interfaces.DTOs;
using TasksManager.Services.Interfaces.RepositoryServices;

namespace TasksManager.Services.RepositoryServices
{
    public class CategoryRepositoryCommandService : ICategoryRepositoryCommandService
    {
        private readonly IDatabasePathProvider _pathProvider;

        public CategoryRepositoryCommandService(IDatabasePathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        public async Task<int> CreateCategory(AddUpdateCategoryDto dto)
        {
            var repo = RepositoryFactory<ICategoryRepository>.ResolveRepository(_pathProvider);
            var model = new PersistenceCategoryDto(
                dto.Id, dto.Name, dto.ColorRGB, dto.IsGroup,
                dto.Comment, dto.ShowInNavigator, dto.ParentId);
            return await repo.CreateCategory(model);
        }
    }
}
