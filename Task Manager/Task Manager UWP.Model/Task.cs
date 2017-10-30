using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager_UWP.Model
{
    [Serializable]
    public class Task
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int DonePoints { get; set; }

        public int AllPointsCount { get; set; }

        public bool IsDone { get; set; }

        public string PointName { get; set; }

        public TaskType GetTaskType { get; set; }

        public override int GetHashCode()
        {
            return GetStringHashCode(Name) + GetStringHashCode(Description) + DonePoints +
                   AllPointsCount + (GetTaskType == TaskType.Progress ? GetStringHashCode(PointName) : 0);
        }

        public override bool Equals(object obj)
        {
            if (obj is Task task) return GetHashCode() == task.GetHashCode();
            return false;
        }

        private int GetStringHashCode(string s)
        {
            int i = 0;
            return s.Sum(ch => char.ConvertToUtf32(ch.ToString(), 0) * i++);
        }
    }

    [Serializable]
    public enum TaskType
    {
        Simple, Progress
    }
}
