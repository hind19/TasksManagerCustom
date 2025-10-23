using SQLite;

namespace TasksManager.Persistence.DomainModels.Abstract
{
    public abstract class BaseTable
    {
        [PrimaryKey, NotNull, AutoIncrement]
        public int Id { get; set; }
    }
}
