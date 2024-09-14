using AutoMapper;
using TasksManager.Application.Models;
using TasksManager.Services.DTOs;
using TasksManager.Services.Interfaces.DTOs;

namespace TasksManager.Application.AutomapperProfiles
{
    public class CategoryProfiles : Profile
    {
        public CategoryProfiles()
        {
            CreateMap<CategoryModel, AddUpdateCategoryDto>()
                .ReverseMap();

            CreateMap<ShortCategoryDto, NameValuePair<int>>()
                .ConstructUsing(s => new NameValuePair<int>(s.Name, s.Id));
        }
    }
}
