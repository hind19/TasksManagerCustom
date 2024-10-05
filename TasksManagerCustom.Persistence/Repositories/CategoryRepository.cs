using AutoMapper;
using SQLite;
using TasksManager.Persistence.DomainModels;
using TasksManager.Persistence.Queries;
using TasksManager.PersistenceContracts.Dtos;
using TasksManager.PersistenceContracts.Repositories;

namespace TasksManager.Persistence.Repositories
{
    public class CategoryRepository : AbstarctRepository, ICategoryRepository
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
            var category = _mapper.Map<Category>(model);
            var result = await connection.InsertAsync(category);
            
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
