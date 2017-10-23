using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using Task_Manager_UWP.ViewModel;
using Task_Manager_UWP.Views;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Task_Manager_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private TasksPageVm _tasksPage;
        private SettingsPageVm _settingsPage;

        public MainPage()
        {
            this.InitializeComponent();
            _tasksPage = new TasksPageVm();
            _settingsPage = new SettingsPageVm();
        }

        private void HumburgerMenuBtn_OnClick(object sender, RoutedEventArgs e)
        {
            SplitView.IsPaneOpen = !SplitView.IsPaneOpen;
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is MainViewVm m)
            {
                m.ListBoxSelectionChangedCommand.Execute((sender as ListBox)?.SelectedIndex);
            }
        }
    }
}
