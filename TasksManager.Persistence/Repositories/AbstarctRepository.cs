using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TasksManager.Persistence.Repositories
{
    public abstract class AbstarctRepository
    {
        protected AbstarctRepository()
        {
            
        }

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
    }
}
