using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Manager_UWP.Model;
using UWPMVVMLib;
using UWPMVVMLib.Commands;
using Task = Task_Manager_UWP.Model.Task;

namespace Task_Manager_UWP.ViewModel
{
    public class BaseTaskVm : ViewModelBase
    {
        protected Task _task;

        public Task GetTask => _task;

        public BaseTaskVm(Task task, TasksPageVm parentVm)
        {
            _task = task;
            ParentVm = parentVm;
        }

        public TasksPageVm ParentVm { get; set; }

        public SimpleCommand<object> DeleteTaskCommand => ParentVm?.DeleteTaskCommand;

        public static BaseTaskVm GetTaskVmFromTask(Task task, TasksPageVm parentVm)
        {
            if (task.GetTaskType == TaskType.Simple) return new SimpleTaskVm(task, parentVm);
            if (task.GetTaskType == TaskType.Progress) return new ProgressTaskVm(task, parentVm);
            return null;
        }

        public override bool Equals(object obj)
        {
            if (obj is BaseTaskVm t) return _task.Equals(t._task);
            return false;
        }

        public override int GetHashCode()
        {
            return _task.GetHashCode();
        }
    }
}
