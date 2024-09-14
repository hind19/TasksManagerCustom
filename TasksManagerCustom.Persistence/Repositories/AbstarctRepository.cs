using AutoMapper;
using SQLite;
using TasksManagerCustom.Persistence.DomainModels.Abstract;

namespace TasksManager.Persistence.Repositories
{
    public abstract class AbstarctRepository
    {
        protected IMapper _mapper;

        protected string GetDatabasePath()
        {
            var currentDirectory = Environment.CurrentDirectory;
            var dbPathDirectory = Path.Combine(currentDirectory, Constants.DatabaseDirectory);
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
