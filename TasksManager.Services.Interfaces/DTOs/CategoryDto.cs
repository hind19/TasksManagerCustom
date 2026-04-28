namespace TasksManager.Services.Interfaces.DTOs
{
    public class CategoryDto(
        int id,
        string name,
        int? parentId,
        bool isGroup,
        string colorRGB,
        string comment,
        bool showInNavigator,
        string parentName)
        : ShortCategoryDto(id, name, parentId, isGroup)
    {
        public string ColorRGB { get; } = colorRGB;
        public string Comment { get; } = comment;
        public bool ShowInNavigator { get; } = showInNavigator;
        public string ParentName { get; } = parentName;
    }
}
