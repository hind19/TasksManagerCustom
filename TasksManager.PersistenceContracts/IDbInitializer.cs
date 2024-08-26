using System.Threading.Tasks;

namespace TasksManager.PersistenceContracts
{
    public interface IDbInitializer
    {
        Task CheckOrCreateDatabase();
    }
}
