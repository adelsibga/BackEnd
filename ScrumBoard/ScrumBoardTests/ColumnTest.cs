using Xunit;
using ScrumBoardLibrary.Task;
using ScrumBoardLibrary.Column;
using System.Collections.Generic;

namespace ScrumBoardTests
{
    public class ColumnTest
    {
        [Fact]
        public void CreateColumn_ColumnWithoutTasks()
        {
            string columnName = "Studies";
 
            IColumn newColumn = new Column(columnName);

            Assert.Equal(columnName, newColumn.ColumnName);
            Assert.Empty(newColumn.LookAllTasks());
        }

        [Fact]
        public void ChangeColumnName_ColumnWithNewName()
        {
            string newColumnName = "Student life";
            string columnName = "Studies";
            IColumn newColumn = new Column(columnName);

            newColumn.ColumnName = newColumnName;

            Assert.Equal(newColumnName, newColumn.ColumnName);
        }

        [Fact]
        public void ChangeColumnName_ColumnWithNewTask()
        {
            string columnName = "Studies";
            string taskName = "History";
            string taskDescription = "Write a report, learn definitions";
            ITask newTask = new Task(taskName, taskDescription, Priority.NORMAL);
            IColumn newColumn = new Column(columnName);

            newColumn.AddTask(newTask);

            Assert.Equal(newTask, newColumn.LookAllTasks()[0]);
        }

        [Fact]
        public void GetTaskFromColumn_ReturnNeededTask()
        {
            string columnName = "Studies";
            string taskName = "History";
            string taskDescription = "Write a report, learn definitions";
            ITask newTask = new Task(taskName, taskDescription, Priority.NORMAL);
            IColumn newColumn = new Column(columnName);
            newColumn.AddTask(newTask);

            ITask? neededTask = newColumn.GetTask(newTask.UnicalID);

            Assert.Equal(newTask, neededTask);
        }

        [Fact]
        public void DeleteTaskInColumn_DeletedTask()
        {
            string columnName = "Studies";
            string taskName = "History";
            string taskDescription = "Write a report, learn definitions";
            ITask newTask = new Task(taskName, taskDescription, Priority.NORMAL);
            IColumn newColumn = new Column(columnName);
            newColumn.AddTask(newTask);

            newColumn.DeleteTask(newTask.UnicalID);

            Assert.Null(newColumn.GetTask(newTask.UnicalID));
        }

        [Fact]
        public void LookAllTaskFromColumn_AllTask()
        {
            ITask firstTask = new Task("History", "Write a report, learn definitions", Priority.NORMAL);
            ITask secondTask = new Task("Philosophy", "Send report by e-mail", Priority.NORMAL);
            string columnName = "Studies";
            IColumn newColumn = new Column(columnName);
            newColumn.AddTask(firstTask);
            newColumn.AddTask(secondTask);

            List<ITask> listOfTask = newColumn.LookAllTasks();

            Assert.Equal(new List<ITask>() { firstTask, secondTask }, listOfTask);
        } 

        [Fact]
        public void AddTaskWithExistName_GetException()
        {
            ITask firstTask = new Task("History", "Write a report, learn definitions", Priority.NORMAL);
            ITask secondTask = new Task("History", "Write a report, learn definitions", Priority.NORMAL);
            string columnName = "Studies";
            IColumn newColumn = new Column(columnName);
            newColumn.AddTask(firstTask);

            newColumn.AddTask(secondTask);

            Assert.Throws<System.Exception>(() => newColumn.AddTask(secondTask));
        }
    }
}
