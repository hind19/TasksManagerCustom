using AutoMapper;
using SQLite;
using TasksManager.Persistence.DomainModels.Abstract;
using TasksManager.PersistenceContracts;

namespace TasksManager.Persistence.Repositories
{
    public abstract class AbstractRepository
    {
        protected IMapper _mapper = null!;
        private readonly IDatabasePathProvider _pathProvider;

        protected AbstractRepository(IDatabasePathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        protected string GetDatabasePath() => _pathProvider.GetDatabasePath();

        protected async Task<bool> ExecuteQuery(SQLiteAsyncConnection connection, string query)
        {
            var op = await connection.ExecuteAsync(query);
            return op > 0;
        }

        protected async Task<IEnumerable<T>> GetItemsWithQuery<T>(SQLiteAsyncConnection connection, string query) where T : BaseTable, new()
        {
            return await connection.QueryAsync<T>(query);
        }
    }
}
