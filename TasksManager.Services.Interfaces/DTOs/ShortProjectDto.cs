namespace TasksManager.Services.Interfaces.DTOs
{
    public class ShortProjectDto(int id, string name)
    {
        public int Id { get; } = id;
        public string Name { get; } = name;
    }
}
