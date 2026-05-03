using System;

namespace TasksManager.PersistenceContracts.Dtos
{
    public class PersistenceProjectDto(
        int id,
        string name,
        string? colorRGB,
        bool isGroup,
        string? comment,
        bool showInNavigator,
        int? parentId,
        string? target,
        bool isCompleted,
        DateTime? startDate,
        DateTime? endDate)
    {
        public int Id { get; } = id;
        public string Name { get; } = name;
        public string? ColorRGB { get; } = colorRGB;
        public bool IsGroup { get; } = isGroup;
        public string? Comment { get; } = comment;
        public bool ShowInNavigator { get; } = showInNavigator;
        public int? ParentId { get; } = parentId;
        public string? Target { get; } = target;
        public bool IsCompleted { get; } = isCompleted;
        public DateTime? StartDate { get; } = startDate;
        public DateTime? EndDate { get; } = endDate;
    }
}
