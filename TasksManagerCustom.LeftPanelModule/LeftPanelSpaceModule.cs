using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using TasksManager.Core;
using TasksManager.LeftPanelModule.Views;

namespace TasksManager.LeftPanelModule
{
    public class LeftPanelSpaceModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public LeftPanelSpaceModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.LeftPanelSpace, "LeftPanelSpaceView");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LeftPanelSpaceView>();
        }
    }
}
