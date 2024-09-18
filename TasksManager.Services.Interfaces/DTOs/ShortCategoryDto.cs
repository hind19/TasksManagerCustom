using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksManager.Services.Interfaces.DTOs
{
    public class ShortCategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public bool IsGroup { get; set; }
    }
}
