using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DTO
{
    public class DatesForShiftsDTO : INotifyPropertyChanged
    {
        private string day1;
        public string Day1
        {
            get { return day1; }
            set
            {
                day1 = value;
                OnPropertyChanged("Day1");
            }
        }

        private string day2;
        public string Day2
        {
            get { return day2; }
            set
            {
                day2 = value;
                OnPropertyChanged("Day2");
            }
        }

        private string day3;
        public string Day3
        {
            get { return day3; }
            set
            {
                day3 = value;
                OnPropertyChanged("Day3");
            }
        }

        private string day4;
        public string Day4
        {
            get { return day4; }
            set
            {
                day4 = value;
                OnPropertyChanged("Day4");
            }
        }

        private string day5;
        public string Day5
        {
            get { return day5; }
            set
            {
                day5 = value;
                OnPropertyChanged("Day5");
            }
        }

        private string day6;
        public string Day6
        {
            get { return day6; }
            set
            {
                day6 = value;
                OnPropertyChanged("Day6");
            }
        }

        private string day7;
        public string Day7
        {
            get { return day7; }
            set
            {
                day7 = value;
                OnPropertyChanged("Day7");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
