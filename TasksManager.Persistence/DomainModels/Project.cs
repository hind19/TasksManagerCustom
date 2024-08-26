using SQLite;
using System;
using TasksManager.Persistence.DomainModels.Abstract;

namespace TasksManager.Persistence.DomainModels
{
    [Table (Constants.ProjectsTable)]
    internal class Project : BaseProjectCategoryModel
    {
        [MaxLength(1000)]
        public string Target { get; set; }

        [NotNull]
        public bool IsCompleted { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
