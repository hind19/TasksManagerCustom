using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using TasksScheduleModule.Models;

namespace TasksManagerCustom.Modules.ModuleName.ViewModels
{
    internal class TaskScheduleViewModel :BindableBase
    {
        public TaskScheduleViewModel()
        {
            CurrentTasksList = new ObservableCollection<DataGridTaskModel>
            {
                new DataGridTaskModel
                {
                    Id = 1,
                    TaskName = "Task 1",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(10)
                },
                new DataGridTaskModel
                {
                    Id = 2,
                    TaskName = "Task 2",
                    StartDate = DateTime.Now.AddDays(-5),
                    EndDate = DateTime.Now.AddDays(18)
                }

            };
        }
        public ObservableCollection<DataGridTaskModel> CurrentTasksList { get; set; }
    }
}
