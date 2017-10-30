using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Task_Manager_UWP.Views
{
    public sealed partial class TaskDialog : ContentDialog, INotifyPropertyChanged
    {
        private int _taskType;

        public TaskDialog()
        {
            this.InitializeComponent();
            DataContext = this;
        }

        public bool IsProgress => _taskType == 1;

        public int TaskType
        {
            get => _taskType;
            set
            {
                _taskType = value;
                OnPropertyChanged();
                OnPropertyChanged("IsProgress");
            }
        }

        public ObservableCollection<string> TaskTypes => new ObservableCollection<string>(new[]{"Simple", "Progress"});

        public string TaskName { get; set; }

        public string Description { get; set; }

        public string PointName { get; set; }

        public int DonePoints { get; set; }

        public int AllPoints { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
