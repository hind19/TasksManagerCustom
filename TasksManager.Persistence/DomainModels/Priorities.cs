using SQLite;

namespace TasksManager.Persistence.DomainModels
{
    [Table("Priorities")]
    internal class Priorities
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int Id { get; set; }

        [NotNull]
        public string PriorityName { get; set; }
    }
}
