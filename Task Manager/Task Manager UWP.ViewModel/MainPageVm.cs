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

namespace Task_Manager_UWP.ViewModel
{
    /// <summary>
    /// DataContext for MainPage.xaml
    /// </summary>
    public class MainPageVm : ViewModelBase
    {
        #region Fields

        private string _searchText;
        private Page _currentPage;
        private TasksPageVm _tasksPageVm;
        private SettingsPageVm _settingsPageVm;
        private bool _isHumburgerMenuOpen;

        #endregion Fields

        #region Properties

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                _tasksPageVm.SearchText = value;
                OnPropertyChanged();
            }
        }

        public bool IsHumburgerMenuOpened
        {
            get => _isHumburgerMenuOpen;
            set { _isHumburgerMenuOpen = value; OnPropertyChanged(); }
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

        public List<TaskOld> TasksListMock => new List<TaskOld>
        {
            new TaskOld{Name = "1", Description = "Первая задача", IsDone = false, TaskType = TaskTypeEnum.Simple},
            new TaskOld{Name = "2", Description = "Вторая задача", IsDone = false, TaskType = TaskTypeEnum.Simple},
            new TaskOld{Name = "4", Description = "Выучить", IsDone = false, DonePoints = 0, AllPointsCount = 5, PointName = "Слов", TaskType = TaskTypeEnum.Progress},
            new TaskOld{Name = "5", Description = "Прочесть", IsDone = false, DonePoints = 3, AllPointsCount = 10, PointName = "Книг", TaskType = TaskTypeEnum.Progress},
            new TaskOld{Name = "3", Description = "Третья задача.  очень длинная задача "
                                               +"авмоадивалаоллллллллллллллллллллллллллллллавоалымодвлаоидавомитавилаотилавтилавргкрмвоитлаито. Конец", IsDone = false, TaskType = TaskTypeEnum.Simple}
        };

        #endregion

        #region Commands

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

        public DelegateCommand HumburgerMenuOpenedChange => new DelegateCommand(() =>
        {
            IsHumburgerMenuOpened = !IsHumburgerMenuOpened;
        });

        public SimpleCommand<object> ListBoxSelectionChangedCommand
        {
            get
            {
                return new SimpleCommand<object>(id =>
                {
                    switch (id)
                    {
                        // TODO: Rewrite this shit in MVVM style
                        case 0: CurrentPage = new TasksPage { DataContext = _tasksPageVm }; break;
                        case 1: CurrentPage = new SettingsPage { DataContext = _settingsPageVm }; break;
                        default: throw new NotImplementedException();
                    }
                });
            }
        }

        #endregion
        
        public MainPageVm()
        {
            _settingsPageVm = new SettingsPageVm();
        }

        ~MainPageVm()
        {
            SaveTasks();
        }

        public void SaveTasks()
        {
            var taskVmCollection = _tasksPageVm.TasksVmCollection;
            var taskCollection = from task in taskVmCollection select (task as BaseTaskVm)?.GetTaskOld;
            DataProvider<TaskOld>.Serialize(taskCollection);
        }

        public async void LoadTasks()
        {
            _tasksPageVm = new TasksPageVm();
            ObservableCollection<object> col = new ObservableCollection<object>();
            var taskenum = await DataProvider<TaskOld>.Deserialize();
            foreach (var task in TasksListMock)
            {
                col.Add(BaseTaskVm.GetTaskVmFromTask(task, _tasksPageVm));
            }
            _tasksPageVm.TasksVmCollection = col;
        }
    }
}
