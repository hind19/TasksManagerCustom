using AutoMapper;
using TasksManager.LeftPanelModule.Models;
using TasksManager.Services.Interfaces.DTOs;

namespace TasksManager.LeftPanelModule.Automapper
{
    public class LeftPanelModuleProfiles : Profile 
    {
        public LeftPanelModuleProfiles()
        {
            CreateMap<ShortCategoryDto, HierarchicalCollectionModel>();
        }


    }
}
