namespace TasksManager.Application.Models
{
    public class NameValuePair<T>
    {
        public NameValuePair(string name, T value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public T Value { get; set; }
    }
}
