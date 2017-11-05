using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using UWPMVVMLib;

namespace Task_Manager_UWP.ViewModel
{
    public class TaskDialogVm : ViewModelBase
    {
        private int _taskType;
        private bool _incorrect;
        private string _errorMessage;
        private int _donePoints;
        private int _allPoints;
        private Brush _allPointsBorderBrush;
        private Brush _donePointsBorderBrush;

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

        public bool Incorrect
        {
            get => _incorrect;
            set
            {
                _incorrect = value;
                OnPropertyChanged();
                OnPropertyChanged("IsPrimaryBtnEnabled");
            }
        }

        public bool IsPrimaryBtnEnabled => !Incorrect;

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public Brush AllPointsBorderBrush
        {
            get => _allPointsBorderBrush;
            set { _allPointsBorderBrush = value; OnPropertyChanged(); }
        }

        public Brush DonePointsBorderBrush
        {
            get => _donePointsBorderBrush;
            set { _donePointsBorderBrush = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> TaskTypes => new ObservableCollection<string>(new[] { "Simple", "Progress" });

        public string TaskName { get; set; }

        public string Description { get; set; }

        public string PointName { get; set; }

        public int DonePoints
        {
            get => _donePoints;
            set
            {
                _donePoints = value;
                ValidatePoints();
                OnPropertyChanged();
            }
        }

        public int AllPoints
        {
            get => _allPoints;
            set
            {
                _allPoints = value;
                ValidatePoints(false);
                OnPropertyChanged();
            }
        }

        public void ValidatePoints(bool isDonePoints = true)
        {
            if (_donePoints > _allPoints && _taskType == 1)
            {
                Incorrect = true;
                if (isDonePoints)
                {
                    DonePointsBorderBrush = new SolidColorBrush(Colors.Red);
                    ErrorMessage = "Done points should be smaller than all points!";
                }
                else
                {
                    AllPointsBorderBrush = new SolidColorBrush(Colors.Red);
                    ErrorMessage = "All points should be greater than done points!";
                }
            }
            else
            {
                Incorrect = false;
                DonePointsBorderBrush = new SolidColorBrush(Colors.LightGray);
                AllPointsBorderBrush = new SolidColorBrush(Colors.LightGray);
            }
        }

        public TaskDialogVm()
        {
            DonePointsBorderBrush = new SolidColorBrush(Colors.LightGray);
            AllPointsBorderBrush = new SolidColorBrush(Colors.LightGray);
        }
    }
}
