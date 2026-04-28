namespace TasksManager.Application.Models
{
    public class NameValuePair<T>(string name, T value)
    {
        public string Name { get; } = name;
        public T Value { get; } = value;
    }
}
