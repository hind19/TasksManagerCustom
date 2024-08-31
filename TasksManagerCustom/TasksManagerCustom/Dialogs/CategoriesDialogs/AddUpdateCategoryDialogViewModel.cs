using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using TasksManager.Application.Models;

namespace TasksManager.Application.Dialogs.CategoriesDialogs
{
    public class AddUpdateCategoryDialogViewModel : BindableBase, IDialogAware
    {
        #region Fiedls
        
        private CategoryModel _categoryModel;
        private NameValuePair<int> _selectedParent;
        private IEnumerable<NameValuePair<int>> _categoriesList;
        #endregion

        #region Constructors
        public AddUpdateCategoryDialogViewModel()
        {
            
        }

        #endregion

        #region Properties, Events and Commands
        public string Title { get; private set;}

        public CategoryModel CurrentCategory
        {
            get
            {
                return _categoryModel;
            }
            set
            {
                SetProperty(ref _categoryModel, value);
            }
        }

        public NameValuePair<int> SelectedParent
        {
            get
            {
                return _selectedParent;
            }
            set
            {
                SetProperty(ref _selectedParent, value);
            }
        }

        public IEnumerable<NameValuePair<int>> CategoriesList
        {
            get
            {
                return _categoriesList;
            }
            set
            {
                SetProperty(ref _categoriesList, value);
            }
        }

        public event Action<IDialogResult> RequestClose;

        #endregion

        #region Methods

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

            // TODO: Add and implement Colors
            // TODO: Styles in .xaml!
            // TODO: implement update
            //if(! update)
            CurrentCategory = new CategoryModel();

            //debug code (tenporary)
            CategoriesList = new List<NameValuePair<int>>()
            {
                new NameValuePair<int>("Option 1", 999),
                new NameValuePair<int>("Option 2", 888)
            };
        }

        #endregion
    }
}
