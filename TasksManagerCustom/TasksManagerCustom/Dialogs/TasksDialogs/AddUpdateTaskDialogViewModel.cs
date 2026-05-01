using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using TasksManager.Application.Models;
using TasksManager.Services.Interfaces.DTOs;
using TasksManager.Shared.Enums;

namespace TasksManager.Application.Dialogs.TasksDialogs
{
    public class AddUpdateTaskDialogViewModel : BindableBase, IDialogAware
    {
        #region Fields
        private TaskModel _currentTask = new();
        private IReadOnlyCollection<NameValuePair<int>> _prioritiesList = new List<NameValuePair<int>>().AsReadOnly();
        private IReadOnlyCollection<NameValuePair<TaskStatusEnum>> _statusesList = new List<NameValuePair<TaskStatusEnum>>().AsReadOnly();
        private NameValuePair<TaskStatusEnum>? _selectedStatus;
        #endregion

        #region Constructor
        public AddUpdateTaskDialogViewModel()
        {
            OpenProjectsCommand  = new DelegateCommand(OpenProjects);
            OpenCategoriesCommand = new DelegateCommand(OpenCategories);
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

            SaveCommand   = new DelegateCommand(Save);
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
        public DelegateCommand OpenProjectsCommand  { get; }
        public DelegateCommand OpenCategoriesCommand { get; }
        public DelegateCommand OpenDateCommand      { get; }
        public DelegateCommand OpenReminderCommand  { get; }
        public DelegateCommand OpenRepeatCommand    { get; }
        #endregion

        #region Commands — clear fields (Phase 2)
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

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Title = parameters.GetValue<string>("DialogTitle");

            InitializeStatusesList();

            // TODO: Phase 2 — load PrioritiesList from DB via ICategoryRepositoryQueryService / IPriorityQueryService
            PrioritiesList = new List<NameValuePair<int>>().AsReadOnly();

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
            CurrentTask   = new TaskModel();
            SelectedStatus = StatusesList.First();
        }

        private void LoadExistingTask(TaskModel task)
        {
            CurrentTask    = task;
            SelectedStatus = StatusesList.FirstOrDefault(s => s.Value == task.Status)
                             ?? StatusesList.First();
        }

        // Open popup commands — Phase 2
        private void OpenProjects()   { }
        private void OpenCategories() { }
        private void OpenDate()       { }
        private void OpenReminder()   { }
        private void OpenRepeat()     { }

        // Clear field commands — Phase 2
        private void ClearProject()   { }
        private void ClearCategory()  { }
        private void ClearDate()      { }
        private void ClearReminder()  { }
        private void ClearRepeat()    { }
        private void ClearPriority()  { }
        private void ClearStatus()    { }

        private void Save()
        {
            // TODO: Phase 2 — call task service to create/update before closing
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        private void Cancel()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }
        #endregion
    }
}
