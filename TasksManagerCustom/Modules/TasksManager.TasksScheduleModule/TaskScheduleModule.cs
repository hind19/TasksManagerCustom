using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using TasksManager.Core;
using TasksManager.TasksScheduleModule.Views;

namespace TasksManager.TasksScheduleModule
{
    public class TaskScheduleModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public TaskScheduleModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.TaskScheduleView);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TaskScheduleView>();
            containerRegistry.RegisterForNavigation<MeasuresView>();
        }
    }
}