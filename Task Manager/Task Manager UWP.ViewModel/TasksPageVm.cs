using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPMVVMLib;

namespace Task_Manager_UWP.ViewModel
{
    public class TasksPageVm : ViewModelBase
    {
        private ObservableCollection<object> _tasksVmCollection;

        public ObservableCollection<object> TasksVmCollection
        {
            get => _tasksVmCollection;
            set { _tasksVmCollection = value; OnPropertyChanged(); }
        }

    }
}
