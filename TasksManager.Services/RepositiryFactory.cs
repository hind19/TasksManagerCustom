using TasksManager.Persistence.Repositories;
using TasksManager.PersistenceContracts;


namespace TasksManager.Services
{
    internal static class RepositiryFactory
    {
        private static Dictionary<Repositories, Type> _repositoryValues = new Dictionary<Repositories, Type>
        {
            { Repositories.CategoryRepository, typeof(CategoryRepository) }
        };

        public static IRepository ResolveRepository(Repositories key)
        {
            if(! _repositoryValues.ContainsKey(key))
            {
                throw new InvalidOperationException($"Repository {key.ToString()}  has not been implemented yet");
            }

            return (IRepository)Activator.CreateInstance(_repositoryValues[key]);
        }


    }
}
