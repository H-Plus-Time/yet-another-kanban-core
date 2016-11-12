using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using YetAnotherKanbanAPI.Utils;
using Newtonsoft.Json;

namespace YetAnotherKanbanAPI.Models
{
    public class TaskList : ITaskList
    {
        public string Id {get; set; }
        public string Name {get; set; }

        [JsonProperty()]
        private static ConcurrentDictionary<string, TaskCard> _cards =
              new ConcurrentDictionary<string, TaskCard>();

        public TaskList()
        {
        }

        public IEnumerable<TaskCard> GetAll()
        {
            return _cards.Values;
        }

        public TaskCard Add(TaskCard item)
        {
            item.Id = shortUid.generate();
            _cards[item.Id] = item;
            return item;
        }

        public TaskCard Find(string key)
        {
            TaskCard item;
            _cards.TryGetValue(key, out item);
            return item;
        }

        public bool Remove(string key)
        {
            TaskCard item;
            _cards.TryRemove(key, out item);
            return item != null;
        }

        public void Update(TaskCard item)
        {
            _cards[item.Id] = item;
        }
        public override string ToString()
        {
          return $"Name = {this.Name}, Id = {this.Id}";
        }
    }
}
