using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Task_Manager_UWP.Model;
using Task_Manager_UWP.Views;
using UWPMVVMLib;
using UWPMVVMLib.Commands;
using Task = Task_Manager_UWP.Model.Task;

namespace Task_Manager_UWP.ViewModel
{
    public class TasksPageVm : ViewModelBase
    {
        private ObservableCollection<object> _tasksVmCollection;
        private ObservableCollection<object> _searchTaskVmCollection;
        private string _searchText;
        private bool _searchMode;
        private bool _boolValue;
        private int _sortNameClickCount;
        private int _sortDescriptionClickCount;
        private int _sortProgressClickCount;

        public ObservableCollection<object> TasksVmCollection
        {
            get => SearchMode ? _searchTaskVmCollection : _tasksVmCollection;
            set
            {
                if (SearchMode) _searchTaskVmCollection = value;
                else _tasksVmCollection = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand AddTaskCommand => new DelegateCommand(async () =>
        {
            var add = new TaskDialog { Title = "Add a task", PrimaryButtonText = "Add", DataContext = new TaskDialogVm() };
            add.PrimaryButtonCommand = new DelegateCommand(() =>
            {
                if (add.DataContext is TaskDialogVm vm)
                {
                    var task = new Task
                    {
                        GetTaskType = (TaskType)vm.TaskType,
                        Name = vm.TaskName,
                        Description = vm.Description,
                        PointName = vm.PointName,
                        DonePoints = vm.DonePoints,
                        AllPointsCount = vm.AllPoints
                    };
                    if (SearchMode)
                    {
                        _tasksVmCollection.Add(BaseTaskVm.GetTaskVmFromTask(task, this));
                        UpdateSearchResults();
                    }
                    else
                    {
                        TasksVmCollection.Add(BaseTaskVm.GetTaskVmFromTask(task, this));
                    }
                }
            });
            await add.ShowAsync();
        });

        public SimpleCommand<object> EditTaskCommand => new SimpleCommand<object>(async item =>
        {
            var vm = (item as MenuFlyoutItem)?.DataContext;
            var editingTask = (vm as BaseTaskVm)?.GetTask;
            var edit = new TaskDialog
            {
                Title = "Edit a task",
                PrimaryButtonText = "Apply",
                DataContext = new TaskDialogVm
                {
                    TaskName = editingTask?.Name,
                    Description = editingTask?.Description,
                    TaskType = (int)editingTask.GetTaskType,
                    DonePoints = editingTask.DonePoints,
                    AllPoints = editingTask.AllPointsCount,
                    PointName = editingTask.PointName
                }
            };
            edit.PrimaryButtonCommand = new DelegateCommand(() =>
            {
                if (edit.DataContext is TaskDialogVm editVm)
                {
                    var task = new Task
                    {
                        GetTaskType = (TaskType)editVm.TaskType,
                        Name = editVm.TaskName,
                        Description = editVm.Description,
                        PointName = editVm.PointName,
                        DonePoints = editVm.DonePoints,
                        AllPointsCount = editVm.AllPoints
                    };
                    var oldIndex = _tasksVmCollection.IndexOf(vm);
                    if (SearchMode)
                    {
                        if (_tasksVmCollection.Remove(vm))
                        {
                            _tasksVmCollection.Add(BaseTaskVm.GetTaskVmFromTask(task, this));
                            _tasksVmCollection.Move(_tasksVmCollection.Count - 1, oldIndex);
                        }
                        UpdateSearchResults();
                    }
                    else if (TasksVmCollection.Remove(vm))
                    {
                        TasksVmCollection.Add(BaseTaskVm.GetTaskVmFromTask(task, this));
                        _tasksVmCollection.Move(_tasksVmCollection.Count - 1, oldIndex);
                    }
                }
            });
            await edit.ShowAsync();
        });

        public SimpleCommand<object> DeleteTaskCommand => new SimpleCommand<object>(item =>
        {
            var vm = (item as MenuFlyoutItem)?.DataContext;
            if (SearchMode)
            {
                _tasksVmCollection.Remove(vm);
                UpdateSearchResults();
            }
            else TasksVmCollection.Remove(vm);
        });

        public DelegateCommand SortByNameCommand => new DelegateCommand(() =>
        {
            if (!SearchMode)
            {
                var tasks = from taskVm in _tasksVmCollection select (taskVm as BaseTaskVm)?.GetTask;
                tasks = Task.OrderByName(tasks, _sortNameClickCount % 2 == 1);
                TasksVmCollection =
                    new ObservableCollection<object>(from task in tasks
                        select (object) BaseTaskVm.GetTaskVmFromTask(task, this));
                OnPropertyChanged("IsSortByNameHighToLow");
                _sortNameClickCount++;
                _sortDescriptionClickCount = 0;
                _sortProgressClickCount = 0;
                OnPropertyChanged("IsSortByDescriptionHighToLow");
                OnPropertyChanged("IsSortByProgressHighToLow");
            }
        });

        public bool IsSortByNameHighToLow => _sortNameClickCount % 2 == 0;

        public DelegateCommand SortByDescriptionCommand => new DelegateCommand(() =>
        {
            if (!SearchMode)
            {
                var tasks = from taskVm in _tasksVmCollection select (taskVm as BaseTaskVm)?.GetTask;
                tasks = Task.OrderByDescription(tasks, _sortDescriptionClickCount % 2 == 1);
                TasksVmCollection =
                    new ObservableCollection<object>(from task in tasks
                        select (object)BaseTaskVm.GetTaskVmFromTask(task, this));
                OnPropertyChanged("IsSortByDescriptionHighToLow");
                _sortNameClickCount = 0;
                _sortDescriptionClickCount++;
                _sortProgressClickCount = 0;
                OnPropertyChanged("IsSortByProgressHighToLow");
                OnPropertyChanged("IsSortByNameHighToLow");
            }
        });

        public bool IsSortByDescriptionHighToLow => _sortDescriptionClickCount % 2 == 0;

        public DelegateCommand SortByProgressCommand => new DelegateCommand(() =>
        {
            if (!SearchMode)
            {
                var tasks = from taskVm in _tasksVmCollection select (taskVm as BaseTaskVm)?.GetTask;
                tasks = Task.OrderByTaskProgress(tasks, _sortProgressClickCount % 2 == 1);
                TasksVmCollection =
                    new ObservableCollection<object>(from task in tasks
                        select (object)BaseTaskVm.GetTaskVmFromTask(task, this));
                OnPropertyChanged("IsSortByProgressHighToLow");
                _sortNameClickCount = 0;
                _sortDescriptionClickCount++;
                _sortProgressClickCount = 0;
                OnPropertyChanged("IsSortByDescriptionHighToLow");
                OnPropertyChanged("IsSortByNameHighToLow");
            }
        });

        public bool IsSortByProgressHighToLow => _sortProgressClickCount % 2 == 0;

        public bool BoolValue
        {
            get => _boolValue;
            set { _boolValue = value; OnPropertyChanged(); }
        }

        private bool SearchMode
        {
            get => _searchMode;
            set { _searchMode = value; OnPropertyChanged("TasksVmCollection"); }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                int searchTextDelta = 1;
                if (_searchText != null)
                    searchTextDelta = value.Length - _searchText.Length;
                _searchText = value;
                bool notEmpty = !string.IsNullOrEmpty(_searchText);

                // New request
                if (notEmpty != SearchMode)
                {
                    if (notEmpty)
                    {
                        UpdateSearchResults(searchTextDelta);
                    }
                    else _searchTaskVmCollection = null;
                }
                SearchMode = notEmpty;
                if (searchTextDelta != 0 && SearchMode)
                    UpdateSearchResults(searchTextDelta);
            }
        }

        private void UpdateSearchResults(int searchDelta = 0)
        {
            var collectionForSearch = SearchMode && searchDelta > 0 ? _searchTaskVmCollection : _tasksVmCollection;
            var orderedTasks = Task.GetSatisfyingTasks(
                from taskVm in collectionForSearch select (taskVm as BaseTaskVm)?.GetTask, SearchText);
            ObservableCollection<object> col = new ObservableCollection<object>();
            foreach (var task in orderedTasks)
            {
                col.Add(BaseTaskVm.GetTaskVmFromTask(task, this));
            }
            _searchTaskVmCollection = col;
            OnPropertyChanged("TasksVmCollection");
        }

        public TasksPageVm()
        {
            _sortDescriptionClickCount = 0;
            _sortNameClickCount = 0;
            _sortProgressClickCount = 0;
        }
    }
}
