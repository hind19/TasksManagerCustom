using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksManager.Persistence.Queries
{
    internal class TasksQueries
    {
        internal const string AllTasksQuery = "SELECT * FROM Tasks";
        internal const string ProjectFilterClause = " WHERE ProjectId in ({0})";
        internal const string CategoryFilterClause = " WHERE CategoryId in ({0})";

    }
}
