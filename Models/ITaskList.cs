using System.Collections.Generic;

namespace YetAnotherKanbanAPI.Models
{
  public interface ITaskList
  {
    void Add(TaskCard card);
    IEnumerable<TaskCard> GetAll();
    TaskCard Find(string key);
    TaskCard Remove(string key);
    void Update(TaskCard item);
  }
}
