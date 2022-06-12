using ScrumBoardLibrary.Task;

namespace ScrumBoardLibrary.Column
{
    public interface IColumn
    {
        public string ColumnName { get; set; }
        public string UnicalID { get; }
        public void AddTask(ITask task);
        public bool DeleteTask(string taskUnicalID);
        public ITask? GetTask(string taskUnicalID);
        public List<ITask> LookAllTasks();
    }
}
