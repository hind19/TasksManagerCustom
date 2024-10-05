using AutoMapper;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using TasksManager.Core.Enums;
using TasksManager.Core.EventModels;
using TasksManager.Core.Events;
using TasksManager.Persistence;
using TasksManager.Services.Interfaces.DTOs;
using TasksManager.Services.Interfaces.RepositoryServices;
using TasksManager.TasksScheduleModule.Models;
using TasksManagerCustom.Shared;

namespace TasksManager.Modules.TaskScheduleModule.ViewModels
{
    internal class TaskScheduleViewModel :BindableBase
    {
        #region Fields
        private ObservableCollection<DataGridTaskModel> _curentTasksList;
        private DataGridTaskModel _selectedTask;
        private readonly ITasksQueryService _tasksQueryService;
        private readonly ITaskCommandService _taskCommandService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public TaskScheduleViewModel(
            IEventAggregator eventAggregator,
            ITasksQueryService tasksQueryService,
            ITaskCommandService taskCommandService)
        {
            eventAggregator.GetEvent<CategoryOrProjectChangedEvent>().Subscribe(OnCategotyProjectChanged);
            _tasksQueryService = tasksQueryService;
            _taskCommandService = taskCommandService;

            //modules are independent from main app, so they aren't included to main Assembly
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TaskDto, DataGridTaskModel>()
                .ForMember(x => x.StartDate, o =>
                {
                    o.PreCondition(x => x.StartDate is not null);
                    o.MapFrom(x => x.EndDate.HasValue && x.StartDate.Value.Date == x.EndDate.Value.Date
                              ?  x.StartDate!.Value.ToString(Constants.FullDateTimeFormat, CultureInfo.CurrentCulture)
                              : x.StartDate!.Value.ToString(Constants.ShortDateTimeFormat, CultureInfo.CurrentCulture));
                })
                .ForMember(x => x.EndDate, o =>
                {
                    o.PreCondition(x => x.EndDate is not null);
                    o.MapFrom(x => x.StartDate.HasValue && x.EndDate.Value.Date == x.StartDate.Value.Date
                              ? x.EndDate!.Value.ToString(Constants.FullDateTimeFormat, CultureInfo.CurrentCulture)
                              : x.EndDate!.Value.ToString(Constants.ShortDateTimeFormat, CultureInfo.CurrentCulture));
                })
                    .ReverseMap();
            }));
        }

        #endregion

        #region Properties
        public ObservableCollection<DataGridTaskModel> CurrentTasksList
        {
            get
            {
                return _curentTasksList;
            }
            set
            {
                SetProperty(ref _curentTasksList, value);
            }
        }
        public DataGridTaskModel SelectedTask
        {
            get
            {
                return _selectedTask;
            }
            set
            {
                SetProperty(ref _selectedTask, value);
            }
        }

        #endregion

        #region Methods
        public async Task CompleteOrResetTask(DataGridTaskModel model)
        {
            if (model is null) 
            {
                throw new ArgumentNullException(typeof(DataGridTaskModel).FullName, ErrorMessages.ModelIsNullMessage);
            }

            model.PercentageOfCompletion = model.PercentageOfCompletion !=100
                ? 100
                : 0;
            model.Status = model.PercentageOfCompletion == 100
                ? Shared.Enums.TaskStatusEnum.Completed
                : Shared.Enums.TaskStatusEnum.NotStarted;

            await _taskCommandService.UpdateTaskProgress(_mapper.Map<TaskDto>(model));

            SelectedTask = model;
            RaisePropertyChanged(nameof(SelectedTask));
            CurrentTasksList = new ObservableCollection<DataGridTaskModel>(_curentTasksList);
        }

        private async void OnCategotyProjectChanged(Tuple<HierarchicalCollectionModel, CategoryProjectEnum> tuple)
        {
            var ids = GetSubCategoroesIds(tuple.Item1);
            if (tuple.Item2 == CategoryProjectEnum.Category)
            {
                var tasks = await _tasksQueryService.GetTasksListForCategory(ids);
                CurrentTasksList = new ObservableCollection<DataGridTaskModel>(_mapper.Map<IReadOnlyCollection<DataGridTaskModel>>(tasks));
            }
            else if (tuple.Item2 == CategoryProjectEnum.Project)
            {
                var tasks = await _tasksQueryService.GetTasksListForProject(ids);
                CurrentTasksList = new ObservableCollection<DataGridTaskModel>(_mapper.Map<IReadOnlyCollection<DataGridTaskModel>>(tasks));
            }
        }

        private IEnumerable<int> GetSubCategoroesIds(HierarchicalCollectionModel model)
        {
            var result = new List<int>();
            FindChildrenIds(ref result, model);
            return result;
        }

        private void FindChildrenIds(ref List<int> list, HierarchicalCollectionModel model)
        {
           list.Add(model.Id);
            foreach (var item in model.Children)
            {
                FindChildrenIds(ref list, item);
            }
        }
        #endregion


    }
}
