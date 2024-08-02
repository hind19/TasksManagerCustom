using MenuBarModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using TasksManagerCustom.Core;

namespace MenuBarModule
{
    public class TopMenuBarModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public TopMenuBarModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.TopMenuBar, "TopMenuBarView");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TopMenuBarView>();
        }
    }
}
