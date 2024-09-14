using SQLite;
using TasksManagerCustom.Persistence.DomainModels.Abstract;

namespace TasksManager.Persistence.DomainModels
{
    // Name of this file has a different logic in order to avoid matching with Task class from TPL
    [Table (Constants.TasksTable)]
    internal class TaskDomainModel : BaseTable
    {
        [MaxLength (1000)]
        public string TaskName { get; set; }

        public int? ProjectId { get; set; }

        public int? CategoryId { get; set; }

        [MaxLength(25)]
        public DateTime? StartDate { get; set; }

        [MaxLength(25)]
        public DateTime? EndDate { get; set; }

        // not sure if I Will use it
       // public DateTime? ReminderTime { get; set; }

        public int? PriorityId { get; set; }

        public int Status { get; set; }

        public int PercentageOfCompletion { get; set; }
    }
}
