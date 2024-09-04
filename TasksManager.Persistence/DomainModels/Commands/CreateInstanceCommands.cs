namespace TasksManager.Persistence.DomainModels.Commands
{
    public static class CreateInstanceCommands
    {
        public static string CreateCategoryCommand => @$"INSERT INTO {Constants.CategoriesTable}
(Name,ColorRGB,Is_Group,COMMENT,Show_in_Navigator,ParentId)
VALUES(?,?,?,?,?,?)";
    }
}
