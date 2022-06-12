using Microsoft.AspNetCore.Mvc;
using ScrumBoardAPI.DTO;
using ApplicationCore.Interfaces;
using ApplicationCore.DTO;

namespace ScrumBoardAPI.Controllers
{
    [ApiController]
    [Route("scrumBoard")]
    public class ScrumBoardController : ControllerBase
    {
        private readonly IScrumBoardRepository _repository;

        public ScrumBoardController(IScrumBoardRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("newBoard")] //POST. "scrumBoard/newBoard"
        public IActionResult CreateNewBoard([FromBody] CreateBoardDTO arg)
        {
            try
            {
                _repository.AddBoard(arg);
            }

            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("{boardUnicalID}/newColumn")] //POST. "scrumBoard/{boardUnicalID}/newColumn"
        public IActionResult CreateNewColumn(int boardUnicalID, [FromBody] CreateColumnDTO arg)
        {
            try
            {
                _repository.AddColumn(boardUnicalID, arg);
            }
            catch
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost("{boardUnicalID}/newTask")] //POST. "scrumBoard/{boardUnicalID}/newTask"
        public IActionResult CreateNewTask(int boardUnicalID, [FromBody] CreateTaskDTO arg)
        {
            try
            {
                _repository.AddTask(boardUnicalID, arg);
            }

            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("{boardUnicalID}")] //GET. "scrumBoard/{boardUnicalID}"
        public IActionResult GetBoardByUnicalID(int boardUnicalID)
        {
            BoardDTO board;
            try
            {
                board = _repository.GetBoard(boardUnicalID);
            }

            catch
            {
                return NotFound();
            }

            return Ok(board);
        }

        [HttpDelete("{boardUnicalID}/deleteBoard")] //DELETE. "scrumBoard/{boardUnicalID}/deleteBoard"
        public IActionResult DeleteBoard(int boardUnicalID)
        {
            try
            {
                _repository.DeleteBoard(boardUnicalID);
            }

            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{columnUnicalID}/deleteColumn")] //DELETE. "scrumBoard/{columnUnicalID}/deleteColumn"
        public IActionResult DeleteColumn(int columnUnicalID)
        {
            try
            {
                _repository.DeleteColumn(columnUnicalID);
            }

            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{taskUnicalID}/deleteTask")] //DELETE. "scrumBoard/{taskUnicalID}/deleteTask"
        public IActionResult DeleteTask(int taskUnicalID)
        {
            try
            {
                _repository.DeleteTask(taskUnicalID);
            }

            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("allBoards")] //GET. "scrumBoard/allBoards"
        public IActionResult ShowAllBoards()
        {
            IEnumerable<BoardDTO> listBoards;

            try
            {
                listBoards = _repository.GetAllBoard();
            }

            catch
            {
                listBoards = Enumerable.Empty<BoardDTO>();
            }

            return Ok(listBoards);
        }

        [HttpPut("{columnUnicalID}/move/{taskUnicalID}")] // PUT. "scrumBoard/{columnUnicalID}/move/{taskUnicalID}"
        public IActionResult MoveTaskIntoColumn(int columnUnicalID, int taskUnicalID)
        {
            try
            {
                _repository.MoveTask(columnUnicalID, taskUnicalID);
            }

            catch
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}