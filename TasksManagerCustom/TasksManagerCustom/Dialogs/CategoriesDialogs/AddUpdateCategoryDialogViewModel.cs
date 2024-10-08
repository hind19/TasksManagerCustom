﻿using AutoMapper;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using TasksManager.Application.Models;
using TasksManager.Services.DTOs;
using TasksManager.Services.Interfaces.RepositoryServices;

namespace TasksManager.Application.Dialogs.CategoriesDialogs
{
    public class AddUpdateCategoryDialogViewModel : BindableBase, IDialogAware
    {
        #region Fiedls
        private readonly IMapper _mapper;
        private CategoryModel _categoryModel;
        private NameValuePair<int> _selectedParent;
        private IReadOnlyCollection<NameValuePair<int>> _categoriesList;
        private readonly ICategoryRepositoryCommandService _commandService;
        private readonly ICategoryRepositoryQueryService _queryService;
        #endregion

        #region Constructors
        public AddUpdateCategoryDialogViewModel(
            ICategoryRepositoryCommandService commandService,
            IMapper mapper,
            ICategoryRepositoryQueryService queryService)
        {
            _mapper = mapper;
            _commandService = commandService;
            CreateCategoryCommand = new DelegateCommand(CreateCategory);
            _queryService = queryService;
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

        public IReadOnlyCollection<NameValuePair<int>> CategoriesList
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

        public DelegateCommand CreateCategoryCommand{ get; private set; }

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

        public async void OnDialogOpened(IDialogParameters parameters)
        {

            Title = parameters.GetValue<string>("DialogTitle");

            // TODO: Add and implement Colors
            // TODO: Styles in .xaml!
            // TODO: implement update
            //if(! update)
            CurrentCategory = new CategoryModel();
            var data = await _queryService.GetAllCategories(true);
            CategoriesList =  _mapper.Map<IReadOnlyCollection<NameValuePair<int>>>(data);
        }

        public async void CreateCategory()
        {
            var dto = _mapper.Map<AddUpdateCategoryDto>(_categoryModel);
            dto.IsCreate = true;
            dto.ParentId = SelectedParent?.Value;

            
            await _commandService.CreateCategory(dto);
        }

        #endregion
    }
}
