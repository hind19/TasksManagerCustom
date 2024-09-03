using SQLite;
using System.Threading.Tasks;
using TasksManager.Persistence.DomainModels.Commands;
using TasksManager.PersistenceContracts;
using TasksManager.PersistenceContracts.Dtos;

namespace TasksManager.Persistence.Repositories
{
    public class CategoryCommandRepository : AbstarctRepository, IRepository
    {
        public async Task<int> CreateCategory(CategoryDto model)
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

            return result;
        }
    }
}
