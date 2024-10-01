using AutoMapper;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Linq;
using System.Threading;
using System.Windows;
using TasksManager.Application.Dialogs.CategoriesDialogs;
using TasksManager.LeftPanelModule;
using TasksManager.Modules.MenuBarModule;
using TasksManager.Modules.ModuleName;
using TasksManager.Services;
using TasksManager.Services.Interfaces;
using TasksManager.Services.Interfaces.RepositoryServices;
using TasksManager.Services.RepositoryServices;
using TasksManager.Views;

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
            CheckDatabase();
        }

       
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
            containerRegistry.RegisterScoped<ICategoryRepositoryCommandService, CategoryRepositoryCommandService>();
            containerRegistry.RegisterScoped<ICategoryRepositoryQueryService, CategoryRepositoryQueryService>();
            containerRegistry.RegisterScoped<ITasksQueryService, TasksQueryService>();
            containerRegistry.RegisterScoped<ITaskCommandService, TasksCommandService>();

            //// Register Automapper
            var profileType = typeof(Profile);
            var mapperConfiguration = new MapperConfiguration(cfg =>
                cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => profileType.IsAssignableFrom(p))));
            containerRegistry.RegisterInstance<IMapper>(new Mapper(mapperConfiguration));



            RegisterDialogs(containerRegistry);
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
                // TODO: Switch to en-US. I have the en-US cultural settings on my local machine but want to see the Russian version
                case "en-US":
                    dict.Source = new Uri("..\\Languages\\ru-RU.xaml", UriKind.Relative);
                    break;
                case "ru-RU":
                    dict.Source = new Uri("..\\Languages\\ru-RU.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("..\\Resources\\ru-RU.xaml", UriKind.Relative);
                    break;
            }
            this.Resources.MergedDictionaries.Add(dict);
        }

        private void CheckDatabase()
        {
            DatabaseService.CreateDataBaseIfNotExists();
        }

        private void RegisterDialogs(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<AddUpdateCategoryDialog, AddUpdateCategoryDialogViewModel>();
        }

    }
}
