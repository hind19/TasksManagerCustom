using TasksManager.Persistence.DomainModels;
using TasksManager.Persistence.Queries;
using TasksManager.PersistenceContracts;
using TasksManager.PersistenceContracts.Dtos;
using TasksManager.PersistenceContracts.Repositories;

namespace TasksManager.Persistence.Repositories
{
    public class CategoryRepository : AbstractRepository, ICategoryRepository
    {
        public CategoryRepository(IDatabasePathProvider pathProvider) : base(pathProvider) { }

        public async Task<int> CreateCategory(PersistenceCategoryDto model)
        {
            return await Connection.InsertAsync(ToEntity(model));
        }

        public async Task<IReadOnlyCollection<PersistenceCategoryDto>> GetAllCategories(bool showInNavigatorOnly)
        {
            var query = showInNavigatorOnly
                ? CategoryQueries.AllCategoriesQuery + CategoryQueries.ShowInNavigatorOnly
                : CategoryQueries.AllCategoriesQuery;

            var result = await GetItemsWithQuery<Category>(query);
            return result.Select(ToDto).ToList().AsReadOnly();
        }

        private static PersistenceCategoryDto ToDto(Category c) =>
            new(c.Id, c.Name!, c.ColorRGB!, c.IsGroup, c.Comment!, c.ShowInNavigator, c.ParentId);

        private static Category ToEntity(PersistenceCategoryDto dto) => new()
        {
            Id = dto.Id,
            Name = dto.Name,
            ColorRGB = dto.ColorRGB,
            IsGroup = dto.IsGroup,
            Comment = dto.Comment,
            ShowInNavigator = dto.ShowInNavigator,
            ParentId = dto.ParentId
        };
    }
}
