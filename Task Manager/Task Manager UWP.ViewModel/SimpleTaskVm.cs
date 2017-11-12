using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPMVVMLib;
using Task_Manager_UWP.Model;
using UWPMVVMLib.Commands;

namespace Task_Manager_UWP.ViewModel
{
    public class SimpleTaskVm : BaseTaskVm
    {
        public string Name
        {
            get => TaskOld.Name;
            set { TaskOld.Name = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get => TaskOld.Description;
            set { TaskOld.Description = value; OnPropertyChanged(); }
        }

        public bool IsDone
        {
            get => TaskOld.IsDone;
            set { TaskOld.IsDone = value; OnPropertyChanged(); }
        }

        public SimpleTaskVm(TaskOld taskOld, TasksPageVm parentVm) : base(taskOld, parentVm) { }
    }
}
