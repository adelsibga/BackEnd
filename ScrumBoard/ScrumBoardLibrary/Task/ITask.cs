
namespace ScrumBoardLibrary.Task

{
    public enum Priority
    {
        HIGH,
        NORMAL,
        LOW,
    }
    public interface ITask
    {
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public Priority TaskPriority { get; set; }
        public string UnicalID { get; }
    }
}
