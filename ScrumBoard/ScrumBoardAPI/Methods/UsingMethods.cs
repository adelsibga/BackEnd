using ScrumBoardLibrary.Board;
using ScrumBoardLibrary.Column;
using ScrumBoardLibrary.Task;
using ScrumBoardAPI.DTO;
using Microsoft.Extensions.Caching.Memory;
using Task = ScrumBoardLibrary.Task.Task;


namespace ScrumBoardAPI.Methods
{
    public class UsingMethods: IUsingMethods
    {
        private readonly IMemoryCache _memoryCache;
        public UsingMethods(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        private static Priority GetTaskPriority(int priority)
        {
            switch (priority)
            {
                case 0:
                    priority = (int)Priority.HIGH;
                    break;
                case 1:
                    priority = (int)Priority.NORMAL;
                    break;
                case 2:
                    priority = (int)Priority.LOW;
                    break;
                default:
                    throw new System.Exception("Неизвестная сложность задачи.");
            }
            return (Priority)priority;
        }

        private List<IBoard> GetListBoards()
        {
            _memoryCache.TryGetValue("boards", out List<IBoard> listBoards);

            if (listBoards == null)
                throw new System.Exception("Невозможно выполнить действие! Список досок пуст.");

            return listBoards;
        }

        private int GetIndexBoard(string boardUnicalID)
        {
            List<IBoard> listBoards = GetListBoards();
            int listBoardsLength = listBoards.Count;

            for (int i = 0; i < listBoardsLength; i++)
            {
                if (listBoards[i].UnicalID == boardUnicalID)
                    return i;
            }
            throw new System.Exception("Доска не найдена.");
        }
        public void AddBoard(CreateBoardDTO arg)
        {
            List<IBoard> listBoards;
            try
            {
                listBoards = GetListBoards();
            }
            catch (System.Exception)
            {
                listBoards = new List<IBoard>();
            }

            listBoards.Add(new Board(arg.BoardName));
            _memoryCache.Set("boards", listBoards);
        }

        public void AddColumn(string boardUnicalID, CreateColumnDTO arg)
        {
            List<IBoard> listBoards = GetListBoards();
            listBoards[GetIndexBoard(boardUnicalID)].AddColumn(new Column(arg.ColumnName));
            _memoryCache.Set("boards", listBoards);
        }

        public void AddTask(string boardUnicalID, CreateTaskDTO arg)
        {
            List<IBoard> listBoards = GetListBoards();
            listBoards[GetIndexBoard(boardUnicalID)].AddTaskIntoColumn(new Task(arg.TaskName, arg.TaskDescription, GetTaskPriority(arg.TaskPriority)));
            _memoryCache.Set("boards", listBoards);
        }
        public BoardDTO GetBoard(string boardUnicalID)
        {
            BoardDTO board = new(GetListBoards()[GetIndexBoard(boardUnicalID)]);
            return board;
        }

        public void DeleteBoard(string boardUnicalID)
        {
            List<IBoard> listBoards = GetListBoards();
            int listBoardsLength = listBoards.Count;

            for (int i = 0; i < listBoardsLength; i++)
            {
                if (listBoards[i].UnicalID == boardUnicalID)
                {
                    listBoards.RemoveAt(i);
                    _memoryCache.Set("boards", listBoards);
                    return;
                }
            }

            throw new System.Exception("Доска не найдена.");
        }

        public void DeleteTask(string columnUnicalID, string taskUnicalID)
        {
            List<IBoard> listBoards = GetListBoards();
            listBoards[GetIndexBoard(columnUnicalID)].DeleteTask(taskUnicalID);
            _memoryCache.Set("boards", listBoards);
        }
        public IEnumerable<BoardDTO> GetAllBoard()
        {
            IEnumerable <BoardDTO> allBoards = GetListBoards().Select(board => new BoardDTO(board));
            return allBoards;
        }
        public void MoveTask(string boardUnicalID, string taskUnicalID, MoveTaskBetweenColumnsDTO arg)
        {
            List<IBoard> boards = GetListBoards();

            boards[GetIndexBoard(boardUnicalID)].MoveTaskBetweenColumns(arg.columnUnicalID, taskUnicalID);

            _memoryCache.Set("boards", boards);
        }
    }
}
