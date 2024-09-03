using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TasksManager.Persistence.DomainModels.Queries;

namespace TasksManager.Persistence.DomainModels.Commands
{
    public class CreateInstanceCommands
    {
        internal const string nameParam = "@nameParam";
        internal const string colorParam = "@colorParam";
        internal const string isGroupParam = "@isGroupParam";
        internal const string commentParam = "@commentParam";
        internal const string showInNavigatorParam = "@showInNavigatorParam";
        internal const string parentIdParam = "@parentIdParam";



        public static string CreateCategoryCommand => @$"INSERT INTO {Constants.CategoriesTable}
(Name,
ColorRGB,
Is_Group,
COMMENT,
Show_in_Navigator,
ParentId)
VALUES(?,?,?,?,?,?)";

    }
}
