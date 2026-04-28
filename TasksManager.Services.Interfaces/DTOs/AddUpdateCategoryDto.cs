namespace TasksManager.Services.Interfaces.DTOs
{
    public class AddUpdateCategoryDto(
        int id,
        string name,
        int? parentId,
        bool isGroup,
        string colorRGB,
        string comment,
        bool showInNavigator,
        string parentName,
        bool isCreate)
        : CategoryDto(id, name, parentId, isGroup, colorRGB, comment, showInNavigator, parentName)
    {
        public bool IsCreate { get; } = isCreate;
    }
}
