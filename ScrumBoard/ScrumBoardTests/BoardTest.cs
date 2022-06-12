using Xunit;
using ScrumBoardLibrary.Task;
using ScrumBoardLibrary.Column;
using ScrumBoardLibrary.Board;
using System;
using System.Collections.Generic;

namespace ScrumBoardTests
{
    public class BoardTest
    {
        [Fact]
        public void CreateBoard_WithoutColumns()
        {
            string boardName = "Activity board";

            IBoard newBoard = new Board(boardName);

            Assert.Equal(boardName, newBoard.BoardName);
            Assert.Empty(newBoard.LookAllColumn());
        }

        [Fact]
        public void AddColumnInBoard_ColumnWillBeAdded()
        {
            string boardName = "Activity board";
            string columnName = "Studies";
            IBoard newBoard = new Board(boardName);
            IColumn newcolumn = new Column(columnName);

            newBoard.AddColumn(newcolumn);

            Assert.Equal(newcolumn, newBoard.LookAllColumn()[0]);
        }

        [Fact]
        public void AddTaskIntoColumn_AddedTask()
        {
            string taskName = "History";
            string taskDescription = "Write a report, learn definitions";
            ITask newTask = new Task(taskName, taskDescription, Priority.NORMAL);
            string boardName = "Activity board";
            string columnName = "Studies";
            IBoard newBoard = new Board(boardName);
            IColumn newcolumn = new Column(columnName);
            newBoard.AddColumn(newcolumn);

            newBoard.AddTaskIntoColumn(newTask);

            Assert.Equal(newTask, newcolumn.LookAllTasks()[0]);
        }

        [Fact]
        public void GetTaskFromBoard_ReturnFindTask()
        {
            string taskName = "History";
            string taskDescription = "Write a report, learn definitions";
            ITask newTask = new Task(taskName, taskDescription, Priority.NORMAL);
            string boardName = "Activity board";
            string columnName = "Studies";
            IBoard newBoard = new Board(boardName);
            IColumn newcolumn = new Column(columnName);
            newBoard.AddColumn(newcolumn);
            newBoard.AddTaskIntoColumn(newTask);

            ITask findTask = newBoard.GetTask(newTask.UnicalID);

            Assert.Equal(newTask, findTask);
        }

        [Fact]
        public void GetColumnFromBoard_ReturnFindColumn()
        {
            string boardName = "Activity board";
            string columnName = "Studies";
            IBoard newBoard = new Board(boardName);
            IColumn newcolumn = new Column(columnName);
            newBoard.AddColumn(newcolumn);

            IColumn findColumn = newBoard.GetColumn(newcolumn.UnicalID);

            Assert.Equal(newcolumn, findColumn);
        }

        [Fact]
        public void AddTaskInDefiniteColumn_AddedTask()
        {
            string taskName = "History";
            string taskDescription = "Write a report, learn definitions";
            ITask newTask = new Task(taskName, taskDescription, Priority.NORMAL);
            string boardName = "Activity board";
            IBoard newBoard = new Board(boardName);
            string firstColumnName = "Studies";
            IColumn firstColumn = new Column(firstColumnName);
            string secondColumnName = "Home task";
            IColumn secondColumn = new Column(secondColumnName);

            newBoard.AddColumn(firstColumn);
            newBoard.AddColumn(secondColumn);

            newBoard.AddTaskIntoColumn(newTask, 0);

            Assert.Equal(newTask, firstColumn.LookAllTasks()[0]);
        }

        [Fact]
        public void GetAllColumnFromBoard_ReturnAllColumn()
        {
            string boardName = "Activity board";
            IBoard newBoard = new Board(boardName);
            string firstColumnName = "Studies";
            IColumn firstColumn = new Column(firstColumnName);
            string secondColumnName = "Home task";
            IColumn secondColumn = new Column(secondColumnName);
            newBoard.AddColumn(firstColumn);
            newBoard.AddColumn(secondColumn);

            List<IColumn> listOfColumn = newBoard.LookAllColumn();

            Assert.Equal(new List<IColumn>() { firstColumn, secondColumn}, listOfColumn);
        }

