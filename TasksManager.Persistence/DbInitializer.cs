using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TasksManager.Persistence.DomainModels;

namespace TasksManager.Persistence
{
    internal class DbInitializer
    {
        internal void CreateDatabase()
        {
            if (!System.IO.File.Exists(@"C:\Users\abc\Desktop\1\synccc.sqlite"))
            {
                Console.WriteLine("Just entered to create Sync DB");
                //SQLiteConnection.CreateFile(@"C:\Users\abc\Desktop\1\synccc.sqlite");

                using (var sqlite2 = new SQLiteConnection(@"Data Source=C:\Users\abc\Desktop\1\synccc.sqlite"))
                {
                    sqlite2.CreateTable<Priorities>();
                    //string sql = "create table highscores (name varchar(20), score int)";
                    //SQLiteCommand command = new SQLiteCommand(sql, sqlite2);
                    //command.ExecuteNonQuery();
                }
            }
        }
    }
}
