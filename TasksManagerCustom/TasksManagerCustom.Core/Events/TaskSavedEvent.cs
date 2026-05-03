using System;
using Prism.Events;

namespace TasksManager.Core.Events
{
    // Payload: Item1 = CategoryId of the saved task (null if none), Item2 = true when task was newly created
    public class TaskSavedEvent : PubSubEvent<Tuple<int?, bool>>
    {
    }
}
