using ScrumBoardLibrary.Column;
using ScrumBoardLibrary.Task;
namespace ScrumBoardAPI.DTO;

public class ColumnsDTO
{
    public ColumnsDTO(IColumn column)
    {
        UnicalID = column.UnicalID;
        ColumnName = column.ColumnName;

        List<TasksDTO> listTasksDTO = new List<TasksDTO>();
        List<ITask> listTasks = column.LookAllTasks();

        foreach (ITask task in listTasks)
        {
            listTasksDTO.Add(new TasksDTO(task));
        }

        Tasks = listTasksDTO;
    }

    public string UnicalID { get; set; }
    public string ColumnName { get; set; }
    public IEnumerable<TasksDTO> Tasks { get; set; }
}
