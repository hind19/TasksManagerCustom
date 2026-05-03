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
using TasksManager.Core.Events;
using TasksManager.Services.Interfaces.DTOs;
using TasksManager.Services.Interfaces.RepositoryServices;
using TasksManager.Shared.Enums;

namespace TasksManager.Application.Dialogs.TasksDialogs
{
    public class AddUpdateTaskDialogViewModel : BindableBase, IDialogAware
    {
        #region Fields
        private TaskModel _currentTask = new();
        private IReadOnlyCollection<NameValuePair<int>> _categoriesList = new List<NameValuePair<int>>().AsReadOnly();
        private IReadOnlyCollection<NameValuePair<int>> _projectsList   = new List<NameValuePair<int>>().AsReadOnly();
        private IReadOnlyCollection<NameValuePair<int>> _prioritiesList = new List<NameValuePair<int>>().AsReadOnly();
        private IReadOnlyCollection<NameValuePair<TaskStatusEnum>> _statusesList = new List<NameValuePair<TaskStatusEnum>>().AsReadOnly();
        private NameValuePair<int>? _selectedCategory;
        private NameValuePair<int>? _selectedProject;
        private NameValuePair<int>? _selectedPriority;
        private NameValuePair<TaskStatusEnum>? _selectedStatus;
        private readonly ICategoryRepositoryQueryService _categoryQueryService;
        private readonly IProjectQueryService _projectQueryService;
        private readonly ITaskCommandService _taskCommandService;
        private readonly IEventAggregator _eventAggregator;
        #endregion

        #region Constructor
        public AddUpdateTaskDialogViewModel(
            ICategoryRepositoryQueryService categoryQueryService,
            IProjectQueryService projectQueryService,
            ITaskCommandService taskCommandService,
            IEventAggregator eventAggregator)
        {
            _categoryQueryService = categoryQueryService;
            _projectQueryService  = projectQueryService;
            _taskCommandService   = taskCommandService;
            _eventAggregator      = eventAggregator;

            OpenDateCommand      = new DelegateCommand(OpenDate);
            OpenReminderCommand  = new DelegateCommand(OpenReminder);
            OpenRepeatCommand    = new DelegateCommand(OpenRepeat);

            ClearProjectCommand  = new DelegateCommand(ClearProject);
            ClearCategoryCommand = new DelegateCommand(ClearCategory);
            ClearDateCommand     = new DelegateCommand(ClearDate);
            ClearReminderCommand = new DelegateCommand(ClearReminder);
            ClearRepeatCommand   = new DelegateCommand(ClearRepeat);
            ClearPriorityCommand = new DelegateCommand(ClearPriority);
            ClearStatusCommand   = new DelegateCommand(ClearStatus);

            SaveCommand   = new DelegateCommand(() => _ = SaveAsync());
            CancelCommand = new DelegateCommand(Cancel);
        }
        #endregion

        #region Properties
        public string Title { get; private set; } = string.Empty;

        public TaskModel CurrentTask
        {
            get => _currentTask;
            set => SetProperty(ref _currentTask, value);
        }

        public IReadOnlyCollection<NameValuePair<int>> CategoriesList
        {
            get => _categoriesList;
            set => SetProperty(ref _categoriesList, value);
        }

        public IReadOnlyCollection<NameValuePair<int>> ProjectsList
        {
            get => _projectsList;
            set => SetProperty(ref _projectsList, value);
        }

        public IReadOnlyCollection<NameValuePair<int>> PrioritiesList
        {
            get => _prioritiesList;
            set => SetProperty(ref _prioritiesList, value);
        }

        public IReadOnlyCollection<NameValuePair<TaskStatusEnum>> StatusesList
        {
            get => _statusesList;
            set => SetProperty(ref _statusesList, value);
        }

