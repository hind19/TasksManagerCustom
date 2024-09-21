using AutoMapper;
using SQLite;
using TasksManager.Persistence.DomainModels;
using TasksManager.Persistence.DomainModels.Commands;
using TasksManager.PersistenceContracts;
using TasksManager.PersistenceContracts.Dtos;
using TasksManagerCustom.Persistence.DomainModels.Queries;

namespace TasksManager.Persistence.Repositories
{
    public class CategoryRepository : AbstarctRepository, IRepository
    {
        public CategoryRepository()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<Category, PersistenceCategoryDto>().
                ReverseMap();
            }));
        }
        public async Task<int> CreateCategory(PersistenceCategoryDto model)
        {
            var connection = new SQLiteAsyncConnection(GetDatabasePath());
            var result = await connection.ExecuteAsync(
                CreateInstanceCommands.CreateCategoryCommand,
                model.Name,
                model.ColorRGB,
                model.IsGroup,
                model.Comment,
                model.ShowInNavigator,
                model.ParentId);
            await connection.CloseAsync();

            return result;
        }

        public async Task<IReadOnlyCollection<PersistenceCategoryDto>> GetAllCategories(bool showInNavigatorOnly)
        {
            var connection = new SQLiteAsyncConnection(GetDatabasePath());
            var query = showInNavigatorOnly 
                ? CategoryQueries.AllCategoriesQuery + CategoryQueries.ShowInNavigatorOnly
                : CategoryQueries.AllCategoriesQuery;

            var result = await GetItemsWithQuery<Category>(connection, query);
            await connection.CloseAsync();

            return _mapper.Map<IReadOnlyCollection<PersistenceCategoryDto>>(result.ToList().AsReadOnly());
                
        }
    }
}
