using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksManager.Services.Interfaces.DTOs
{
    public class CategoryDto : ShortCategoryDto
    {
        public string ColorRGB { get; set; }

        public bool IsGroup { get; set; }

        public string Comment { get; set; }

        public bool ShowInNavigator { get; set; }

        public string ParentName { get; set; }

    }
}
