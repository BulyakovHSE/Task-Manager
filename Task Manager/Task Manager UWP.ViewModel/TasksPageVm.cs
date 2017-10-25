using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Task_Manager_UWP.Model;
using Task_Manager_UWP.Views;
using UWPMVVMLib;
using UWPMVVMLib.Commands;

namespace Task_Manager_UWP.ViewModel
{
    public class TasksPageVm : ViewModelBase
    {
        private ObservableCollection<object> _tasksVmCollection;
        private ObservableCollection<object> _searchTaskVmCollection;
        private string _searchText;
        private bool _searchMode;

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
                if(_searchText!=null)
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

        private void UpdateSearchResults(int searchDelta)
        {
            var collectionForSearch = SearchMode && searchDelta > 0 ? _searchTaskVmCollection : _tasksVmCollection;
            var orderedTasks = TaskSearcher.GetSatisfyingTasks(
                from taskVm in collectionForSearch select (taskVm as BaseTaskVm)?.GetTask, SearchText);
            ObservableCollection<object> col = new ObservableCollection<object>();
            foreach (var task in orderedTasks)
            {
                if (task.GetTaskType == TaskType.Simple) col.Add(new SimpleTaskVm(task));
                else if (task.GetTaskType == TaskType.Progress) col.Add(new ProgressTaskVm(task));
            }
            _searchTaskVmCollection = col;
            OnPropertyChanged("TasksVmCollection");
        }
    }
}
