using AutoMapper;
using TasksManager.Persistence.Repositories;
using TasksManager.PersistenceContracts;
using TasksManager.PersistenceContracts.Repositories;
using TasksManager.Services.Interfaces.DTOs;
using TasksManager.Services.Interfaces.RepositoryServices;

namespace TasksManager.Services.RepositoryServices
{
    public class CategoryRepositoryQueryService : ICategoryRepositoryQueryService
    {
        private readonly IMapper _mapper;
        private readonly IDatabasePathProvider _pathProvider;

        public CategoryRepositoryQueryService(IMapper mapper, IDatabasePathProvider pathProvider)
        {
            _mapper = mapper;
            _pathProvider = pathProvider;
        }

        public async Task<IReadOnlyCollection<ShortCategoryDto>> GetAllCategories(bool shownInNavigatorOnly)
        {
            var repo = RepositoryFactory<ICategoryRepository>.ResolveRepository(_pathProvider);
            var data = await repo.GetAllCategories(shownInNavigatorOnly);
            return _mapper.Map<IReadOnlyCollection<ShortCategoryDto>>(data);
        }
    }
}
