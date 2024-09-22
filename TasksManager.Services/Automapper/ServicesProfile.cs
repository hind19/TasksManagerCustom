using AutoMapper;
using TasksManager.PersistenceContracts.Dtos;
using TasksManager.Services.DTOs;
using TasksManager.Services.Interfaces.DTOs;

namespace TasksManager.Services.Automapper
{
    public class ServicesProfile : Profile
    {
        public ServicesProfile()
        {
            AddCategoryMaps();
            AddTasksMaps();
        }

        private void AddCategoryMaps()
        {
            CreateMap<AddUpdateCategoryDto, CategoryDto>()
                .ReverseMap();

            CreateMap<PersistenceCategoryDto, ShortCategoryDto>()
                .ReverseMap();

            CreateMap<AddUpdateCategoryDto,PersistenceCategoryDto>();
        }

        private void AddTasksMaps()
        {
            CreateMap<PersistenceTaskDto, TaskDto>()
                .ReverseMap();
        }
    }
}
