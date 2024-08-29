using System;
using TasksManager.Persistence;
using TasksManager.PersistenceContracts;
using TasksManager.Services.Interfaces;

namespace TasksManager.Services
{
    public static class DatabaseService
    {
        // Think how to avoid direct initialization. May be using separate DI container for this project?????
        private static readonly IDbInitializer _dbInitializer = new DbInitializer();

        public static void CreateDataBaseIfNotExists()
        {
            _dbInitializer.CheckOrCreateDatabase();
        }
    }
}
