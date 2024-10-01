namespace TasksManager.Persistence.Commands
{
    public static class TasksCommands
    {
        public static string UpdatePercentageCommand => $@"UPDATE {Constants.TasksTable} SET PercentageOfCompletion = 75, Status = 3 WHERE Id = 1";
    }
}
