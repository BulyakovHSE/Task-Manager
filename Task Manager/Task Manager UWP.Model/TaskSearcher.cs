using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Task_Manager_UWP.Model
{
    public static class TaskSearcher
    {
        public static IEnumerable<Task> GetSatisfyingTasks(IEnumerable<Task> tasks, string searchRequest)
        {
            searchRequest = searchRequest.ToLower();
            List<Task> tasksList = new List<Task>(tasks);
            Dictionary<Task, int> suitability = new Dictionary<Task, int>();
            foreach (var task in tasksList)
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
    }
}
