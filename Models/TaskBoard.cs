using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace YetAnotherKanbanAPI.Models
{
    public class TaskBoard : ITaskBoard
    {
        public string Id {get; set; }
        public string Name {get; set; }
        public IEnumerable<TaskList> lists { get; set; }

        private static ConcurrentDictionary<string, TaskList> _lists =
              new ConcurrentDictionary<string, TaskList>();

        public TaskBoard()
        {
            Add(new TaskList { Id = Guid.NewGuid().ToString(), Name = "List1" , cards = new TaskCard[] {}});
        }

        public IEnumerable<TaskList> GetAll()
        {
            return _lists.Values;
        }

        public void Add(TaskList item)
        {
            item.Id = Guid.NewGuid().ToString();
            _lists[item.Id] = item;
        }

        public TaskList Find(string key)
        {
            TaskList item;
            _lists.TryGetValue(key, out item);
            return item;
        }

        public TaskList Remove(string key)
        {
            TaskList item;
            _lists.TryRemove(key, out item);
            return item;
        }

        public void Update(TaskList item)
        {
            _lists[item.Id] = item;
        }
    }
}
