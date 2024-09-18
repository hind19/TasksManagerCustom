namespace TasksManager.Persistence.DomainModels.Commands
{
    public static class CreateInstanceCommands
    {
        public static string CreateCategoryCommand => @$"INSERT INTO {Constants.CategoriesTable}
(Name,ColorRGB,IsGroup,COMMENT,ShowinNavigator,ParentId)
VALUES(?,?,?,?,?,?)";
    }
}
