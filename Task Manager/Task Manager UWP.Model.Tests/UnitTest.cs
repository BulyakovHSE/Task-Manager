
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
            TaskOld t1 = new TaskOld { Name = "1", Description = "1", DonePoints = 0, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0" };
            TaskOld t2 = new TaskOld { Name = "2", Description = "2", DonePoints = 3, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0" };

            Assert.IsFalse(t1.Equals(t2));
        }

        [TestMethod]
        public void Tasks_Equal()
        {
            TaskOld t1 = new TaskOld { Name = "1", Description = "1", DonePoints = 0, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0" };
            TaskOld t2 = new TaskOld { Name = "1", Description = "1", DonePoints = 0, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0" };

            Assert.IsTrue(t1.Equals(t2));
        }

        [TestMethod]
        public void TasksEnum_OrderingByName_LowToHigh()
        {
            List<TaskOld> tasks = new List<TaskOld>
            {
                new TaskOld{Name = "abc", Description = "1", DonePoints = 0, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0"},
                new TaskOld { Name = "cba", Description = "2", DonePoints = 3, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0" }
            };
            Assert.AreEqual(tasks[1], TaskOld.OrderByName(tasks, true).ToList()[0]);
        }

        [TestMethod]
        public void TasksEnum_OrderingByName_HighToLow()
        {
            List<TaskOld> tasks = new List<TaskOld>
            {
                new TaskOld{Name = "cba", Description = "1", DonePoints = 0, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0"},
                new TaskOld { Name = "abc", Description = "2", DonePoints = 3, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0" }
            };
            Assert.AreEqual(tasks[1], TaskOld.OrderByName(tasks).ToList()[0]);
        }

        [TestMethod]
        public void TasksEnum_OrderByDescription_LowToHigh()
        {
            List<TaskOld> tasks = new List<TaskOld>
            {
                new TaskOld{Name = "abc", Description = "abc", DonePoints = 0, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0"},
                new TaskOld { Name = "cba", Description = "cba", DonePoints = 3, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0" }
            };
            Assert.AreEqual(tasks[1], TaskOld.OrderByDescription(tasks, true).ToList()[0]);
        }

        [TestMethod]
        public void TasksEnum_OrderingByDescription_HighToLow()
        {
            List<TaskOld> tasks = new List<TaskOld>
            {
                new TaskOld{Name = "cba", Description = "cba", DonePoints = 0, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0"},
                new TaskOld { Name = "abc", Description = "abc", DonePoints = 3, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0" }
            };
            Assert.AreEqual(tasks[1], TaskOld.OrderByDescription(tasks).ToList()[0]);
        }

        [TestMethod]
        public void TasksEnum_OrderByTaskType_LowToHigh()
        {
            List<TaskOld> tasks = new List<TaskOld>
            {
                new TaskOld{Name = "abc", Description = "abc", DonePoints = 0, TaskType = TaskTypeEnum.Simple, AllPointsCount = 5, PointName = "0"},
                new TaskOld { Name = "cba", Description = "cba", DonePoints = 3, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0" }
            };
            Assert.AreEqual(tasks[1], TaskOld.OrderByTaskType(tasks, true).ToList()[0]);
        }

        [TestMethod]
        public void TasksEnum_OrderingByTaskType_HighToLow()
        {
            List<TaskOld> tasks = new List<TaskOld>
            {
                new TaskOld{Name = "cba", Description = "cba", DonePoints = 0, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0"},
                new TaskOld { Name = "abc", Description = "abc", DonePoints = 3, TaskType = TaskTypeEnum.Simple, AllPointsCount = 5, PointName = "0" }
            };
            Assert.AreEqual(tasks[1], TaskOld.OrderByTaskType(tasks).ToList()[0]);
        }

        [TestMethod]
        public void TasksEnum_OrderByTaskProgress_LowToHigh()
        {
            List<TaskOld> tasks = new List<TaskOld>
            {
                new TaskOld{Name = "abc", Description = "abc", DonePoints = 3, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0"},
                new TaskOld { Name = "cba", Description = "cba", DonePoints = 0, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0" }
            };
            Assert.AreEqual(tasks[1], TaskOld.OrderByTaskProgress(tasks).ToList()[0]);
        }

        [TestMethod]
        public void TasksEnum_OrderingByTaskProgress_HighToLow()
        {
            List<TaskOld> tasks = new List<TaskOld>
            {
                new TaskOld{Name = "cba", Description = "cba", DonePoints = 0, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0"},
                new TaskOld { Name = "abc", Description = "abc", DonePoints = 3, TaskType = TaskTypeEnum.Progress, AllPointsCount = 5, PointName = "0" }
            };
            Assert.AreEqual(tasks[1], TaskOld.OrderByTaskProgress(tasks, true).ToList()[0]);
        }
    }
}
