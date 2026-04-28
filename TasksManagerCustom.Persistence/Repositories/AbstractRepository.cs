using SQLite;
using TasksManager.Persistence.DomainModels.Abstract;
using TasksManager.PersistenceContracts;

namespace TasksManager.Persistence.Repositories
{
    public abstract class AbstractRepository
    {
        protected AbstractRepository(IDatabasePathProvider pathProvider)
        {
            Connection = new SQLiteAsyncConnection(pathProvider.GetDatabasePath());
        }

        protected SQLiteAsyncConnection Connection { get; }

        protected async Task<bool> ExecuteQuery(string query)
        {
            var op = await Connection.ExecuteAsync(query);
            return op > 0;
        }

        protected async Task<IEnumerable<T>> GetItemsWithQuery<T>(string query) where T : BaseTable, new()
        {
            return await Connection.QueryAsync<T>(query);
        }
    }
}
