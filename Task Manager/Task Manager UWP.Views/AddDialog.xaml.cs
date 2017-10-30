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
    public sealed partial class AddDialog : ContentDialog, INotifyPropertyChanged
    {
        private bool _isProgress;

        public AddDialog()
        {
            this.InitializeComponent();
            DataContext = this;
        }

        public bool IsProgress
        {
            get => _isProgress;
            set { _isProgress = value; OnPropertyChanged(); }
        }

        public int TaskType { get; set; }

        public ObservableCollection<string> TaskTypes => new ObservableCollection<string>(new[]{"Simple", "Progress"});

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = (sender as ComboBox);
            TaskType = combo?.SelectedIndex ?? 0;
            switch (TaskType)
            {
                case 1: IsProgress = true; break;
                default: IsProgress = false; break;
            } 
        }

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
