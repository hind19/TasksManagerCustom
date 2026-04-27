using System.Threading.Tasks;

namespace TasksManager.Services.Interfaces
{
    public interface IDatabaseService
    {
        Task CreateDataBaseIfNotExists();
    }
}
