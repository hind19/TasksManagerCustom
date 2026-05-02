using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksManager.Persistence.DomainModels;
using TasksManager.Persistence.Queries;
using TasksManager.PersistenceContracts;
using TasksManager.PersistenceContracts.Dtos;
using TasksManager.PersistenceContracts.Repositories;

namespace TasksManager.Persistence.Repositories
{
    public class ProjectRepository : AbstractRepository, IProjectRepository
    {
        public ProjectRepository(IDatabasePathProvider pathProvider) : base(pathProvider) { }

        public async Task<IReadOnlyCollection<PersistenceProjectDto>> GetAllProjects()
        {
            var result = await GetItemsWithQuery<Project>(ProjectQueries.AllProjectsQuery);
            return result.Select(ToDto).ToList().AsReadOnly();
        }

        private static PersistenceProjectDto ToDto(Project p) =>
            new(p.Id, p.Name!, p.ColorRGB!, p.IsGroup, p.Comment!, p.ShowInNavigator,
                p.ParentId, p.Target!, p.IsCompleted, p.StartDate, p.EndDate);
    }
}
