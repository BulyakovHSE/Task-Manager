using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Task_Manager_UWP.Views;
using Task_Manager_UWP.Model;
using UWPMVVMLib;
using UWPMVVMLib.Commands;
using Task = Task_Manager_UWP.Model.Task;

namespace Task_Manager_UWP.ViewModel
{
    public class MainViewVm : ViewModelBase
    {
        private string _searchText;
        private Page _currentPage;
        private TasksPageVm _tasksPage;
        private SettingsPageVm _settingsPage;

        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(); }
        }

        public DelegateCommand SearchCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    throw new NotImplementedException();
                });
            }
        }

        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
                OnPropertyChanged("CurrentPageName");
            }
        }

        public string CurrentPageName => _currentPage?.Name;

        public SimpleCommand<int> ListBoxSelectionChangedCommand
        {
            get
            {
                return new SimpleCommand<int>(id =>
                {
                    switch (id)
                    {
                        case 0: CurrentPage = new TasksPage { DataContext = _tasksPage }; break;
                        case 1: CurrentPage = new SettingsPage { DataContext = _settingsPage }; break;
                        default: throw new NotImplementedException();
                    }
                });
            }
        }

        public MainViewVm()
        {
            ObservableCollection<object> col = new ObservableCollection<object>();
            foreach (var task in TasksListMock)
            {
                if(task.GetTaskType== TaskType.Simple) col.Add(new SimpleTaskVm(task));
                else if(task.GetTaskType == TaskType.Progress) col.Add(new ProgressTaskVm(task));
            }
                _tasksPage = new TasksPageVm() {TasksVmCollection = col};
            _settingsPage = new SettingsPageVm();
        }

        public List<Task> TasksListMock
        {
            get => new List<Task>
            {
                new Task(TaskType.Simple){Name = "1", Description = "Первая задача", IsDone = false},
                new Task(TaskType.Simple){Name = "2", Description = "Вторая задача", IsDone = false},
                new Task(TaskType.Progress){Name = "4", Description = "Выучить", IsDone = false, DonePoints = 0, AllPointsCount = 5, PointName = "Слов"},
                new Task(TaskType.Progress){Name = "5", Description = "Прочесть", IsDone = false, DonePoints = 3, AllPointsCount = 10, PointName = "Книг"},
                new Task(TaskType.Simple){Name = "3", Description = "Третья задача.  очень длинная задача "
                    +"авмоадивалаоллллллллллллллллллллллллллллллавоалымодвлаоидавомитавилаотилавтилавргкрмвоитлаито. Конец", IsDone = false}
            };
        }
    }
}
