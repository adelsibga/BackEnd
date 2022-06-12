
namespace ScrumBoardLibrary.Task

{
    public class Task : ITask
    {
        public Task(string name, string description, Priority taskPriority)
        {
            TaskName = name;
            TaskDescription = description;
            TaskPriority = taskPriority;
            UnicalID = Guid.NewGuid().ToString();
        }

        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string UnicalID { get; set; }
        public Priority TaskPriority { get; set; }

    }
}
