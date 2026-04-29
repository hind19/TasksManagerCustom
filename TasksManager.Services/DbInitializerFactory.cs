using TasksManager.Persistence;
using TasksManager.PersistenceContracts.Repositories;

namespace TasksManager.Services
{
    internal static class DbInitializerFactory
    {
        public static IDbInitializer Create() => new DbInitializer();
    }
}
