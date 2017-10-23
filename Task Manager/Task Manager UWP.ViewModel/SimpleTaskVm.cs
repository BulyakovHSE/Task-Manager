using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPMVVMLib;
using Task_Manager_UWP.Model;
using Task = Task_Manager_UWP.Model.Task;

namespace Task_Manager_UWP.ViewModel
{
    public class SimpleTaskVm : ViewModelBase
    {
        private Task _task;

        public string Name
        {
            get => _task.Name;
            set { _task.Name = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get => _task.Description;
            set { _task.Description = value; OnPropertyChanged(); }
        }

        public bool IsDone
        {
            get => _task.IsDone;
            set { _task.IsDone = value; OnPropertyChanged(); }
        }

        public Task GetTask => _task;

        public SimpleTaskVm(Task task)
        {
            _task = task;
        }
    }
}
