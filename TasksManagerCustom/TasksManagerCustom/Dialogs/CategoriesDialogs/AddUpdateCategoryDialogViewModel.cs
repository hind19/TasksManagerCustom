using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TasksManager.Application.Models;
using TasksManager.Core;
using TasksManager.Core.Enums;
using TasksManager.Core.Events;
using TasksManager.Services.Interfaces.DTOs;
using TasksManager.Services.Interfaces.RepositoryServices;

namespace TasksManager.Application.Dialogs.CategoriesDialogs
{
    public class AddUpdateCategoryDialogViewModel : BindableBase, IDialogAware
    {
        #region Fields
        private CategoryModel _categoryModel;
        private NameValuePair<int> _selectedParent;
        private IReadOnlyCollection<NameValuePair<int>> _categoriesList;
        private readonly ICategoryRepositoryCommandService _commandService;
        private readonly ICategoryRepositoryQueryService _queryService;
        private readonly IEventAggregator _eventAggregator;
        #endregion

        #region Constructors
        public AddUpdateCategoryDialogViewModel(
            ICategoryRepositoryCommandService commandService,
            ICategoryRepositoryQueryService queryService,
            IEventAggregator eventAggregator)
        {
            _commandService = commandService;
            _queryService = queryService;
            _eventAggregator = eventAggregator;
            CreateCategoryCommand = new DelegateCommand(() => _ = CreateCategoryAsync());
        }
        #endregion

        #region Properties, Events and Commands
        public string Title { get; private set; }

        public CategoryModel CurrentCategory
        {
            get => _categoryModel;
            set => SetProperty(ref _categoryModel, value);
        }

        public NameValuePair<int> SelectedParent
        {
            get => _selectedParent;
            set => SetProperty(ref _selectedParent, value);
        }

        public IReadOnlyCollection<NameValuePair<int>> CategoriesList
        {
            get => _categoriesList;
            set => SetProperty(ref _categoriesList, value);
        }

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand CreateCategoryCommand { get; private set; }
        #endregion

        #region Methods
        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
            // TODO: Implement
        }

        public async void OnDialogOpened(IDialogParameters parameters)
        {
            Title = parameters.GetValue<string>(DialogParameterNames.DialogTitle);
            await LoadDialogDataAsync();
        }

        private async Task LoadDialogDataAsync()
        {
            try
            {
                // TODO: Add and implement Colors
                // TODO: Styles in .xaml!
                // TODO: implement update
                CurrentCategory = new CategoryModel();
                var data = await _queryService.GetAllCategories(true);
                CategoriesList = data.Select(c => new NameValuePair<int>(c.Name, c.Id))
                                     .ToList().AsReadOnly();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async Task CreateCategoryAsync()
        {
            try
            {
                var dto = new AddUpdateCategoryDto(
                    _categoryModel.Id,
                    _categoryModel.Name,
                    SelectedParent?.Value,
                    _categoryModel.IsGroup,
                    _categoryModel.ColorRGB,
                    _categoryModel.Comment,
                    _categoryModel.ShowInNavigator,
                    _categoryModel.ParentName,
                    isCreate: true);

                var result = await _commandService.CreateCategory(dto);
                if (result > 0)
                {
                    _eventAggregator.GetEvent<CategoryIsCreated>().Publish();
                    RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
                }
                // TODO: Send notification to main VM to update categories list
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        #endregion
    }
}
