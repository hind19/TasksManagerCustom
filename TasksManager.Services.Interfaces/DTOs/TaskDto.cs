namespace TasksManager.Services.Interfaces.DTOs
{
    public class TaskDto(
        int id,
        string taskName,
        int? projectId,
        int? categoryId,
        DateTime? startDate,
        DateTime? endDate,
        int? priorityId,
        int status,
        int percentageOfCompletion)
    {
        public int Id { get; } = id;
        public string TaskName { get; } = taskName;
        public int? ProjectId { get; } = projectId;
        public int? CategoryId { get; } = categoryId;
        public DateTime? StartDate { get; } = startDate;
        public DateTime? EndDate { get; } = endDate;
        public int? PriorityId { get; } = priorityId;
        public int Status { get; } = status;
        public int PercentageOfCompletion { get; } = percentageOfCompletion;
    }
}
