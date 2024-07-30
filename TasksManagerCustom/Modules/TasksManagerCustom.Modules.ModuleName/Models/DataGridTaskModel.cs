﻿using System;

namespace TasksScheduleModule.Models
{
    internal class DataGridTaskModel
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public DateTime StartDate { get; set; }
        public  DateTime EndDate { get; set; }
    }
}
