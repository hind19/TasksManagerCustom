namespace TasksManager.Services.Interfaces.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }

        public string TaskName { get; set; }

        public int? ProjectId { get; set; }

        public int? CategoryId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        // not sure if I Will use it
        // public DateTime? ReminderTime { get; set; }

        public int? PriorityId { get; set; }

        public int Status { get; set; }

        public int PercentageOfCompletion { get; set; }
    }
}