        public NameValuePair<int>? SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                SetProperty(ref _selectedCategory, value);
                CurrentTask.Category = value;
            }
        }

        public NameValuePair<int>? SelectedProject
        {
            get => _selectedProject;
            set
            {
                SetProperty(ref _selectedProject, value);
                CurrentTask.Project = value;
            }
        }

        public NameValuePair<int>? SelectedPriority
        {
            get => _selectedPriority;
            set
            {
                SetProperty(ref _selectedPriority, value);
                CurrentTask.Priority = value;
            }
        }

        public NameValuePair<TaskStatusEnum>? SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                SetProperty(ref _selectedStatus, value);
                if (value is not null)
                    CurrentTask.Status = value.Value;
            }
        }

        // TODO: Map to a future TaskModel.Repeat field when recurrence is added to the domain model
        public string RepeatDisplayText { get; set; } = string.Empty;
        #endregion

        #region Commands — open popup windows (Phase 2)
        public DelegateCommand OpenDateCommand      { get; }
        public DelegateCommand OpenReminderCommand  { get; }
        public DelegateCommand OpenRepeatCommand    { get; }
        #endregion

        #region Commands — clear fields
        public DelegateCommand ClearProjectCommand  { get; }
        public DelegateCommand ClearCategoryCommand { get; }
        public DelegateCommand ClearDateCommand     { get; }
        public DelegateCommand ClearReminderCommand { get; }
        public DelegateCommand ClearRepeatCommand   { get; }
        public DelegateCommand ClearPriorityCommand { get; }
        public DelegateCommand ClearStatusCommand   { get; }
        #endregion

        #region Commands — dialog
        public DelegateCommand SaveCommand   { get; }
        public DelegateCommand CancelCommand { get; }
        #endregion

        #region IDialogAware
        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }

        public async void OnDialogOpened(IDialogParameters parameters)
        {
            Title = parameters.GetValue<string>("DialogTitle");

            InitializeStatusesList();

            // TODO: Phase 2 — load PrioritiesList from DB via IPriorityQueryService
            PrioritiesList = new List<NameValuePair<int>>().AsReadOnly();

            await Task.WhenAll(LoadCategoriesAsync(), LoadProjectsAsync());

            var existingTask = parameters.GetValue<TaskModel>("Task");
            if (existingTask is not null)
            {
                LoadExistingTask(existingTask);
                return;
            }

            var taskDto = parameters.GetValue<TaskDto>("TaskDto");
            if (taskDto is not null)
            {
                LoadExistingTask(TaskDtoToTaskModel(taskDto));
                return;
            }

            InitializeNewTask();
        }
        #endregion

        #region Private methods
        private async Task LoadCategoriesAsync()
        {
            try
            {
                var data = await _categoryQueryService.GetAllCategories(false);
                CategoriesList = data.Select(c => new NameValuePair<int>(c.Name, c.Id))
                                     .ToList().AsReadOnly();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async Task LoadProjectsAsync()
        {
            try
            {
                var data = await _projectQueryService.GetAllProjects();
                ProjectsList = data.Select(p => new NameValuePair<int>(p.Name, p.Id))
                                   .ToList().AsReadOnly();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private static TaskModel TaskDtoToTaskModel(TaskDto dto) => new()
        {
            Id                     = dto.Id,
            TaskName               = dto.TaskName,
            Category               = dto.CategoryId.HasValue ? new NameValuePair<int>("", dto.CategoryId.Value) : null,
            Project                = dto.ProjectId.HasValue  ? new NameValuePair<int>("", dto.ProjectId.Value)  : null,
            Priority               = dto.PriorityId.HasValue ? new NameValuePair<int>("", dto.PriorityId.Value) : null,
            StartDate              = dto.StartDate,
            EndDate                = dto.EndDate,
            Status                 = (TaskStatusEnum)dto.Status,
            PercentageOfCompletion = dto.PercentageOfCompletion,
            Comment                = dto.Comment,
        };

        private void InitializeStatusesList()
        {
            // TODO: Phase 2 — localize display names via IResourceDictionaryProvider or similar
            StatusesList = new List<NameValuePair<TaskStatusEnum>>
            {
                new("Not started",      TaskStatusEnum.NotStarted),
                new("In progress",      TaskStatusEnum.InProgress),
                new("Ready for review", TaskStatusEnum.ReadyForReview),
                new("Completed",        TaskStatusEnum.Completed),
                new("Paused",           TaskStatusEnum.Paused),
                new("Cancelled",        TaskStatusEnum.Cancelled),
            }.AsReadOnly();
        }

        private void InitializeNewTask()
        {
            CurrentTask      = new TaskModel();
            SelectedStatus   = StatusesList.First();
            SelectedCategory = null;
            SelectedProject  = null;
            SelectedPriority = null;
        }

        private void LoadExistingTask(TaskModel task)
        {
            CurrentTask      = task;
            SelectedStatus   = StatusesList.FirstOrDefault(s => s.Value == task.Status) ?? StatusesList.First();
            SelectedCategory = CategoriesList.FirstOrDefault(c => c.Value == task.Category?.Value);
            SelectedProject  = ProjectsList.FirstOrDefault(p => p.Value == task.Project?.Value);
            SelectedPriority = PrioritiesList.FirstOrDefault(p => p.Value == task.Priority?.Value);
        }

        // Open popup commands — Phase 2
        private void OpenDate()      { }
        private void OpenReminder()  { }
        private void OpenRepeat()    { }

        // Clear field commands
        private void ClearProject()  => SelectedProject  = null;
        private void ClearCategory() => SelectedCategory = null;
        private void ClearDate()     { }
        private void ClearReminder() { }
        private void ClearRepeat()   { }
        private void ClearPriority() => SelectedPriority = null;
        private void ClearStatus()   { }

        private async Task SaveAsync()
        {
            try
            {
                var dto = new TaskDto(
                    CurrentTask.Id,
                    CurrentTask.TaskName ?? string.Empty,
                    SelectedProject?.Value,
                    SelectedCategory?.Value,
                    CurrentTask.StartDate,
                    CurrentTask.EndDate,
                    SelectedPriority?.Value,
                    (int)CurrentTask.Status,
                    CurrentTask.PercentageOfCompletion,
                    CurrentTask.Comment);

                bool isNew = CurrentTask.Id == 0;
                if (isNew)
                    await _taskCommandService.CreateTask(dto);
                else
                    await _taskCommandService.UpdateTaskProgress(dto);

                _eventAggregator.GetEvent<TaskSavedEvent>().Publish(Tuple.Create(dto.CategoryId, isNew));
                var resultParams = new DialogParameters { { "TaskDto", dto } };
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK, resultParams));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void Cancel()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }
        #endregion
    }
}
