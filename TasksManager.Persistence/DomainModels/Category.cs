using SQLite;
using TasksManager.Persistence.DomainModels.Abstract;

namespace TasksManager.Persistence.DomainModels
{
    [Table(Constants.CategoriesTable)]
    internal class Category : BaseProjectCategoryModel
    {
    }
}
