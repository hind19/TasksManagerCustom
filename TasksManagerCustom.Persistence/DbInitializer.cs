using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using TasksManager.Persistence.Queries;
using TasksManager.PersistenceContracts.Repositories;

namespace TasksManager.Persistence
{
    public class DbInitializer : IDbInitializer
    {
        public async Task CheckOrCreateDatabase()
        {
            var currentDirectory = Environment.CurrentDirectory;
            var dbPathDirectory = Path.Combine(currentDirectory, Constants.DatabaseDirectory);
            var dbPath = Path.Combine(dbPathDirectory, Constants.DatabaseFilename);
            if (File.Exists(dbPath))
            {
                return;
            }

            if (!Directory.Exists(dbPathDirectory))
            {
                Directory.CreateDirectory(dbPathDirectory);
            }
            
            await Init(dbPath);
        }

        private async Task Init(string dbPath)
        {
            //if (_connection != null)
            //    return;

            try
            {
                var connection = new SQLiteAsyncConnection(dbPath);

                connection.Tracer = new Action<string>(q => Debug.WriteLine(q));
                connection.Trace = true;

                await CreateTables(connection);
            }
            catch (Exception ex)
            {
                // TODO: Add Logging to the project
                Debug.WriteLine(ex);
            }
        }

        private async Task CreateTables(SQLiteAsyncConnection connection)
        {
            var createTableStatements = new List<string>()
            {
                DatabaseConstants.CreatePrioritiesTableQuery,
                DatabaseConstants.CreateProjectsTableQuery,
                DatabaseConstants.CreateCategoriesTableQuery,
                DatabaseConstants.CreateTaskTableQuery
            };

            foreach (var statement in createTableStatements)
                await ExecuteQuery(connection,statement);
        }

        public async Task<bool> ExecuteQuery(SQLiteAsyncConnection connection, string query)
        {
            var op = await connection.ExecuteAsync(query);
            return op > 0;
        }
    }
}
