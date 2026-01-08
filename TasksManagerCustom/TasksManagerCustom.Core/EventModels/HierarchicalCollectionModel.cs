using System.Collections.Generic;

namespace TasksManager.Core.EventModels
{
    public class HierarchicalCollectionModel
    {
        public List<HierarchicalCollectionModel> Children { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsGroup { get; set; }

        public bool IsSelected { get; set; }

        public bool IsExpanded { get; set; }

        public HierarchicalCollectionModel()
        {
            Children = new List<HierarchicalCollectionModel>();
        }
    }
}
