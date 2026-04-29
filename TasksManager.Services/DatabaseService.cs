using System.Threading.Tasks;
using TasksManager.PersistenceContracts;
using TasksManager.PersistenceContracts.Repositories;
using TasksManager.Services.Interfaces;

namespace TasksManager.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IDbInitializer _dbInitializer;
        private readonly IDatabasePathProvider _pathProvider;

        public DatabaseService(IDatabasePathProvider pathProvider)
            : this(DbInitializerFactory.Create(), pathProvider) { }

        internal DatabaseService(IDbInitializer dbInitializer, IDatabasePathProvider pathProvider)
        {
            _dbInitializer = dbInitializer;
            _pathProvider = pathProvider;
        }

        public async Task CreateDataBaseIfNotExists()
        {
            await _dbInitializer.CheckOrCreateDatabase(_pathProvider.GetDatabasePath());
        }
    }
}
