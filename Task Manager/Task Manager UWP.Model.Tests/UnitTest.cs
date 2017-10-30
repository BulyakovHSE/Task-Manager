
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task = Task_Manager_UWP.Model.Task;

namespace Task_Manager_UWP.Model.Tests
{
    [TestClass]
    public class TaskTests
    {
        [TestMethod]
        public void Tasks_NotEqual()
        {
            Task t1 = new Task{Name = "1", Description = "1", DonePoints = 0, GetTaskType = TaskType.Progress, AllPointsCount = 5, PointName = "0"};
            Task t2 = new Task { Name = "2", Description = "2", DonePoints = 3, GetTaskType = TaskType.Progress, AllPointsCount = 5, PointName = "0" };

            Assert.IsFalse(t1.Equals(t2));
        }

        [TestMethod]
        public void Tasks_Equal()
        {
            Task t1 = new Task { Name = "1", Description = "1", DonePoints = 0, GetTaskType = TaskType.Progress, AllPointsCount = 5, PointName = "0" };
            Task t2 = new Task { Name = "1", Description = "1", DonePoints = 0, GetTaskType = TaskType.Progress, AllPointsCount = 5, PointName = "0" };

            Assert.IsTrue(t1.Equals(t2));
        }
    }
}
