using Xunit;
using ScrumBoardLibrary.Task;

namespace ScrumBoardTests
{
    public class TaskTest
    {
        [Fact]
        public void CreateTask_TaskWithDescription()
        {
            string taskName = "History";
            string taskDescription = "Write a report, learn definitions";

            ITask newTask = new Task(taskName, taskDescription, Priority.NORMAL);

            Assert.Equal(taskName, newTask.TaskName);
            Assert.Equal(taskDescription, newTask.TaskDescription);
            Assert.Equal(Priority.NORMAL, newTask.TaskPriority);
        }

        [Fact]
        public void ChangeTask_TaskWithNewName()
        {
            string taskName = "History";
            string taskNameNew = "Philosophy";
            string taskDescription = "Write a report, learn definitions";
            ITask newTask = new Task(taskName, taskDescription, Priority.NORMAL);

            newTask.TaskName = taskNameNew;

            Assert.Equal(taskNameNew, newTask.TaskName);
        }

        [Fact]
        public void ChangeTask_TaskWithNewDescription()
        {
            string taskName = "History";
            string taskDescription = "Write a report, learn definitions";
            string taskDescriptionNew = "Send report by e-mail";
            ITask newTask = new Task(taskName, taskDescription, Priority.NORMAL);

            newTask.TaskDescription = taskDescriptionNew;

            Assert.Equal(taskDescriptionNew, newTask.TaskDescription);
        }

        [Fact]
        public void ChangeTask_TaskWithNewPriority()
        {
            string taskName = "History";
            string taskDescription = "Write a report, learn definitions";
            ITask newTask = new Task(taskName, taskDescription, Priority.NORMAL);

            newTask.TaskPriority = Priority.HIGH;

            Assert.Equal(Priority.HIGH, newTask.TaskPriority);
        }
    }
}
