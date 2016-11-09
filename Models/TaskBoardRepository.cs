using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace YetAnotherKanbanAPI.Models
{
    public class TaskBoardRepository : ITaskBoardRepository
    {
        private static ConcurrentDictionary<string, TaskCard> _cards =
              new ConcurrentDictionary<string, TaskCard>();

        public TaskBoardRepository()
        {
            Add(new TaskCard { Name = "Item1" });
        }

        public IEnumerable<TaskCard> GetAll()
        {
            return _cards.Values;
        }

        public void Add(TaskCard item)
        {
            item.Key = Guid.NewGuid().ToString();
            _cards[item.Key] = item;
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
            _cards[item.Key] = item;
        }
    }
}
