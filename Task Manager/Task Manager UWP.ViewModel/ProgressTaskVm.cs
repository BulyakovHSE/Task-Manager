using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPMVVMLib;
using UWPMVVMLib.Commands;
using Task = Task_Manager_UWP.Model.Task;

namespace Task_Manager_UWP.ViewModel
{
    public class ProgressTaskVm : ViewModelBase
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

        public int DonePoints
        {
            get => _task.DonePoints;
            set { _task.DonePoints = value; OnPropertyChanged(); }
        }

        public int AllPointsCount => _task.AllPointsCount;

        public string PointName
        {
            get => _task.PointName;
            set { _task.PointName = value; OnPropertyChanged(); }
        }

        public DelegateCommand DecreaseCommand => new DelegateCommand(() =>
        {
            if (_task.DonePoints > 0)
                DonePoints = DonePoints - 1;
        });

        public DelegateCommand IncreaseCommand => new DelegateCommand(() =>
        {
            if (_task.DonePoints < _task.AllPointsCount)
                DonePoints = DonePoints + 1;
        });

        public DelegateCommand CompleteCommand => new DelegateCommand(() =>
        {
            DonePoints = _task.AllPointsCount;
        });

        public Task GetTask => _task;

        public ProgressTaskVm(Task task)
        {
            _task = task;
        }
    }
}
