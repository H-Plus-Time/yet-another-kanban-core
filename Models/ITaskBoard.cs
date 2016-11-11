using System.Collections.Generic;

namespace YetAnotherKanbanAPI.Models
{
  public interface ITaskBoard
  {
    void Add(TaskList list);
    IEnumerable<TaskList> GetAll();
    TaskList Find(string key);
    TaskList Remove(string key);
    void Update(TaskList item);
  }
}
