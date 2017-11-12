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
    public class ProgressTaskVm : BaseTaskVm
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

        public int DonePoints
        {
            get => TaskOld.DonePoints;
            set { TaskOld.DonePoints = value; OnPropertyChanged(); }
        }

        public int AllPointsCount => TaskOld.AllPointsCount;

        public string PointName
        {
            get => TaskOld.PointName;
            set { TaskOld.PointName = value; OnPropertyChanged(); }
        }

        public DelegateCommand DecreaseCommand => new DelegateCommand(() =>
        {
            if (TaskOld.DonePoints > 0)
                DonePoints = DonePoints - 1;
        });

        public DelegateCommand IncreaseCommand => new DelegateCommand(() =>
        {
            if (TaskOld.DonePoints < TaskOld.AllPointsCount)
                DonePoints = DonePoints + 1;
        });

        public DelegateCommand CompleteCommand => new DelegateCommand(() =>
        {
            DonePoints = TaskOld.AllPointsCount;
        });

        public ProgressTaskVm(TaskOld taskOld, TasksPageVm parentVm) : base(taskOld, parentVm) { }
    }
}
