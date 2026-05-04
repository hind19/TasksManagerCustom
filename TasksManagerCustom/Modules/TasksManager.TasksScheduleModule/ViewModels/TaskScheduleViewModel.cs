using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using TasksManager.Core;
using TasksManager.Core.Enums;
using TasksManager.Core.EventModels;
using TasksManager.Core.Events;
using TasksManager.Services.Interfaces.DTOs;
using TasksManager.Services.Interfaces.RepositoryServices;
using TasksManager.Core;
using TasksManager.Shared;
using TasksManager.Shared.GlobalConstants;
using TasksManager.Shared.Helpers;
using TasksManager.TasksScheduleModule.Models;

namespace TasksManager.TasksScheduleModule.ViewModels
{
    internal class TaskScheduleViewModel : BindableBase
    {
        #region Fields
        private ObservableCollection<DataGridTaskModel> _curentTasksList;
        private DataGridTaskModel _selectedTask;
        private IReadOnlyCollection<int>? _lastQueriedIds;
        private CategoryProjectEnum _lastQueriedType;
        private readonly ITasksQueryService _tasksQueryService;
        private readonly ITaskCommandService _taskCommandService;
        private readonly IDialogService _dialogService;
        #endregion

        #region Constructors
        public TaskScheduleViewModel(
            IEventAggregator eventAggregator,
            ITasksQueryService tasksQueryService,
            ITaskCommandService taskCommandService,
            IDialogService dialogService)
        {
            eventAggregator.GetEvent<CategoryOrProjectChangedEvent>().Subscribe(OnCategotyProjectChanged);
            eventAggregator.GetEvent<TaskSavedEvent>().Subscribe(args => _ = ReloadCurrentAsync());
            _tasksQueryService  = tasksQueryService;
            _taskCommandService = taskCommandService;
            _dialogService      = dialogService;
            EditTaskCommand     = new DelegateCommand<DataGridTaskModel>(EditTask);
        }
        #endregion

        #region Properties
        public DelegateCommand<DataGridTaskModel> EditTaskCommand { get; }

        public ObservableCollection<DataGridTaskModel> CurrentTasksList
        {
            get => _curentTasksList;
            set => SetProperty(ref _curentTasksList, value);
        }

        public DataGridTaskModel SelectedTask
        {
            get => _selectedTask;
            set => SetProperty(ref _selectedTask, value);
        }
        #endregion

        #region Methods
        private void EditTask(DataGridTaskModel model)
        {
            if (model is null) return;
            var parameters = new DialogParameters();
            parameters.Add(DialogParameterNames.TaskDto, ToTaskDto(model));
            _dialogService.ShowDialog(DialogNames.AddUpdateTask, parameters, _ => { });
        }

        private async Task ReloadCurrentAsync()
        {
            if (_lastQueriedIds is null) return;
            await LoadTasksAsync(_lastQueriedIds, _lastQueriedType);
        }

        public async Task CompleteOrResetTask(DataGridTaskModel model)
        {
            if (model is null)
                throw new ArgumentNullException(typeof(DataGridTaskModel).FullName, ErrorMessages.ModelIsNullMessage);

            model.PercentageOfCompletion = model.PercentageOfCompletion != TaskCompletionValues.Completed ? TaskCompletionValues.Completed : TaskCompletionValues.None;
            model.Status = model.PercentageOfCompletion == TaskCompletionValues.Completed
                ? Shared.Enums.TaskStatusEnum.Completed
                : Shared.Enums.TaskStatusEnum.NotStarted;

            await _taskCommandService.UpdateTaskProgress(ToTaskDto(model));

            SelectedTask = model;
            RaisePropertyChanged(nameof(SelectedTask));
            CurrentTasksList = new ObservableCollection<DataGridTaskModel>(_curentTasksList);
        }

        private async void OnCategotyProjectChanged(Tuple<HierarchicalCollectionModel, CategoryProjectEnum> tuple)
        {
            _lastQueriedIds  = GetSubCategoryIds(tuple.Item1).ToList().AsReadOnly();
            _lastQueriedType = tuple.Item2;
            await LoadTasksAsync(_lastQueriedIds, _lastQueriedType);
        }

        private async Task LoadTasksAsync(IEnumerable<int> ids, CategoryProjectEnum type)
        {
            IReadOnlyCollection<TaskDto> tasks;

            if (type == CategoryProjectEnum.Category)
                tasks = await _tasksQueryService.GetTasksListForCategory(ids);
            else if (type == CategoryProjectEnum.Project)
                tasks = await _tasksQueryService.GetTasksListForProject(ids);
            else
                return;

            CurrentTasksList = new ObservableCollection<DataGridTaskModel>(tasks.Select(ToDataGridModel));
        }

        private IEnumerable<int> GetSubCategoryIds(HierarchicalCollectionModel model)
        {
            var result = new List<int>();
            FindChildrenIds(ref result, model);
            return result;
        }

        private void FindChildrenIds(ref List<int> list, HierarchicalCollectionModel model)
        {
            list.Add(model.Id);
            foreach (var item in model.Children)
                FindChildrenIds(ref list, item);
        }

        private static DataGridTaskModel ToDataGridModel(TaskDto t) => new()
        {
            Id                     = t.Id,
            TaskName               = t.TaskName,
            CategoryId             = t.CategoryId,
            ProjectId              = t.ProjectId,
            PriorityId             = t.PriorityId,
            Status                 = (Shared.Enums.TaskStatusEnum)t.Status,
            PercentageOfCompletion = t.PercentageOfCompletion,
            StartDate              = FormatDate(t.StartDate, t.EndDate),
            EndDate                = FormatDate(t.EndDate,   t.StartDate),
            Comment                = t.Comment
        };

        private static TaskDto ToTaskDto(DataGridTaskModel m) =>
            new(m.Id, m.TaskName, m.ProjectId, m.CategoryId,
                startDate:  DateHelper.TryParseDate(m.StartDate),
                endDate:    DateHelper.TryParseDate(m.EndDate),
                priorityId: m.PriorityId,
                status:     (int)m.Status,
                m.PercentageOfCompletion,
                m.Comment);

        // if both dates fall on the same day show time, otherwise show date only
        private static string? FormatDate(DateTime? date, DateTime? otherDate) =>
            date is null ? null
            : otherDate.HasValue && date.Value.Date == otherDate.Value.Date
                ? date.Value.ToString(DateFormats.FullDateTime, CultureInfo.CurrentCulture)
                : date.Value.ToString(DateFormats.ShortDate,    CultureInfo.CurrentCulture);
        #endregion
    }
}
