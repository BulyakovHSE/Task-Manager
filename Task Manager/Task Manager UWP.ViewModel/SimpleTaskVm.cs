using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPMVVMLib;
using Task_Manager_UWP.Model;
using UWPMVVMLib.Commands;
using Task = Task_Manager.Model.Task;

namespace Task_Manager_UWP.ViewModel
{
    public class SimpleTaskVm : BaseTaskVm
    {
        public string Name
        {
            get => Task.Name;
            set { Task.Name = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get => Task.Description;
            set { Task.Description = value; OnPropertyChanged(); }
        }

        public bool IsDone
        {
            get => Task.IsDone;
            set { Task.IsDone = value; OnPropertyChanged(); }
        }

        public SimpleTaskVm(Task task, TasksPageVm parentVm) : base(task, parentVm) { }
    }
}
