using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using TasksManager.Core;
using TasksManager.Core.Enums;
using TasksManager.Core.EventModels;
using TasksManager.Core.Events;
using TasksManager.Services.Interfaces.DTOs;
using TasksManager.Services.Interfaces.RepositoryServices;

namespace TasksManager.LeftPanelModule.ViewModels
{
    internal class LeftPanelSpaceViewModel : BindableBase
    {
        #region fields
        private readonly ICategoryRepositoryQueryService _queryService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        private IReadOnlyCollection<HierarchicalCollectionModel> _categoriesList;
        private HierarchicalCollectionModel _selectedCategory;
        #endregion

        #region Constructors
        public LeftPanelSpaceViewModel(
            ICategoryRepositoryQueryService queryService,
            IEventAggregator eventAggregator,
            IRegionManager regionManager)
        {
            _queryService = queryService;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _categoriesList = new List<HierarchicalCollectionModel>();
            _selectedCategory = new HierarchicalCollectionModel();

            LoadCategoriesCommand = new DelegateCommand(LoadCategories);
            MeasuresCommand = new DelegateCommand(NavigateToMeasure);
            _eventAggregator.GetEvent<CategoryIsCreated>().Subscribe(CategoryCreated);
            _eventAggregator.GetEvent<TaskSavedEvent>().Subscribe(OnTaskSaved);
        }
        #endregion

        #region Properties
        public DelegateCommand LoadCategoriesCommand { get; set; }
        public DelegateCommand MeasuresCommand { get; set; }

        public IReadOnlyCollection<HierarchicalCollectionModel> CategoriesList
        {
            get => _categoriesList;
            set => SetProperty(ref _categoriesList, value);
        }

        public HierarchicalCollectionModel SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if (_selectedCategory is not null)
                    _selectedCategory.IsSelected = false;
                SetProperty(ref _selectedCategory, value);
                if (_selectedCategory is null) return;
                _selectedCategory.IsSelected = true;
                SendCategoryChangedEvent();
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
            var roots = flatList.Where(x => x.ParentId is null).ToList();
            var hierarchicalList = roots.Select(ToHierarchical).ToList();

            var remaining = flatList.Where(x => x.ParentId is not null).ToList();
            int prevCount;
            do
            {
                prevCount = remaining.Count;
                var unresolved = new List<ShortCategoryDto>();
                foreach (var item in remaining)
                {
                    var parent = FindParent(hierarchicalList, item.ParentId.GetValueOrDefault());
                    if (parent is not null)
                        parent.Children.Add(ToHierarchical(item));
                    else
                        unresolved.Add(item);
                }
                remaining = unresolved;
            } while (remaining.Count > 0 && remaining.Count < prevCount);

            CategoriesList = hierarchicalList;
            SelectedCategory = CategoriesList?.FirstOrDefault();
        }

        private void NavigateToMeasure()
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.MeasuresView);
        }

        private HierarchicalCollectionModel FindParent(List<HierarchicalCollectionModel> parents, int parentId)
        {
            if (!parents.Any())
                return null;

            var direct = parents.FirstOrDefault(x => x.Id == parentId);
            if (direct is not null) return direct;

            foreach (var item in parents)
            {
                var result = FindParent(item.Children, parentId);
                if (result is not null) return result;
            }
            return null;
        }

        private void SendCategoryChangedEvent()
        {
            _eventAggregator.GetEvent<CategoryOrProjectChangedEvent>()
                .Publish(new Tuple<HierarchicalCollectionModel, CategoryProjectEnum>(SelectedCategory, CategoryProjectEnum.Category));
        }

        private void CategoryCreated()
        {
            LoadCategories();
        }

        private void OnTaskSaved(Tuple<int?, bool> args)
        {
            if (!args.Item2 || args.Item1 is null) return;
            var match = FindParent(CategoriesList.ToList(), args.Item1.Value);
            if (match is not null)
                SelectedCategory = match;
        }

        private static HierarchicalCollectionModel ToHierarchical(ShortCategoryDto dto) => new()
        {
            Id = dto.Id,
            Name = dto.Name,
            IsGroup = dto.IsGroup
        };
        #endregion
    }
}
