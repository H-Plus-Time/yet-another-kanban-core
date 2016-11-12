using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YetAnotherKanbanAPI.Models;
using YetAnotherKanbanAPI.Utils;
using Newtonsoft.Json;

namespace YetAnotherKanbanAPI.Controllers
{
    [Route("api/[controller]")]
    public class BoardsController : Controller
    {
        static List<TaskBoard> boards = new List<TaskBoard>();
        // GET api/values
        [HttpGet]
        public IEnumerable<TaskBoard> Get()
        {
            return boards;
        }

        // GET api/boards/longuid
        [HttpGet("{id}")]
        public TaskBoard Get(string id)
        {
            var board = boards.FirstOrDefault((b) => b.Id == id);
            return board;
        }

        // POST api/boards
        [HttpPost]
        public string Post([FromBody]string value)
        {
          return value;
        }

        // PUT api/boards
        [HttpPut]
        public void Put([FromBody]TaskBoard newBoard)
        {
          newBoard.Id = shortUid.generate();
          boards.Add(newBoard);
        }
        // PUT api/boards/longuid
        [HttpPut("{boardId}")]
        public void Put(string boardId, [FromBody]TaskList newList)
        {
          boards.Find((b) => b.Id == boardId)?.Add(newList);
        }

        // PUT api/boards/longuid/otherlonguid
        [HttpPut("{boardId}/{listId}")]
        public void Put(string boardId, string listId, [FromBody]TaskCard newCard)
        {
          newCard.Id = shortUid.generate();
          boards.Find((b) => b.Id == boardId)?.Find(listId).Add(newCard);
        }

        [HttpGet("{boardId}/{listId}")]
        public TaskList Get(string boardId, string listId)
        {
          return boards.Find((b) => b.Id == boardId)?.Find(listId);
        }

        // DELETE api/boards/longuid
        [HttpDelete("{boardId}")]
        public void Delete(string boardId)
        {
          boards.Remove(boards.Find((b) => b.Id == boardId));
        }

        // DELETE api/boards/longuid/otherlonguid
        [HttpDelete("{boardId}/{listId}")]
        public void Delete(string boardId, string listId)
        {
          boards.FirstOrDefault((b) => b.Id == boardId)?.Remove(listId);
        }
    }
}
