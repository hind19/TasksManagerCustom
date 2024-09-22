﻿using AutoMapper;
using TasksManager.Persistence.Repositories;
using TasksManager.PersistenceContracts.Dtos;
using TasksManager.PersistenceContracts.Repositories;
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
            var repo = RepositiryFactory<ICategoryRepository>.ResolveRepository();
            var model = _mapper.Map<PersistenceCategoryDto>(addUpdateCategoryDto);
            await repo.CreateCategory(model);
        }
    }
}
