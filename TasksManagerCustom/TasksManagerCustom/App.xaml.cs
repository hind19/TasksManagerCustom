using TasksManager.Modules.MenuBarModule;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Threading;
using System.Windows;
using TasksManager.Modules.ModuleName;
using TasksManager.Services;
using TasksManager.Services.Interfaces;
using TasksManager.Views;
using TasksManager.LeftPanelModule;

namespace TasksManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        public App()
        {
            SetLanguageDictionary();
        }
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<TaskScheduleModule>();
            moduleCatalog.AddModule<TopMenuBarModule>();
            moduleCatalog.AddModule<LeftPanelSpaceModule>();
        }

        private void SetLanguageDictionary()
        {
            ResourceDictionary dict = new ResourceDictionary();
            switch (Thread.CurrentThread.CurrentCulture.ToString())
            {
                // TODO: Switch to en-US. I have the en-US cultural settings but want to see russian version
                case "en-US":
                    dict.Source = new Uri("..\\Languages\\ru-RU.xaml", UriKind.Relative);
                    break;
                case "fr-CA":
                    dict.Source = new Uri("..\\Languages\\ru-RU.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("..\\Resources\\ru-RU.xaml", UriKind.Relative);
                    break;
            }
            this.Resources.MergedDictionaries.Add(dict);
        }
    }
}
