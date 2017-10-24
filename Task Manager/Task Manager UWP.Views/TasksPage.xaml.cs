using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Task_Manager_UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TasksPage : Page
    {
        private object _draggingElementVm;

        public TasksPage()
        {
            this.InitializeComponent();
        }

        private void Task_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Move;
        }

        private void Task_OnDrop(object sender, DragEventArgs e)
        {
            if (sender is UserControl s)
            {
                ItemsControl.ItemsSource = Swap(ItemsControl.ItemsSource as ObservableCollection<object>, s.DataContext,
                    _draggingElementVm);
            }
        }

        private void Task_DragStarting(UIElement sender, DragStartingEventArgs args)
        {
            if (sender is UserControl s) _draggingElementVm = s.DataContext;
        }

        private ObservableCollection<object> Swap(ObservableCollection<object> enumerable, object A, object B)
        {
            var res = from str in enumerable select str == A ? B : str == B ? A : str;
            return new ObservableCollection<object>(res);
        }
    }
}
