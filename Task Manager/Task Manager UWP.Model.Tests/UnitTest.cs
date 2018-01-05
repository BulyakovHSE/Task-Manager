
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Task_Manager_UWP.Model.Tests
{
    [TestClass]
    public class TaskTests
    {
        [TestMethod]
        public void Tasks_NotEqual()
        {
            Task t1 = new Task { Name = "1", Description = "1", DonePoints = 0, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0" };
            Task t2 = new Task { Name = "2", Description = "2", DonePoints = 3, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0" };

            Assert.IsFalse(t1.Equals(t2));
        }

        [TestMethod]
        public void Tasks_Equal()
        {
            Task t1 = new Task { Name = "1", Description = "1", DonePoints = 0, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0" };
            Task t2 = new Task { Name = "1", Description = "1", DonePoints = 0, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0" };

            Assert.IsTrue(t1.Equals(t2));
        }

        [TestMethod]
        public void TasksEnum_OrderingByName_LowToHigh()
        {
            List<Task> tasks = new List<Task>
            {
                new Task{Name = "abc", Description = "1", DonePoints = 0, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0"},
                new Task { Name = "cba", Description = "2", DonePoints = 3, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0" }
            };
            Assert.AreEqual(tasks[1], Task.OrderByName(tasks, true).ToList()[0]);
        }

        [TestMethod]
        public void TasksEnum_OrderingByName_HighToLow()
        {
            List<Task> tasks = new List<Task>
            {
                new Task{Name = "cba", Description = "1", DonePoints = 0, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0"},
                new Task { Name = "abc", Description = "2", DonePoints = 3, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0" }
            };
            Assert.AreEqual(tasks[1], Task.OrderByName(tasks).ToList()[0]);
        }

        [TestMethod]
        public void TasksEnum_OrderByDescription_LowToHigh()
        {
            List<Task> tasks = new List<Task>
            {
                new Task{Name = "abc", Description = "abc", DonePoints = 0, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0"},
                new Task { Name = "cba", Description = "cba", DonePoints = 3, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0" }
            };
            Assert.AreEqual(tasks[1], Task.OrderByDescription(tasks, true).ToList()[0]);
        }

        [TestMethod]
        public void TasksEnum_OrderingByDescription_HighToLow()
        {
            List<Task> tasks = new List<Task>
            {
                new Task{Name = "cba", Description = "cba", DonePoints = 0, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0"},
                new Task { Name = "abc", Description = "abc", DonePoints = 3, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0" }
            };
            Assert.AreEqual(tasks[1], Task.OrderByDescription(tasks).ToList()[0]);
        }

        [TestMethod]
        public void TasksEnum_OrderByTaskType_LowToHigh()
        {
            List<Task> tasks = new List<Task>
            {
                new Task{Name = "abc", Description = "abc", DonePoints = 0, TaskType = TaskTypeEnum.Simple, AllPointsCount = 5, PointName = "0"},
                new Task { Name = "cba", Description = "cba", DonePoints = 3, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0" }
            };
            Assert.AreEqual(tasks[1], Task.OrderByTaskType(tasks, true).ToList()[0]);
        }

        [TestMethod]
        public void TasksEnum_OrderingByTaskType_HighToLow()
        {
            List<Task> tasks = new List<Task>
            {
                new Task{Name = "cba", Description = "cba", DonePoints = 0, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0"},
                new Task { Name = "abc", Description = "abc", DonePoints = 3, TaskType = TaskTypeEnum.Simple, AllPointsCount = 5, PointName = "0" }
            };
            Assert.AreEqual(tasks[1], Task.OrderByTaskType(tasks).ToList()[0]);
        }

        [TestMethod]
        public void TasksEnum_OrderByTaskProgress_LowToHigh()
        {
            List<Task> tasks = new List<Task>
            {
                new Task{Name = "abc", Description = "abc", DonePoints = 3, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0"},
                new Task { Name = "cba", Description = "cba", DonePoints = 0, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0" }
            };
            Assert.AreEqual(tasks[1], Task.OrderByTaskProgress(tasks).ToList()[0]);
        }

        [TestMethod]
        public void TasksEnum_OrderingByTaskProgress_HighToLow()
        {
            List<Task> tasks = new List<Task>
            {
                new Task{Name = "cba", Description = "cba", DonePoints = 0, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0"},
                new Task { Name = "abc", Description = "abc", DonePoints = 3, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0" }
            };
            Assert.AreEqual(tasks[1], Task.OrderByTaskProgress(tasks, true).ToList()[0]);
        }
    }
}
