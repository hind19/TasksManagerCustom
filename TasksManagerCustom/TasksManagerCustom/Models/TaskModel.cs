using System;
using TasksManager.Shared.Enums;

namespace TasksManager.Application.Models
{
    public class TaskModel
    {
        public int Id { get; set; }

        public string TaskName { get; set; } = string.Empty;

        public NameValuePair<int>? Project { get; set; }

        public NameValuePair<int>? Category { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        // TODO: Decide whether ReminderTime is needed (field is commented out in TaskDomainModel)
        // public DateTime? ReminderTime { get; set; }

        public NameValuePair<int>? Priority { get; set; }

        public TaskStatusEnum Status { get; set; }

        public int PercentageOfCompletion { get; set; }

        public string? Comment { get; set; }
    }
}
