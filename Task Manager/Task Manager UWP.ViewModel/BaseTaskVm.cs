using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Manager.Model;
using UWPMVVMLib;
using UWPMVVMLib.Commands;
using Task = Task_Manager.Model.Task;

namespace Task_Manager_UWP.ViewModel
{
    public class BaseTaskVm : ViewModelBase
    {
        protected Task Task;

        public Task GetTask => Task;

        public BaseTaskVm(Task task, TasksPageVm parentVm)
        {
            Task = task;
            ParentVm = parentVm;
        }

        public TasksPageVm ParentVm { get; set; }

        public SimpleCommand<object> DeleteTaskCommand => ParentVm?.DeleteTaskCommand;

        public SimpleCommand<object> EditTaskCommand => ParentVm?.EditTaskCommand;

        public static BaseTaskVm GetTaskVmFromTask(Task task, TasksPageVm parentVm)
        {
            if (task.TaskType == TaskTypeEnum.Simple) return new SimpleTaskVm(task, parentVm);
            if (task.TaskType == TaskTypeEnum.Progress) return new ProgressTaskVm(task, parentVm);
            return null;
        }

        public override bool Equals(object obj)
        {
            if (obj is BaseTaskVm t) return Task.Equals(t.Task);
            return false;
        }

        public override int GetHashCode()
        {
            return Task.GetHashCode();
        }
    }
}
