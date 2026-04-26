using AutoMapper;
using TasksManager.PersistenceContracts;
using TasksManager.PersistenceContracts.Dtos;
using TasksManager.PersistenceContracts.Repositories;
using TasksManager.Services.Interfaces.DTOs;
using TasksManager.Services.Interfaces.RepositoryServices;

namespace TasksManager.Services.RepositoryServices
{
    public class CategoryRepositoryCommandService : ICategoryRepositoryCommandService
    {
        private readonly IMapper _mapper;
        private readonly IDatabasePathProvider _pathProvider;

        public CategoryRepositoryCommandService(IMapper mapper, IDatabasePathProvider pathProvider)
        {
            _mapper = mapper;
            _pathProvider = pathProvider;
        }

        public async Task<int> CreateCategory(AddUpdateCategoryDto addUpdateCategoryDto)
        {
            var repo = RepositoryFactory<ICategoryRepository>.ResolveRepository(_pathProvider);
            var model = _mapper.Map<PersistenceCategoryDto>(addUpdateCategoryDto);
            return await repo.CreateCategory(model);
        }
    }
}
