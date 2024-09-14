using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksManagerCustom.Persistence.DomainModels.Queries
{
    public class CategoryQueries
    {
        public const string AllCategoriesQuery = "SELECT * FROM Categories";
        public const string ShowInNavigatorOnly = " WHERE Show_In_Navigator = TRUE";
    }
}
