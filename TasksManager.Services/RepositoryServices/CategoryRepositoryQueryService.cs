using AutoMapper;
using TasksManager.Persistence.Repositories;
using TasksManager.Services.Interfaces.DTOs;
using TasksManager.Services.Interfaces.RepositoryServices;

namespace TasksManager.Services.RepositoryServices
{
    public class CategoryRepositoryQueryService : ICategoryRepositoryQueryService
    {
        private readonly IMapper _mapper;

        public CategoryRepositoryQueryService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<IReadOnlyCollection<ShortCategoryDto>> GetAllCategories(bool shownInNavigatorOnly)
        {
            var repo = (CategoryRepository)RepositiryFactory.ResolveRepository(Repositories.CategoryRepository);
            var data =  await repo.GetAllCategories(shownInNavigatorOnly);
            return _mapper.Map<IReadOnlyCollection<ShortCategoryDto>>(data);
        }
    }
}
