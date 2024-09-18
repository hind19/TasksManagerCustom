using AutoMapper;
using Prism.Commands;
using Prism.Mvvm;
using TasksManager.LeftPanelModule.Models;
using TasksManager.Services.Interfaces.DTOs;
using TasksManager.Services.Interfaces.RepositoryServices;

namespace TasksManager.LeftPanelModule.ViewModels
{
    internal class LeftPanelSpaceViewModel : BindableBase
    {
        #region fields
        private readonly ICategoryRepositoryQueryService _queryService;
        private readonly IMapper _mapper;

        private IReadOnlyCollection<HierarchicalCollectionModel> _categoriesList;
        private HierarchicalCollectionModel _selectedCategory;
        #endregion

        #region Constructors
        public LeftPanelSpaceViewModel(ICategoryRepositoryQueryService queryService)
        {
            _queryService = queryService;
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ShortCategoryDto, HierarchicalCollectionModel>().
                ReverseMap();
            }));
            LoadCategoriesCommand = new DelegateCommand (LoadCategories);
        }
        #endregion

        #region Properties
        public DelegateCommand LoadCategoriesCommand { get; set; }

        public IReadOnlyCollection<HierarchicalCollectionModel> CategoriesList 
        { get 
            {
                return _categoriesList;
            }
            set 
            {
                SetProperty(ref _categoriesList, value);
            } 
        }

        private int _selectedcategoryId;
        public int SelectedCategoryId
        {
            get { return _selectedcategoryId; }
            set { SetProperty(ref _selectedcategoryId, value); }
        }

        public HierarchicalCollectionModel SelectedCategory
        {
            get { return _selectedCategory; }
            set 
            { 
                SetProperty(ref _selectedCategory, value); 
                _selectedCategory.IsSelected = true;
            }
        }
        #endregion

        #region Methods
        private async void LoadCategories()
        {
            var flatList = await _queryService.GetAllCategories(true);
            ConvertToHierarchicalList(flatList);
        }

        private void ConvertToHierarchicalList(IReadOnlyCollection<ShortCategoryDto> flatList)
        {
            // TODO: Fill Hierarchical structure

            var roots = flatList.Where(x => x.ParentId is null).ToList();
            var children = flatList.Where(x=> x.ParentId is not null).ToList();
            var hierarchicalList = _mapper.Map<List<HierarchicalCollectionModel>>(roots);

            foreach (var item in children)
            {
                var parent = FindParent(hierarchicalList, item.ParentId.GetValueOrDefault());
                if (parent is not null)
                {
                    parent.Children.Add(_mapper.Map<HierarchicalCollectionModel>(item));
                }
            }

            CategoriesList = hierarchicalList;
           // SelectedCategory = CategoriesList.FirstOrDefault();
        }

       private HierarchicalCollectionModel FindParent( List<HierarchicalCollectionModel> parents, int parentId)
       {
            if (!parents.Any())
                return null;

            if(parents.Any(x => x.Id == parentId))
                return parents.First(x => x.Id == parentId);

            foreach (var item in parents)
            {
                return FindParent(item.Children, parentId);
            }
            return null;
       }

        #endregion
    }
}
