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
    public class TaskOld
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int DonePoints { get; set; }

        public int AllPointsCount { get; set; }

        public bool IsDone { get; set; }

        public string PointName { get; set; }

        public TaskTypeEnum TaskType { get; set; }

        public override int GetHashCode()
        {
            return GetStringHashCode(Name) + GetStringHashCode(Description) + DonePoints +
                   AllPointsCount + (TaskType == TaskTypeEnum.Progress ? GetStringHashCode(PointName) : 0);
        }

        public override bool Equals(object obj)
        {
            if (obj is TaskOld task) return GetHashCode() == task.GetHashCode();
            return false;
        }

        private int GetStringHashCode(string s)
        {
            int i = 0;
            return s.Sum(ch => char.ConvertToUtf32(ch.ToString(), 0) * i++);
        }

        public static IEnumerable<TaskOld> GetSatisfyingTasks(IEnumerable<TaskOld> tasks, string searchRequest)
        {
            searchRequest = searchRequest.ToLower();
            Dictionary<TaskOld, int> suitability = new Dictionary<TaskOld, int>();
            foreach (var task in tasks)
            {
                int suitable = 0;
                suitable += new Regex(searchRequest).Matches(task.Name.ToLower()).Count;
                suitable += new Regex(searchRequest).Matches(task.Description.ToLower()).Count;
                if (task.TaskType == TaskTypeEnum.Progress)
                    suitable += new Regex(searchRequest).Matches(task.PointName.ToLower()).Count;
                suitability.Add(task, suitable);
            }
            var ordered = suitability.OrderBy(val => val.Value).Reverse();
            return from keyValuePair in ordered where keyValuePair.Value > 0 select keyValuePair.Key;
        }

        public static IEnumerable<TaskOld> OrderByName(IEnumerable<TaskOld> tasks, bool inverse = false)
        {
            var ordered = tasks.OrderBy(task => task.Name);
            if (inverse) return ordered.Reverse();
            return ordered;
        }

        public static IEnumerable<TaskOld> OrderByDescription(IEnumerable<TaskOld> tasks, bool inverse = false)
        {
            var ordered = tasks.OrderBy(task => task.Description);
            if (inverse) return ordered.Reverse();
            return ordered;
        }

        public static IEnumerable<TaskOld> OrderByTaskType(IEnumerable<TaskOld> tasks, bool inverse = false)
        {
            var ordered = tasks.OrderBy(task => task.TaskType);
            if (inverse) return ordered.Reverse();
            return ordered;
        }

        public static IEnumerable<TaskOld> OrderByTaskProgress(IEnumerable<TaskOld> tasks, bool inverse = false)
        {
            var ordered = tasks.OrderBy(task => task.TaskType == TaskTypeEnum.Progress ? (double)task.DonePoints / task.AllPointsCount : BoolToByte(task.IsDone));
            if (inverse) return ordered.Reverse();
            return ordered;
        }

        private static double BoolToByte(bool value)
        {
            if (value) return 1;
            return 0;
        }
    }

    [Serializable]
    public enum TaskTypeEnum
    {
        Simple, Progress
    }
}
