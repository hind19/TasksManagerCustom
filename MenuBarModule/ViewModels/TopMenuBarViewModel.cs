using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksManager.Modules.MenuBarModule.ViewModels
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
        }
        #endregion

        #region Properties
        public DelegateCommand<string> CreateCategoryCommand { get; set; }
        #endregion

        #region Methods
        private void CreateCategory(string title)
        {
            
            var parameter = new DialogParameters();
            parameter.Add("DialogTitle", title);  
            _dialogService.ShowDialog(
                "AddUpdateCategoryDialog",
                parameter,
                (result) => 
                { 
                });
        }

        #endregion
    }
}
