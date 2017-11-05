using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public static IEnumerable<Task> GetSatisfyingTasks(IEnumerable<Task> tasks, string searchRequest)
        {
            searchRequest = searchRequest.ToLower();
            Dictionary<Task, int> suitability = new Dictionary<Task, int>();
            foreach (var task in tasks)
            {
                int suitable = 0;
                suitable += new Regex(searchRequest).Matches(task.Name.ToLower()).Count;
                suitable += new Regex(searchRequest).Matches(task.Description.ToLower()).Count;
                if (task.GetTaskType == TaskType.Progress)
                    suitable += new Regex(searchRequest).Matches(task.PointName.ToLower()).Count;
                suitability.Add(task, suitable);
            }
            var ordered = suitability.OrderBy(val => val.Value).Reverse();
            return from keyValuePair in ordered where keyValuePair.Value > 0 select keyValuePair.Key;
        }

        public static IEnumerable<Task> OrderByName(IEnumerable<Task> tasks, bool inverse = false)
        {
            var ordered = tasks.OrderBy(task => task.Name);
            if (inverse) return ordered.Reverse();
            return ordered;
        }

        public static IEnumerable<Task> OrderByDescription(IEnumerable<Task> tasks, bool inverse = false)
        {
            var ordered = tasks.OrderBy(task => task.Description);
            if (inverse) return ordered.Reverse();
            return ordered;
        }

        public static IEnumerable<Task> OrderByTaskType(IEnumerable<Task> tasks, bool inverse = false)
        {
            var ordered = tasks.OrderBy(task => task.GetTaskType);
            if (inverse) return ordered.Reverse();
            return ordered;
        }

        public static IEnumerable<Task> OrderByTaskProgress(IEnumerable<Task> tasks, bool inverse = false)
        {
            var ordered = tasks.OrderBy(task => task.GetTaskType == TaskType.Progress ? (double)task.DonePoints / task.AllPointsCount : BoolToByte(task.IsDone));
            if (inverse) return ordered.Reverse();
            return ordered;
        }

        private static byte BoolToByte(bool value)
        {
            if (value) return 1;
            return 0;
        }
    }

    [Serializable]
    public enum TaskType
    {
        Simple, Progress
    }
}
