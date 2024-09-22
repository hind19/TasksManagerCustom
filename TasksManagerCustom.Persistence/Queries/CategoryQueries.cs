using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksManager.Persistence.Queries
{
    internal class CategoryQueries
    {
        internal const string AllCategoriesQuery = "SELECT * FROM Categories";
        internal const string ShowInNavigatorOnly = " WHERE ShowInNavigator = TRUE";
    }
}
