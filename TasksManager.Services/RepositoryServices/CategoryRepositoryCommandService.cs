using AutoMapper;
using TasksManager.Persistence.Repositories;
using TasksManager.PersistenceContracts.Dtos;
using TasksManager.Services.DTOs;
using TasksManager.Services.Interfaces.RepositoryServices;

namespace TasksManager.Services.RepositoryServices
{
    public class CategoryRepositoryCommandService : ICategoryRepositoryCommandService
    {
        private readonly IMapper _mapper;

        public CategoryRepositoryCommandService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task CreateCategory(AddUpdateCategoryDto addUpdateCategoryDto)
        {
            var repo = (CategoryCommandRepository)RepositiryFactory.ResolveRepository(Repositories.CategoryRepository);
            var model = _mapper.Map<CategoryDto>(addUpdateCategoryDto);
            await repo.CreateCategory(model);
        }
    }
}
