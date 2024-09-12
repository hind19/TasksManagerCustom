using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TasksManager.Persistence.DomainModels.Queries
{
    public static class DatabaseConstants
    {
        public static string CreatePrioritiesTableQuery => $"CREATE TABLE IF NOT EXISTS {Constants.PrioritiesTable} (Id INTEGER PRIMARY KEY AUTOINCREMENT, PriorityName VARCHAR(255));";

        public static string CreateProjectsTableQuery => @$"CREATE TABLE IF NOT EXISTS {Constants.ProjectsTable} 
(Id INTEGER PRIMARY KEY AUTOINCREMENT,
Name VARCHAR(255),
ColorRGB VARCHAR(10),
Is_Group BOOLEAN,
COMMENT VARCHAR(1000),
Show_in_Navigator BOOLEAN,
Target VARCHAR(1000),
Is_Completed BOOLEAN,
StartDate VARCHAR(25),
EndDate VARCHAR(25),
ParentId INTEGER,
FOREIGN KEY(ParentId) REFERENCES {Constants.ProjectsTable}(Id));";

        public static string CreateCategoriesTableQuery => @$"CREATE TABLE IF NOT EXISTS {Constants.CategoriesTable} 
(Id INTEGER PRIMARY KEY AUTOINCREMENT,
Name VARCHAR(255),
ColorRGB VARCHAR(10),
Is_Group BOOLEAN,
COMMENT VARCHAR(1000),
Show_in_Navigator BOOLEAN,
ParentId INTEGER,
FOREIGN KEY(ParentId) REFERENCES {Constants.CategoriesTable}(Id));";


        public static string CreateTaskTableQuery => @$"CREATE TABLE IF NOT EXISTS {Constants.TasksTable}
(Id INTEGER PRIMARY KEY AUTOINCREMENT,
TaskName VARCHAR(1000),
ProjectId INTEGER,
CategoryId INTEGER,
StartDate VARCHAR(25),
EndDate VARCHAR(25),
Status INTEGER,
PriorityId INTEGER,
PercentageOfCompletion INTEGER,
FOREIGN KEY(PriorityId) REFERENCES {Constants.PrioritiesTable}(Id),
FOREIGN KEY(ProjectId) REFERENCES {Constants.ProjectsTable}(Id),
FOREIGN KEY(CategoryId) REFERENCES {Constants.CategoriesTable}(Id))";
    }
}
