using System;
using TasksManager.Shared.Enums;

namespace TasksManager.TasksScheduleModule.Models
{
    internal class DataGridTaskModel
    {
        public int Id { get; set; }

        public string TaskName { get; set; }

        public DateTime? StartDate { get; set; }

        public  DateTime? EndDate { get; set; }

        public int PercentageOfCompletion { get; set; }

        public TaskStatusEnum Status { get; set; }

        public int? CategoryId { get; set; }

        public int? ProjectId { get; set; }
    }
}
