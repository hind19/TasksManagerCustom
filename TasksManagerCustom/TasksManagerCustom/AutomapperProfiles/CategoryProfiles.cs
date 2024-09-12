using AutoMapper;
using TasksManager.Application.Models;
using TasksManager.Services.DTOs;

namespace TasksManager.Application.AutomapperProfiles
{
    public class CategoryProfiles : Profile
    {
        public CategoryProfiles()
        {
            CreateMap<CategoryModel, AddUpdateCategoryDto>()
                .ReverseMap();
        }
    }
}
