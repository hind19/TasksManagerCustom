using System.Threading.Tasks;
using TasksManager.Persistence;
using TasksManager.PersistenceContracts;
using TasksManager.PersistenceContracts.Repositories;
using TasksManager.Services.Interfaces;

namespace TasksManager.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IDbInitializer _dbInitializer = new DbInitializer();
        private readonly IDatabasePathProvider _pathProvider;

        public DatabaseService(IDatabasePathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        public async Task CreateDataBaseIfNotExists()
        {
            await _dbInitializer.CheckOrCreateDatabase(_pathProvider.GetDatabasePath());
        }
    }
}
