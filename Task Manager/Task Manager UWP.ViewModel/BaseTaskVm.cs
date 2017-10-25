using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPMVVMLib;
using Task = Task_Manager_UWP.Model.Task;

namespace Task_Manager_UWP.ViewModel
{
    public class BaseTaskVm : ViewModelBase
    {
        protected Task _task;

        public Task GetTask => _task;

        public BaseTaskVm(Task task)
        {
            _task = task;
        }
    }
}
