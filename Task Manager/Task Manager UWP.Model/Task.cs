using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager_UWP.Model
{
    [Serializable]
    public class Task
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int DonePoints { get; set; }

        public int NotDonePoints { get; set; }

        public bool IsDone { get; set; }
    }
}
