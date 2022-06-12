using ScrumBoardLibrary.Task;
namespace ScrumBoardAPI.DTO;

public class TasksDTO
{
    public TasksDTO(ITask task)
    {
        UnicalID = task.UnicalID;
        TaskName = task.TaskName;
        TaskDescription = task.TaskDescription;
        TaskPriority = task.TaskPriority.ToString();
    }

    public string UnicalID { get; set; }
    public string TaskName { get; set; }
    public string TaskDescription { get; set; }
    public string TaskPriority { get; set; }
}
