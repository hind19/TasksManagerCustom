using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using TasksManager.Core;

namespace TasksManager.MenuBarModule.ViewModels
{
    public class TopMenuBarViewModel : BindableBase
    {
        #region fields
        private readonly IDialogService _dialogService;
        #endregion

        #region Constructors
        public TopMenuBarViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            CreateCategoryCommand = new DelegateCommand<string>(CreateCategory);
            CreateTaskCommand     = new DelegateCommand<string>(CreateTask);
        }
        #endregion

        #region Properties
        public DelegateCommand<string> CreateCategoryCommand { get; set; }
        public DelegateCommand<string> CreateTaskCommand     { get; set; }
        #endregion

        #region Methods
        private void CreateCategory(string title)
        {
            var parameter = new DialogParameters();
            parameter.Add(DialogParameterNames.DialogTitle, title);
            _dialogService.ShowDialog(
                DialogNames.AddUpdateCategory,
                parameter,
                (result) => { });
        }

        private void CreateTask(string title)
        {
            var parameter = new DialogParameters();
            parameter.Add(DialogParameterNames.DialogTitle, title);
            _dialogService.ShowDialog(
                DialogNames.AddUpdateTask,
                parameter,
                (result) => { });
        }
        #endregion
    }
}
