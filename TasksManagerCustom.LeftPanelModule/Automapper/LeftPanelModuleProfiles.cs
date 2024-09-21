using AutoMapper;
using TasksManager.Core.EventModels;
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
