using AutoMapper;
using SQLite;
using TasksManager.Persistence.DomainModels.Abstract;

namespace TasksManager.Persistence.Repositories
{
    public abstract class AbstractRepository
    {
        protected IMapper _mapper;

        protected string GetDatabasePath()
        {
            var dbPathDirectory = Path.Combine(Environment.CurrentDirectory, Constants.DatabaseDirectory);
            return Path.Combine(dbPathDirectory, Constants.DatabaseFilename);
        }

        protected async Task<bool> ExecuteQuery(SQLiteAsyncConnection connection, string query)
        {
            var op = await connection.ExecuteAsync(query);
            return op > 0;
        }

        public async Task<IEnumerable<T>> GetItemsWithQuery<T>(SQLiteAsyncConnection connection, string query) where T : BaseTable, new()
        {
            return await connection.QueryAsync<T>(query);
        }
    }
}
