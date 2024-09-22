using System.Threading.Tasks;

namespace TasksManager.PersistenceContracts.Repositories
{
    public interface IDbInitializer
    {
        Task CheckOrCreateDatabase();
    }
}
