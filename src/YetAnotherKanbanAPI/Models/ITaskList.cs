using System.Collections.Generic;

namespace YetAnotherKanbanAPI.Models
{
  public interface ITaskList
  {
    TaskCard Add(TaskCard card);
    IEnumerable<TaskCard> GetAll();
    TaskCard Find(string key);
    bool Remove(string key);
    void Update(TaskCard item);
  }
}
