using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YetAnotherKanbanAPI.Models;

namespace YetAnotherKanbanAPI.Controllers
{
    [Route("api/[controller]")]
    public class BoardsController : Controller
    {
        TaskBoard[] boards = new TaskBoard[]
        {
          new TaskBoard { Id = Guid.NewGuid().ToString(), Name = "PRJ_3010", lists = new TaskList[] {
                new TaskList { Id = Guid.NewGuid().ToString(), Name = "To Do", cards = new TaskCard[] {
                    new TaskCard { Id = Guid.NewGuid().ToString(), Title = "lorem", Content = "Ipsum"}
                  }
                },
                new TaskList { Id = Guid.NewGuid().ToString(), Name = "Doing", cards = new TaskCard[] {
                    new TaskCard {Id = Guid.NewGuid().ToString(), Title = "lorem", Content = "ipsum"}
                  }
                }
            }
          },
          new TaskBoard { Id = Guid.NewGuid().ToString(), Name = "COSC3020", lists = new TaskList[] {
              new TaskList { Id = Guid.NewGuid().ToString(), Name = "To Do", cards = new TaskCard[] {} }
            }
          }
        };

        // GET api/values
        [HttpGet]
        public IEnumerable<TaskBoard> Get()
        {
            return boards;
        }

        // GET api/boards/PRJ_3010
        [HttpGet("{id}")]
        public JsonResult Get(string id)
        {
            var board = boards.FirstOrDefault((b) => b.Id == id);
            return board != null ? Json(board) : Json("");
        }

        // POST api/boards
        [HttpPost]
        public string Post([FromBody]string value)
        {
          Console.WriteLine("printing entries");
          Console.WriteLine(value);
          Console.WriteLine("printed body");
          return value;
        }

        // PUT api/boards
        [HttpPut("{boardId}/{listId}")]
        public TaskCard Put(string boardId, string listId, [FromBody]TaskCard value)
        {
          Console.WriteLine(boards.FirstOrDefault((b) => b.Id == boardId));
          Console.WriteLine(value);
          return value;

        }

        // DELETE api/boards/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
        }
    }
}
