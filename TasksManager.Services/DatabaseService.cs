using TasksManager.Persistence;
using TasksManager.PersistenceContracts;
using TasksManager.PersistenceContracts.Repositories;

namespace TasksManager.Services
{
    public static class DatabaseService
    {
        private static readonly IDbInitializer _dbInitializer = new DbInitializer();

        public static void CreateDataBaseIfNotExists(IDatabasePathProvider pathProvider)
        {
            _dbInitializer.CheckOrCreateDatabase(pathProvider.GetDatabasePath());
        }
    }
}
