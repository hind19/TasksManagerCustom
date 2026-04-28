namespace TasksManager.Services.Interfaces.DTOs
{
    public class ShortCategoryDto(int id, string name, int? parentId, bool isGroup)
    {
        public int Id { get; } = id;
        public string Name { get; } = name;
        public int? ParentId { get; } = parentId;
        public bool IsGroup { get; } = isGroup;
    }
}
