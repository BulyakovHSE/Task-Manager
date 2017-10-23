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
        public DataTemplate TasksPageTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            Type type = item.GetType();
            if (type.Name == "SimpleTaskVm") return TasksPageTemplate;
            return TasksPageTemplate;
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return SelectTemplateCore(item);
        }
    }
}
