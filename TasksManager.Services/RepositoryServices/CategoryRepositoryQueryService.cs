using TasksManager.PersistenceContracts;
using TasksManager.PersistenceContracts.Repositories;
using TasksManager.Services.Interfaces.DTOs;
using TasksManager.Services.Interfaces.RepositoryServices;

namespace TasksManager.Services.RepositoryServices
{
    public class CategoryRepositoryQueryService : ICategoryRepositoryQueryService
    {
        private readonly IDatabasePathProvider _pathProvider;

        public CategoryRepositoryQueryService(IDatabasePathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        public async Task<IReadOnlyCollection<ShortCategoryDto>> GetAllCategories(bool shownInNavigatorOnly)
        {
            var repo = RepositoryFactory<ICategoryRepository>.ResolveRepository(_pathProvider);
            var data = await repo.GetAllCategories(shownInNavigatorOnly);
            return data.Select(c => new ShortCategoryDto(c.Id, c.Name, c.ParentId, c.IsGroup))
                       .ToList().AsReadOnly();
        }
    }
}
