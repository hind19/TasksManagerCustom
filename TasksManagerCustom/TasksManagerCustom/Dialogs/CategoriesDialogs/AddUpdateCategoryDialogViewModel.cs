using Prism.Mvvm;
using Prism.Regions.Behaviors;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TasksManager.Application.Dialogs.CategoriesDialogs
{
    public class AddUpdateCategoryDialogViewModel : BindableBase, IDialogAware
    {
        public string Title { get; private set;}

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            // debug mock
            return true;
        }

        public void OnDialogClosed()
        {
            // TODO: Implement
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

            Title = parameters.GetValue<string>("DialogTitle");
        }
    }
}
