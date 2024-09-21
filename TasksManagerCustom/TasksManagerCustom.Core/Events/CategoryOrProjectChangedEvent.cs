using Prism.Events;
using System;
using TasksManager.Core.Enums;
using TasksManager.Core.EventModels;

namespace TasksManager.Core.Events
{
    /// <summary>
    /// Raises when categoey or project are changed on the left panel and we need to load tasks to central panel
    /// </summary>
    public class CategoryOrProjectChangedEvent : PubSubEvent<Tuple<HierarchicalCollectionModel, CategoryProjectEnum>>
    {
    }
}
