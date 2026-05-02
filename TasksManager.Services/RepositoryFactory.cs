using TasksManager.Persistence.Repositories;
using TasksManager.PersistenceContracts;
using TasksManager.PersistenceContracts.Repositories;

namespace TasksManager.Services
{
    internal static class RepositoryFactory<T> where T : IRepository
    {
        private static readonly Dictionary<Type, Type> _repositoryValues = new Dictionary<Type, Type>
        {
            { typeof(ICategoryRepository), typeof(CategoryRepository) },
            { typeof(ITaskRepository),     typeof(TaskRepository)     },
            { typeof(IProjectRepository),  typeof(ProjectRepository)  },
        };

        public static T ResolveRepository(IDatabasePathProvider pathProvider)
        {
            if (!_repositoryValues.ContainsKey(typeof(T)))
                throw new InvalidOperationException($"Repository {typeof(T).FullName} has not been implemented yet");
            return (T)Activator.CreateInstance(_repositoryValues[typeof(T)], pathProvider)!;
        }
    }
}
