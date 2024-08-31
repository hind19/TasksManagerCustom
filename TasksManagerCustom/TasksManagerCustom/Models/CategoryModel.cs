using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksManager.Application.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ColorRGB { get; set; }

        public bool IsGroup { get; set; }

        public string Comment { get; set; }

        public bool ShowInNavigator { get; set; } = true;

        public int? ParentId { get; set; }

        public string ParentName { get; set; }
    }
}
