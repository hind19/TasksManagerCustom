using System;
using System.Collections.Generic;
using System.Text;

namespace TasksManager.Services.DTOs
{
    public class AddUpdateCategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ColorRGB { get; set; }

        public bool IsGroup { get; set; }

        public string Comment { get; set; }

        public bool ShowInNavigator { get; set; }

        public int? ParentId { get; set; }

        public string ParentName { get; set; }

        public bool IsCreate { get; set; }
    }
}
