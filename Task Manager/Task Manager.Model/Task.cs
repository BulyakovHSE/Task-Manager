using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Task_Manager.Model
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public int DonePoints { get; set; }

        public int AllPointsCount { get; set; }

        public bool IsDone { get; set; }

        public string PointName { get; set; }

        [Required]
        public TaskTypeEnum TaskType { get; set; }

        public override int GetHashCode()
        {
            return GetStringHashCode(Name) + GetStringHashCode(Description) + DonePoints +
                   AllPointsCount + (TaskType == TaskTypeEnum.Progress ? GetStringHashCode(PointName) : 0);
        }

        public override bool Equals(object obj)
        {
            if (obj is Task task) return GetHashCode() == task.GetHashCode();
            return false;
        }

        private int GetStringHashCode(string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;
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
                if (task.TaskType == TaskTypeEnum.Progress)
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
            var ordered = tasks.OrderBy(task => task.TaskType);
            if (inverse) return ordered.Reverse();
            return ordered;
        }

        public static IEnumerable<Task> OrderByTaskProgress(IEnumerable<Task> tasks, bool inverse = false)
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
    public enum TaskTypeEnum
    {
        Simple, Progress
    }
}