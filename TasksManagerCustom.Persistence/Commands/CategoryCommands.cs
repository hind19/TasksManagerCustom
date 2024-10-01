namespace TasksManager.Persistence.Commands
{
    public static class CategoryCommands
    {
        public static string CreateCategoryCommand => @$"INSERT INTO {Constants.CategoriesTable}
(Name,ColorRGB,IsGroup,COMMENT,ShowinNavigator,ParentId)
VALUES(?,?,?,?,?,?)";
    }
}
