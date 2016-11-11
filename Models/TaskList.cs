using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace YetAnotherKanbanAPI.Models
{
    public class TaskList
    {
        public string Id {get; set; }
        public string Name {get; set; }
        public IEnumerable<TaskCard> cards { get; set; }

        private static ConcurrentDictionary<string, TaskCard> _cards =
              new ConcurrentDictionary<string, TaskCard>();

        public TaskList()
        {
            Add(new TaskCard { Title = "Item1" });
        }

        public IEnumerable<TaskCard> GetAll()
        {
            return _cards.Values;
        }

        public void Add(TaskCard item)
        {
            item.Id = Guid.NewGuid().ToString();
            _cards[item.Id] = item;
        }

        public TaskCard Find(string key)
        {
            TaskCard item;
            _cards.TryGetValue(key, out item);
            return item;
        }

        public TaskCard Remove(string key)
        {
            TaskCard item;
            _cards.TryRemove(key, out item);
            return item;
        }

        public void Update(TaskCard item)
        {
            _cards[item.Id] = item;
        }
    }
}
