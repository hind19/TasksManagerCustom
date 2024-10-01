using TasksManager.Persistence.Repositories;
using TasksManager.PersistenceContracts.Repositories;


namespace TasksManager.Services
{
    internal static class RepositoryFactory<T> where T : IRepository
    {
        private static readonly Dictionary<Type, Type> _repositoryValues = new Dictionary<Type, Type>
        {
            { typeof(ICategoryRepository), typeof(CategoryRepository) },
            { typeof(ITaskRepository), typeof(TaskRepository) }
        };
        public static T ResolveRepository()
        {
            if (!_repositoryValues.ContainsKey(typeof(T)))
            {
                throw new InvalidOperationException($"Repository {typeof(T).FullName}  has not been implemented yet");
            }
            return (T)Activator.CreateInstance(_repositoryValues[typeof (T)])!;
        }

    }
}
