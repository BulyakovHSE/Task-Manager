using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Task_Manager_UWP.Views;
using UWPMVVMLib;
using UWPMVVMLib.Commands;

namespace Task_Manager_UWP.ViewModels
{
    public class MainViewVm : ViewModelBase
    {
        private string _searchText;
        private Type _pageType;

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

        public Type PageType
        {
            get => _pageType;
            set
            {
                _pageType = value;
                OnPropertyChanged();
                OnPropertyChanged("CurrentPageName");
            }
        }

        public string CurrentPageName  => _pageType?.Name; 

        public SimpleCommand<int> ListBoxSelectionChangedCommand
        {
            get
            {
                return new SimpleCommand<int>(id =>
                {
                    switch (id)
                    {
                        case 0: PageType = typeof(TasksPage); break;
                        case 1: PageType = typeof(SettingsPage); break;
                        default: throw new NotImplementedException();
                    }
                });
            }
        }

        public MainViewVm()
        {
            SearchText = String.Empty;
        }
    }
}
