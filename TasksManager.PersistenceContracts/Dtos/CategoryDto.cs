using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace TasksManager.PersistenceContracts.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ColorRGB { get; set; }

        public bool IsGroup { get; set; }

        public string Comment { get; set; }

        public bool ShowInNavigator { get; set; } = true;

        public int? ParentId { get; set; }
    }
}
