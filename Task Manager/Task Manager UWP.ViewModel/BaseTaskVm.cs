using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Manager_UWP.Model;
using UWPMVVMLib;
using UWPMVVMLib.Commands;

namespace Task_Manager_UWP.ViewModel
{
    public class BaseTaskVm : ViewModelBase
    {
        protected TaskOld TaskOld;

        public TaskOld GetTaskOld => TaskOld;

        public BaseTaskVm(TaskOld taskOld, TasksPageVm parentVm)
        {
            TaskOld = taskOld;
            ParentVm = parentVm;
        }

        public TasksPageVm ParentVm { get; set; }

        public SimpleCommand<object> DeleteTaskCommand => ParentVm?.DeleteTaskCommand;

        public SimpleCommand<object> EditTaskCommand => ParentVm?.EditTaskCommand;

        public static BaseTaskVm GetTaskVmFromTask(TaskOld taskOld, TasksPageVm parentVm)
        {
            if (taskOld.TaskType == TaskTypeEnum.Simple) return new SimpleTaskVm(taskOld, parentVm);
            if (taskOld.TaskType == TaskTypeEnum.Progress) return new ProgressTaskVm(taskOld, parentVm);
            return null;
        }

        public override bool Equals(object obj)
        {
            if (obj is BaseTaskVm t) return TaskOld.Equals(t.TaskOld);
            return false;
        }

        public override int GetHashCode()
        {
            return TaskOld.GetHashCode();
        }
    }
}
