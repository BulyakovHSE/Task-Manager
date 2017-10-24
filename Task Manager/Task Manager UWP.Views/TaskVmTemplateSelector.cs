using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Task_Manager_UWP.Views
{
    public class TaskVmTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SimpleTaskTemplate { get; set; }

        public DataTemplate ProgressTaskTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            //TODO: Add code if new TaskControl developed
            Type type = item.GetType();
            if (type.Name == "SimpleTaskVm") return SimpleTaskTemplate;
            return ProgressTaskTemplate;
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return SelectTemplateCore(item);
        }
    }
}
