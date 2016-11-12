namespace YetAnotherKanbanAPI.Models
{
    public class TaskCard
    {
      public string Id {get; set; }
      public string Title {get; set; }
      public string Content { get; set; }
      public int Age { get; set; }
      public void Update(string title, string content)
      {
        if(title != this.Title) {
          this.Title = title;
        }
        if(content != this.Content) {
          this.Content = content;
        }
      }
    }
}
