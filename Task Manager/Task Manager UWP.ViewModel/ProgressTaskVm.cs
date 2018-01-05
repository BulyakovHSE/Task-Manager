using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Manager_UWP.Model;
using UWPMVVMLib;
using UWPMVVMLib.Commands;
using Task = Task_Manager.Model.Task;

namespace Task_Manager_UWP.ViewModel
{
    public class ProgressTaskVm : BaseTaskVm
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

        public int DonePoints
        {
            get => Task.DonePoints;
            set { Task.DonePoints = value; OnPropertyChanged(); }
        }

        public int AllPointsCount => Task.AllPointsCount;

        public string PointName
        {
            get => Task.PointName;
            set { Task.PointName = value; OnPropertyChanged(); }
        }

        public DelegateCommand DecreaseCommand => new DelegateCommand(() =>
        {
            if (Task.DonePoints > 0)
                DonePoints = DonePoints - 1;
        });

        public DelegateCommand IncreaseCommand => new DelegateCommand(() =>
        {
            if (Task.DonePoints < Task.AllPointsCount)
                DonePoints = DonePoints + 1;
        });

        public DelegateCommand CompleteCommand => new DelegateCommand(() =>
        {
            DonePoints = Task.AllPointsCount;
        });

        public ProgressTaskVm(Task task, TasksPageVm parentVm) : base(task, parentVm) { }
    }
}
