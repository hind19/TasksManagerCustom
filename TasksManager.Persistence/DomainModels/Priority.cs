using SQLite;

namespace TasksManager.Persistence.DomainModels
{
    [Table(Constants.PrioritiesTable)]
    internal class Priority
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int Id { get; set; }

        [NotNull, MaxLength(255)]
        public string PriorityName { get; set; }
    }
}
