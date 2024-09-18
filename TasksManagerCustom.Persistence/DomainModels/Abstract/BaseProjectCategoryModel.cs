using SQLite;
using TasksManagerCustom.Persistence.DomainModels.Abstract;

namespace TasksManager.Persistence.DomainModels.Abstract
{
    internal class BaseProjectCategoryModel : BaseTable
    {
        [ MaxLength(255), NotNull]
        public string? Name { get; set; }

        [MaxLength(10)]
        public string? ColorRGB { get; set; }

        public bool IsGroup { get; set; }

        [MaxLength(2000)]
        public string? Comment { get; set; }

        [NotNull]
        public bool ShowInNavigator { get; set; }

        public int? ParentId { get; set; }
    }
}
