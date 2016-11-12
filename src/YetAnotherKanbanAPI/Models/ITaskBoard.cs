using System.Collections.Generic;

namespace YetAnotherKanbanAPI.Models
{
  public interface ITaskBoard
  {
    TaskList Add(TaskList list);
    IEnumerable<TaskList> GetAll();
    TaskList Find(string key);
    bool Remove(string key);
    void Update(TaskList item);
  }
}
