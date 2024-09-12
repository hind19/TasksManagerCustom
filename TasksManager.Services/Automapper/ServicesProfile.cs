using AutoMapper;
using TasksManager.PersistenceContracts.Dtos;
using TasksManager.Services.DTOs;

namespace TasksManager.Services.Automapper
{
    public class ServicesProfile : Profile
    {
        public ServicesProfile()
        {
            AddCategoryMaps(); 
        }

        private void AddCategoryMaps()
        {
            CreateMap<AddUpdateCategoryDto, CategoryDto>()
                .ReverseMap();
        }
    }
}
