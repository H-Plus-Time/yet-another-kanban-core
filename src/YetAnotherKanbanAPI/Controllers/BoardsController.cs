using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        [HttpGet("{boardId}/{listId}")]
        public TaskList Get(string boardId, string listId)
        {
          return boards.Find((b) => b.Id == boardId)?.Find(listId);
        }

        // POST api/boards
        [HttpPost]
        public ObjectResult PostBoard([FromBody]TaskBoard newBoard)
        {
          newBoard.Id = shortUid.generate();
          boards.Add(newBoard);
          if(boards.Find((b) => b.Id == newBoard.Id) != null) {
            return new CreatedAtRouteResult(new { id = newBoard.Id}, newBoard);
          }
          // Throw a somewhat meaningful error. Make this a bit more robust at some point.
          var response = new ObjectResult(new { statusCode = 415});
          return response;
        }
        // POST api/boards/longuid
        [HttpPost("{boardId}")]
        public ObjectResult PostList(string boardId, [FromBody]TaskList newList)
        {
          var item = boards.Find((b) => b.Id == boardId)?.Add(newList);
          return new CreatedAtRouteResult(new { id = item.Id}, item);
        }

        // POST api/boards/longuid/otherlonguid
        [HttpPost("{boardId}/{listId}")]
        public ObjectResult PostCard(string boardId, string listId, [FromBody]TaskCard newCard)
        {
          newCard.Id = shortUid.generate();
          var item = boards.Find((b) => b.Id == boardId)?.Find(listId).Add(newCard);
          return new CreatedAtRouteResult(new { id = item.Id}, item);
        }

        // DELETE api/boards/longuid
        [HttpDelete("{boardId}")]
        public StatusCodeResult DeleteBoard(string boardId)
        {
          var board = boards.Find((b) => b.Id == boardId);
          if(board == null) {
            return new StatusCodeResult(400);
          }
          boards.Remove(board);
          return new StatusCodeResult(200);
        }

        // DELETE api/boards/longuid/otherlonguid
        [HttpDelete("{boardId}/{listId}")]
        public StatusCodeResult DeleteList(string boardId, string listId)
        {
          if((bool)boards.FirstOrDefault((b) => b.Id == boardId)?.Remove(listId))
          {
            return new StatusCodeResult(200);
          }
          return new StatusCodeResult(400);
        }

        // DELETE api/boards/longuid/otherlonguid/cardlonguid
        [HttpDelete("{boardId}/{listId}/{cardId}")]
        public StatusCodeResult DeleteCard(string boardId, string listId, string cardId)
        {
          if((bool)boards.FirstOrDefault((b) => b.Id == boardId)?.Find(listId).Remove(cardId))
          {
            return new StatusCodeResult(200);
          }
          return new StatusCodeResult(400);
        }

        // PUT api/boards/longuid
        [HttpPut("{boardId}/{listId}/{cardId}")]
        public void Put(string boardId, string listId, string cardId, [FromBody]TaskCard revisionCard)
        {
          boards.FirstOrDefault((b) => b.Id == boardId)?.Find(listId)
            .Find(cardId)?.Update(revisionCard.Title, revisionCard.Content);
        }

        // PUT api/boards/longuid
        [HttpPut("{boardId}/{listId}/{cardId}/move{newListId}")]
        public StatusCodeResult Put(string boardId, string srcId, string cardId, string destId)
        {
          var board = boards.FirstOrDefault((b) => b.Id == boardId);
          if(board == null) {
            return new StatusCodeResult(400);
          }
          var destList = board.Find(destId);
          var srcList = board.Find(srcId);
          if(destList == null || srcList == null) {
            return new StatusCodeResult(400);
          }
          destList.Add(srcList.Find(cardId));
          srcList.Remove(cardId);
          return new StatusCodeResult(200);
        }
    }
}
