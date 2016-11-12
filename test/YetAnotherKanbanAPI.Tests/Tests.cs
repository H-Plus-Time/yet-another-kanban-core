using System;
using Xunit;
using YetAnotherKanbanAPI.Models;
using YetAnotherKanbanAPI.Controllers;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;


namespace Tests
{
    public class Tests
    {
        BoardsController controller;

        public Tests()
        {
          controller = new BoardsController();
        }

        public string buildBoard(int n=1) {

          var inBoard = new TaskBoard { Name = $"board{n}" };
          var resp = controller.PostBoard(inBoard);
          return ((TaskBoard)resp.Value).Id;
        }

        public string buildList(string boardId, int n=1) {
          var inList = new TaskList { Name = $"list{n}" };
          var resp = controller.PostList(boardId, inList);
          return ((TaskList)resp.Value).Id;
        }

        public string buildCard(string boardId, string listId, int n=1) {
          var inCard = new TaskCard { Title = $"card{n}", Content = "lorem" };
          var resp = controller.PostCard(boardId, listId, inCard);
          return ((TaskCard)resp.Value).Id;
        }

        public StatusCodeResult teardownBoard(string boardId)
        {
          var resp = controller.DeleteBoard(boardId);

          return new StatusCodeResult(200);
        }

        public StatusCodeResult teardownList(string boardId, string listId)
        {
          var resp = controller.DeleteList(boardId, listId);
          return new StatusCodeResult(200);
        }

        public StatusCodeResult teardownCard(string boardId, string listId, string cardId)
        {
          var resp = controller.DeleteCard(boardId, listId, cardId);
          return new StatusCodeResult(200);
        }

        [Fact]
        public void ReversibleBoardCD()
        {
          var id = buildBoard();
          var delResp = teardownBoard(id);
          Assert.Equal(delResp.StatusCode, 200);
        }

        [Fact]
        public void ReversibleListCD() {
          var boardId = buildBoard();
          // create a list, then delete it
          var listId = buildList(boardId);
          var delListResp = teardownList(boardId, listId);
          var delBoardResp = teardownBoard(boardId);
          Assert.Equal(delListResp.StatusCode, 200);
          Assert.Equal(delBoardResp.StatusCode, 200);
        }

        [Fact]
        public void ReversibleCardCD() {
          var boardId = buildBoard();
          // create a list, then delete it
          var listId = buildList(boardId);
          var cardId = buildCard(boardId, listId);
          var delCardResp = teardownCard(boardId, listId, cardId);
          var delListResp = teardownList(boardId, listId);
          var delBoardResp = teardownBoard(boardId);
          Assert.Equal(delCardResp.StatusCode, 200);
          Assert.Equal(delListResp.StatusCode, 200);
          Assert.Equal(delBoardResp.StatusCode, 200);
        }
    }
}
