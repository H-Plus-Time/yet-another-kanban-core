using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using YetAnotherKanbanAPI.Utils;
using Newtonsoft.Json;

namespace YetAnotherKanbanAPI.Models
{
    public class TaskBoard : ITaskBoard
    {
        public string Id {get; set; }
        public string Name {get; set; }

        [JsonProperty()]
        private static ConcurrentDictionary<string, TaskList> _lists { get; set; }

        public TaskBoard()
        {
          _lists = new ConcurrentDictionary<string, TaskList>();
        }

        public IEnumerable<TaskList> GetAll()
        {
            return _lists.Values;
        }

        public TaskList Add(TaskList item)
        {
            item.Id = shortUid.generate();
            _lists[item.Id] = item;
            return item;
        }

        public TaskList Find(string key)
        {
            TaskList item;
            _lists.TryGetValue(key, out item);
            return item;
        }

        public bool Remove(string key)
        {
            TaskList item;
            _lists.TryRemove(key, out item);
            return item != null;
        }

        public void Update(TaskList item)
        {
            _lists[item.Id] = item;
        }
        public override string ToString() {
          return $"Name = {this.Name}, Id = {this.Id}";
        }
    }
}
