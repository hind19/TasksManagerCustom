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
FOREIGN KEY(ParentID) REFERENCES {Constants.ProjectsTable}(Id));";

        public static string CreateCategoriesTableQuery => @$"CREATE TABLE IF NOT EXISTS {Constants.CategoriesTable} 
(Id INTEGER PRIMARY KEY AUTOINCREMENT,
Name VARCHAR(255),
ColorRGB VARCHAR(10),
Is_Group BOOLEAN,
COMMENT VARCHAR(1000),
Show_in_Navigator BOOLEAN,
FOREIGN KEY(ParentID) REFERENCES {Constants.CategoriesTable}(Id));";


        public static string CreateTaskTableQuery => @$"CREATE TABLE IF NOT EXISTS {Constants.CategoriesTable}
(Id INTEGER PRIMARY KEY AUTOINCREMENT,
TaskName VARCHAR(1000),
FOREIGN KEY(ProjectId) REFERENCES {Constants.ProjectsTable}(ID),
FOREIGN KEY(CategoryId) REFERENCES {Constants.CategoriesTable}(ID),
StartDate VARCHAR(25),
EndDate VARCHAR(25),
INTEGER Statis,
FOREIGN KEY(PriorityId) REFERENCES {Constants.PrioritiesTable}(Id),
PercentageOfCompletion INTEGER";
    }
}
