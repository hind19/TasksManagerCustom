using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using TasksManagerCustom.Core;
using TasksManagerCustom.Modules.ModuleName.Views;

namespace TasksManagerCustom.Modules.ModuleName
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
            // _regionManager.RequestNavigate(RegionNames.ContentRegion, "ViewA");
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "TaskScheduleView");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // containerRegistry.RegisterForNavigation<ViewA>();
            containerRegistry.RegisterForNavigation<TaskScheduleView>();
        }
    }
}