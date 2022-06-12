using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ApplicationCore.Interfaces;
using ApplicationCore.DTO;
using ApplicationCore.Entities.ScrumBoardAggregate;
using Task = ApplicationCore.Entities.ScrumBoardAggregate.Task;
using ScrumBoardAPI.DTO;

namespace Infrastructure.Data
{
    public class ScrumBoardRepository : IScrumBoardRepository
    {
        private readonly ScrumBoardContext _databases;
        public ScrumBoardRepository(IConfiguration configuration)
        {
            var contextOptionsBuilder = new DbContextOptionsBuilder<ScrumBoardContext>();

            DbContextOptions<ScrumBoardContext>? options = contextOptionsBuilder.UseMySql(configuration.GetConnectionString("Default"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("Default"))).Options;

            _databases = new ScrumBoardContext(options);
        }

        public void AddBoard(CreateBoardDTO arg)
        {
            Board board = new(arg.BoardName);

            _databases.Boards.Add(board);
            _databases.SaveChanges();
        }

        public void AddColumn(int boardUnicalID, CreateColumnDTO arg)
        {
            Board? board = _databases.Boards.Include(c => c.Columns)
                .Where(b => b.BoardUnicalID == boardUnicalID).FirstOrDefault();

            if (board != null)
            {
                if (board.Columns.Count() < 10)
                {
                    Column column = new(arg.ColumnName, boardUnicalID);

                    _databases.Columns.Add(column);
                    _databases.SaveChanges();
                }

                else
                    throw new System.Exception("Количество колонок не может превышать 10.");
            }

            else
                throw new System.Exception("Доска не найдена.");
        }

        public void AddTask(int boardUnicalID, CreateTaskDTO arg)
        {
            if ((arg.TaskPriority < 0) || (arg.TaskPriority > 2))
                throw new System.Exception("Приоритет задачи не может быть меньше 0 и больше 2");

            Board? board = _databases.Boards.Include(c => c.Columns)
                .Where(b => b.BoardUnicalID == boardUnicalID).FirstOrDefault();

            if (board != null)
            {
                Column? column = board.Columns.FirstOrDefault();
                if (column != null)
                {
                    Task task = new(arg.TaskName, arg.TaskDescription, arg.TaskPriority, column.ColumnUnicalID);

                    _databases.Tasks.Add(task);
                    _databases.SaveChanges();
                }

                else
                    throw new System.Exception("Колонка не найдена");
            }

            else
                throw new System.Exception("Доска не найдена");
        }

        public BoardDTO GetBoard(int boardUnicalID)
        {
            Board? board = _databases.Boards.Include(c => c.Columns)
                .ThenInclude(t => t.Tasks).Where(b => b.BoardUnicalID == boardUnicalID).FirstOrDefault();

            if (board == null)
                throw new System.Exception("Доска не найдена");

            return new BoardDTO(board);
        }

        public IEnumerable<BoardDTO> GetAllBoard()
        {
            return _databases.Boards.Include(c => c.Columns).ThenInclude(t => t.Tasks).Select(board => new BoardDTO(board));
        }

        public void DeleteBoard(int boardUnicalID)
        {
            Board? board = _databases.Boards.Find(boardUnicalID);

            if (board != null)
            {
                _databases.Boards.Remove(board);
                _databases.SaveChanges();
            }

            else
                throw new System.Exception("Доска не найдена");
        }

        public void DeleteColumn(int columnUnicalID)
        {
            Column? column = _databases.Columns.Find(columnUnicalID);

            if (column != null)
            {
                _databases.Columns.Remove(column);
                _databases.SaveChanges();
            }

            else
                throw new System.Exception("Колонка не найдена");
        }

        public void DeleteTask(int taskUnicalID)
        {
            Task? task = _databases.Tasks.Find(taskUnicalID);

            if (task != null)
            {
                _databases.Tasks.Remove(task);
                _databases.SaveChanges();
            }

            else
                throw new System.Exception("Задача не найдена");
        }

        public void MoveTask(int columnUnicalID, int taskUnicalID)
        {
            Task? task = _databases.Tasks.Find(taskUnicalID);
            Column? column = _databases.Columns.Find(columnUnicalID);

            if (task == null)
                throw new System.Exception("Задача не найдена");

            if (column == null)
                throw new System.Exception("Колонка не найдена");

            task.ColumnID = columnUnicalID;

            _databases.SaveChanges();
        }

    }
}