        [Fact]
        public void TaskTransfer_OnBoard_ColumnWillDelete()
        {
            string taskName = "History";
            string taskDescription = "Write a report, learn definitions";
            ITask newTask = new Task(taskName, taskDescription, Priority.NORMAL);
            string boardName = "Activity board";
            IBoard newBoard = new Board(boardName);
            string firstColumnName = "Studies";
            IColumn firstColumn = new Column(firstColumnName);
            string secondColumnName = "Home task";
            IColumn secondColumn = new Column(secondColumnName);
            newBoard.AddColumn(firstColumn);
            newBoard.AddColumn(secondColumn);
            newBoard.AddTaskIntoColumn(newTask);

            newBoard.MoveTaskBetweenColumns(secondColumn.UnicalID, newTask.UnicalID);

            Assert.Equal(newTask, newBoard.GetColumn(secondColumn.UnicalID).GetTask(newTask.UnicalID));
            Assert.Empty(newBoard.GetColumn(firstColumn.UnicalID).LookAllTasks());
        }

        [Fact]
        public void DeleteTaskOnBoard_DeletedTask()
        {
            string taskName = "History";
            string taskDescription = "Write a report, learn definitions";
            ITask newTask = new Task(taskName, taskDescription, Priority.NORMAL);
            string boardName = "Activity board";
            string columnName = "Studies";
            IBoard newBoard = new Board(boardName);
            IColumn newcolumn = new Column(columnName);
            newBoard.AddColumn(newcolumn);
            newBoard.AddTaskIntoColumn(newTask);

            newBoard.DeleteTask(newTask.UnicalID);

            Assert.Throws<Exception>(() => newBoard.GetTask(newTask.UnicalID));
        }

        [Fact]
        public void AddTaskInNotExistColumn_GetException()
        {
            string taskName = "History";
            string taskDescription = "Write a report, learn definitions";
            ITask newTask = new Task(taskName, taskDescription, Priority.NORMAL);
            string boardName = "Activity board";
            IBoard newBoard = new Board(boardName);

            Assert.Throws<Exception>(() => newBoard.AddTaskIntoColumn(newTask, 9));
        }

        [Fact]
        public void FindNotExistTask_GetExeption()
        {
            string boardName = "Activity board";
            IBoard newBoard = new Board(boardName);

            Assert.Throws<Exception>(() => newBoard.GetTask("123"));
        }

        [Fact]
        public void GetNotExistColumn_GetExeption()
        {
            string boardName = "Activity board";
            IBoard newBoard = new Board(boardName);

            Assert.Throws<Exception>(() => newBoard.GetColumn("123"));
        }

        [Fact]
        public void DeleteNotExistTaskOnBoard_GetException()
        {
            string boardName = "Activity board";
            IBoard newBoard = new Board(boardName);

            Assert.Throws<Exception>(() => newBoard.DeleteTask("123"));
        }

        [Fact]
        public void AddInvalidColumnInBoard_GetException()
        {
            string boardName = "Activity board";
            string columnName = "Studies";
            string invalidColumn = "Column number 11";
            IBoard newBoard = new Board(boardName);
            for (int i = 1; i <= 10; i++)
            {
                newBoard.AddColumn(new Column(columnName + i));
            }

            Assert.Throws<Exception>(() => newBoard.AddColumn(new Column(invalidColumn))
            );
        }

        [Fact]
        public void AddExistColumnInBoard_GetException()
        {
            string boardName = "Activity board";
            string columnName = "Studies";
            IBoard newBoard = new Board(boardName);
            IColumn newcolumn = new Column(columnName);
            newBoard.AddColumn(newcolumn);

            Assert.Throws<Exception>(() => newBoard.AddColumn(newcolumn));
        }
    }
}
