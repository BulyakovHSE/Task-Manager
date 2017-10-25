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
    public class MainPageVm : ViewModelBase
    {
        private string _searchText;
        private Page _currentPage;
        private TasksPageVm _tasksPage;
        private SettingsPageVm _settingsPage;

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                _tasksPage.SearchText = value;
                OnPropertyChanged(); 
            }
        }

        public DelegateCommand SearchCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    //throw new NotImplementedException();
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

        public MainPageVm()
        {
            _settingsPage = new SettingsPageVm();
        }

        ~MainPageVm()
        {
            SaveTasks();
        }

        public void SaveTasks()
        {
            var taskVmCollection = _tasksPage.TasksVmCollection;
            var taskCollection = from task in taskVmCollection select (task as BaseTaskVm)?.GetTask;
            DataProvider<Task>.Serialize(taskCollection);
        }

        public async void LoadTasks()
        {
            ObservableCollection<object> col = new ObservableCollection<object>();
            var taskenum = await DataProvider<Task>.Deserialize();
            foreach (var task in taskenum)
            {
                if (task.GetTaskType == TaskType.Simple) col.Add(new SimpleTaskVm(task));
                else if (task.GetTaskType == TaskType.Progress) col.Add(new ProgressTaskVm(task));
            }
            _tasksPage = new TasksPageVm() { TasksVmCollection = col };
        }

        public List<Task> TasksListMock => new List<Task>
        {
            new Task{Name = "1", Description = "Первая задача", IsDone = false, GetTaskType = TaskType.Simple},
            new Task{Name = "2", Description = "Вторая задача", IsDone = false, GetTaskType = TaskType.Simple},
            new Task{Name = "4", Description = "Выучить", IsDone = false, DonePoints = 0, AllPointsCount = 5, PointName = "Слов", GetTaskType = TaskType.Progress},
            new Task{Name = "5", Description = "Прочесть", IsDone = false, DonePoints = 3, AllPointsCount = 10, PointName = "Книг", GetTaskType = TaskType.Progress},
            new Task{Name = "3", Description = "Третья задача.  очень длинная задача "
                                               +"авмоадивалаоллллллллллллллллллллллллллллллавоалымодвлаоидавомитавилаотилавтилавргкрмвоитлаито. Конец", IsDone = false, GetTaskType = TaskType.Simple}
        };
    }
}
