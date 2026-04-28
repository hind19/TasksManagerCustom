namespace TasksManager.PersistenceContracts.Dtos
{
    public class PersistenceCategoryDto(
        int id,
        string name,
        string colorRGB,
        bool isGroup,
        string comment,
        bool showInNavigator = true,
        int? parentId = null)
    {
        public int Id { get; } = id;
        public string Name { get; } = name;
        public string ColorRGB { get; } = colorRGB;
        public bool IsGroup { get; } = isGroup;
        public string Comment { get; } = comment;
        public bool ShowInNavigator { get; } = showInNavigator;
        public int? ParentId { get; } = parentId;
    }
}
