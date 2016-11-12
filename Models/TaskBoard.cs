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

        public void Add(TaskList item)
        {
            item.Id = shortUid.generate();
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
        public override string ToString() {
          return $"Name = {this.Name}, Id = {this.Id}";
        }
    }
}
