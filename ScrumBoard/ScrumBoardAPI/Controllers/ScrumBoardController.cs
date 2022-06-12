using Microsoft.AspNetCore.Mvc;
using ScrumBoardAPI.Methods;
using ScrumBoardAPI.DTO;


namespace ScrumBoardAPI.Controllers
{
    [ApiController]
    [Route("scrumBoard")]
    public class ScrumBoardController : ControllerBase
    {
        private readonly IUsingMethods _methods;

        public ScrumBoardController(IUsingMethods methods)
        {
            _methods = methods;
        }

        [HttpPost("newBoard")] //тип запроса: POST. адрес запроса: "scrumBoard/newBoard"
        public IActionResult CreateNewBoard([FromBody] CreateBoardDTO arg)
        {
            try
            {
                _methods.AddBoard(arg);
            }

            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("{boardUnicalID}/newColumn")] //тип запроса: POST. адрес запроса: "scrumBoard/{boardUnicalID}/newColumn"
        public IActionResult CreateNewColumn(string boardUnicalID, [FromBody] CreateColumnDTO arg)
        {
            try
            {
                _methods.AddColumn(boardUnicalID, arg);
            }
            catch
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost("{boardUnicalID}/newTask")] //тип запроса: POST. адрес запроса: "scrumBoard/{boardUnicalID}/newTask"
        public IActionResult CreateNewTask(string boardUnicalID, [FromBody] CreateTaskDTO arg)
        {
            try
            {
                _methods.AddTask(boardUnicalID, arg);
            }

            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("{boardUnicalID}")] //тип запроса: GET. адрес запроса: "scrumBoard/{boardUnicalID}"
        public IActionResult GetBoardByUnicalID(string boardUnicalID)
        {
            BoardDTO board;
            try
            {
                board = _methods.GetBoard(boardUnicalID);
            }

            catch
            {
                return NotFound();
            }

            return Ok(board);
        }

        [HttpDelete("{boardUnicalID}/deleteBoard")] //тип запроса: DELETE. адрес запроса: "scrumBoard/{boardUnicalID}/deleteBoard"
        public IActionResult DeleteBoard(string boardUnicalID)
        {
            try
            {
                _methods.DeleteBoard(boardUnicalID);
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{boardUnicalID}/delete/{taskUnicalID}")] //тип запроса: DELETE. адрес запроса: "scrumBoard/{boardUnicalID}/delete/{taskUnicalID}"
        public IActionResult DeleteTask(string boardUnicalID, string taskUnicalID)
        {
            try
            {
                _methods.DeleteTask(boardUnicalID, taskUnicalID);
            }

            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("allBoards")] //тип запроса: GET. адрес запроса: "scrumBoard/allBoards"
        public IActionResult ShowAllBoards()
        {
            IEnumerable<BoardDTO> listBoards;

            try
            {
                listBoards = _methods.GetAllBoard();
            }

            catch
            {
                listBoards = Enumerable.Empty<BoardDTO>();
            }

            return Ok(listBoards);
        }

        [HttpPut("{boardUnicalID}/move/{taskUnicalID}")] //тип запроса: PUT. адрес запроса: "scrumBoard/{boardUnicalID}/move/{taskUnicalID}"
        public IActionResult MoveTaskIntoColumn(string boardUnicalID, string taskUnicalID, [FromBody] MoveTaskBetweenColumnsDTO arg)
        {
            try
            {
                _methods.MoveTask(boardUnicalID, taskUnicalID, arg);
            }

            catch
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
