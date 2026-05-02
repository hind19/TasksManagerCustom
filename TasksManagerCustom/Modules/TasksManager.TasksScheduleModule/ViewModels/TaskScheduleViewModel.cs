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
            var parameters = new DialogParameters();
            parameters.Add("TaskDto", ToTaskDto(model));
            _dialogService.ShowDialog(DialogNames.AddUpdateTask, parameters, result =>
            {
                if (result.Result != ButtonResult.OK) return;
                var dto = result.Parameters.GetValue<TaskDto>("TaskDto");
                if (dto.CategoryId.HasValue)
                    _ = ReloadByCategoryAsync(dto.CategoryId.Value);
            });
        }

        private async Task ReloadByCategoryAsync(int categoryId)
        {
            var tasks = await _tasksQueryService.GetTasksListForCategory(new[] { categoryId });
            CurrentTasksList = new ObservableCollection<DataGridTaskModel>(tasks.Select(ToDataGridModel));
        }

        public async Task CompleteOrResetTask(DataGridTaskModel model)
        {
            if (model is null)
                throw new ArgumentNullException(typeof(DataGridTaskModel).FullName, ErrorMessages.ModelIsNullMessage);

            model.PercentageOfCompletion = model.PercentageOfCompletion != 100 ? 100 : 0;
            model.Status = model.PercentageOfCompletion == 100
                ? Shared.Enums.TaskStatusEnum.Completed
                : Shared.Enums.TaskStatusEnum.NotStarted;

            await _taskCommandService.UpdateTaskProgress(ToTaskDto(model));

            SelectedTask = model;
            RaisePropertyChanged(nameof(SelectedTask));
            CurrentTasksList = new ObservableCollection<DataGridTaskModel>(_curentTasksList);
        }

        private async void OnCategotyProjectChanged(Tuple<HierarchicalCollectionModel, CategoryProjectEnum> tuple)
        {
            var ids = GetSubCategoryIds(tuple.Item1);
            IReadOnlyCollection<TaskDto> tasks;

            if (tuple.Item2 == CategoryProjectEnum.Category)
                tasks = await _tasksQueryService.GetTasksListForCategory(ids);
            else if (tuple.Item2 == CategoryProjectEnum.Project)
                tasks = await _tasksQueryService.GetTasksListForProject(ids);
            else
                return;

            CurrentTasksList = new ObservableCollection<DataGridTaskModel>(
                tasks.Select(ToDataGridModel));
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
            Id = t.Id,
            TaskName = t.TaskName,
            CategoryId = t.CategoryId,
            ProjectId = t.ProjectId,
            Status = (Shared.Enums.TaskStatusEnum)t.Status,
            PercentageOfCompletion = t.PercentageOfCompletion,
            StartDate = FormatDate(t.StartDate, t.EndDate),
            EndDate   = FormatDate(t.EndDate,   t.StartDate)
        };

        private static TaskDto ToTaskDto(DataGridTaskModel m) =>
            new(m.Id, m.TaskName, m.ProjectId, m.CategoryId,
                startDate:  DateHelper.TryParseDate(m.StartDate),
                endDate:    DateHelper.TryParseDate(m.EndDate),
                priorityId: null,
                status:     (int)m.Status,
                m.PercentageOfCompletion);

        // if both dates fall on the same day show time, otherwise show date only
        private static string? FormatDate(DateTime? date, DateTime? otherDate) =>
            date is null ? null
            : otherDate.HasValue && date.Value.Date == otherDate.Value.Date
                ? date.Value.ToString(DateFormats.FullDateTime, CultureInfo.CurrentCulture)
                : date.Value.ToString(DateFormats.ShortDate,    CultureInfo.CurrentCulture);
        #endregion
    }
}
