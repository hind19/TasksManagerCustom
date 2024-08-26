using SQLite;

namespace TasksManager.Persistence.DomainModels.Abstract
{
    internal abstract class BaseProjectCategoryModel
    {
        [PrimaryKey, NotNull, AutoIncrement]
        public int Id { get; set; }
        
        [ MaxLength(255), NotNull]
        public string Name { get; set; }

        [MaxLength(10)]
        public string ColorRGB { get; set; }

        public bool IsGroup { get; set; }

        [MaxLength(2000)]
        public string Comment { get; set; }

        [NotNull]
        public bool ShowInNavigator { get; set; } = true;

        public int? ParentId { get; set; }
    }
}
