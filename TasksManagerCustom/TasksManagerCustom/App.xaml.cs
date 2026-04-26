using System;
using System.Linq;
using System.Threading;
using System.Windows;
using AutoMapper;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using TasksManager.Application.Dialogs.CategoriesDialogs;
using TasksManager.Application.Views;
using TasksManager.LeftPanelModule;
using TasksManager.MenuBarModule;
using TasksManager.PersistenceContracts;
using TasksManager.Services;
using TasksManager.Services.Interfaces;
using TasksManager.Services.Interfaces.RepositoryServices;
using TasksManager.Services.RepositoryServices;
using TasksManager.TasksScheduleModule;

namespace TasksManager.Application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        private readonly IDatabasePathProvider _pathProvider = null!;

        public App()
        {
            SetLanguageDictionary();
            try
            {
                _pathProvider = new DatabasePathProvider();
                CheckDatabase();
            }
            catch (Exception ex)
            {
                var title = TryFindResource("configurationErrorTitle") as string ?? "Configuration Error";
                var message = TryFindResource("configurationErrorMessage") as string ?? "Failed to load application configuration. The application will now close.";
                MessageBox.Show($"{message}\n\n{ex.Message}", title, MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }

       
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IDatabasePathProvider>(_pathProvider);
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
                case "en-US":
                    dict.Source = new Uri("..\\Languages\\en-US.xaml", UriKind.Relative);
                    break;
                case "ru-RU":
                    dict.Source = new Uri("..\\Languages\\ru-RU.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("..\\Languages\\ru-RU.xaml", UriKind.Relative);
                    break;
            }
            this.Resources.MergedDictionaries.Add(dict);
        }

        private void CheckDatabase()
        {
            DatabaseService.CreateDataBaseIfNotExists(_pathProvider);
        }

        private void RegisterDialogs(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<AddUpdateCategoryDialog, AddUpdateCategoryDialogViewModel>();
        }

    }
}
