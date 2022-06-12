using ScrumBoardLibrary.Column;
using ScrumBoardLibrary.Task;

namespace ScrumBoardLibrary.Board
{
    public interface IBoard
    {
        public string UnicalID { get; }
        public string BoardName { get; }
        public void AddTaskIntoColumn(ITask task, int columnNum = 0);
        public void MoveTaskBetweenColumns(string columnUnicalID, string  taskUnicalID);
        public ITask GetTask(string taskUnicalID);
        public void DeleteTask(string unicalID);

        public void AddColumn(IColumn column);
        public IColumn GetColumn(string columnUnicalID);
        public List<IColumn> LookAllColumn();


    }
}
