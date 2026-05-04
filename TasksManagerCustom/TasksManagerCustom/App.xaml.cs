using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using TasksManager.Application.Dialogs.CategoriesDialogs;
using TasksManager.Application.Dialogs.TasksDialogs;
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
        private static class LanguageResources
        {
            public const string EnUsCulture = "en-US";
            public const string RuRuCulture = "ru-RU";
            public const string EnUsPath = "..\\Languages\\en-US.xaml";
            public const string RuRuPath = "..\\Languages\\ru-RU.xaml";
        }

        private static class ConfigErrorRes
        {
            public const string TitleKey = "configurationErrorTitle";
            public const string MessageKey = "configurationErrorMessage";
            public const string TitleFallback = "Configuration Error";
            public const string MessageFallback = "Failed to load application configuration. The application will now close.";
        }

        private readonly IDatabasePathProvider _pathProvider = null!;

        public App()
        {
            SetLanguageDictionary();
            try
            {
                _pathProvider = new DatabasePathProvider();
                CheckDatabase().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                var title = TryFindResource(ConfigErrorRes.TitleKey) as string ?? ConfigErrorRes.TitleFallback;
                var message = TryFindResource(ConfigErrorRes.MessageKey) as string ?? ConfigErrorRes.MessageFallback;
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
            containerRegistry.RegisterSingleton<IDatabaseService, DatabaseService>();
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
            containerRegistry.RegisterScoped<ICategoryRepositoryCommandService, CategoryRepositoryCommandService>();
            containerRegistry.RegisterScoped<ICategoryRepositoryQueryService, CategoryRepositoryQueryService>();
            containerRegistry.RegisterScoped<ITasksQueryService, TasksQueryService>();
            containerRegistry.RegisterScoped<ITaskCommandService, TasksCommandService>();
            containerRegistry.RegisterScoped<IProjectQueryService, ProjectQueryService>();

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
                case LanguageResources.EnUsCulture:
                    dict.Source = new Uri(LanguageResources.EnUsPath, UriKind.Relative);
                    break;
                case LanguageResources.RuRuCulture:
                    dict.Source = new Uri(LanguageResources.RuRuPath, UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri(LanguageResources.RuRuPath, UriKind.Relative);
                    break;
            }
            this.Resources.MergedDictionaries.Add(dict);
        }

        private async Task CheckDatabase()
        {
            await new DatabaseService(_pathProvider).CreateDataBaseIfNotExists();
        }

        private void RegisterDialogs(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<AddUpdateCategoryDialog, AddUpdateCategoryDialogViewModel>();
            containerRegistry.RegisterDialog<AddUpdateTaskDialog, AddUpdateTaskDialogViewModel>();
        }

    }
}
