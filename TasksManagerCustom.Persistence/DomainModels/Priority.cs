using SQLite;
using TasksManager.Persistence.DomainModels.Abstract;

namespace TasksManager.Persistence.DomainModels
{
    [Table(Constants.PrioritiesTable)]
    internal class Priority : BaseTable
    {
        [NotNull, MaxLength(255)]
        public string PriorityName { get; set; }
    }
}
