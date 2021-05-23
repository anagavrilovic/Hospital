using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DTO
{
    public class DatesInWeeklyCalendar : INotifyPropertyChanged
    {
        private string monday;

        public string Monday
        {
            get { return monday; }
            set 
            { 
                monday = value;
                OnPropertyChanged("Monday");
            }
        }

        private string tuesday;

        public string Tuesday
        {
            get { return tuesday; }
            set 
            { 
                tuesday = value;
                OnPropertyChanged("Tuesday");
            }
        }

        private string wednesday;

        public string Wednesday
        {
            get { return wednesday; }
            set 
            { 
                wednesday = value;
                OnPropertyChanged("Wednesday");
            }
        }

        private string thursday;

        public string Thursday
        {
            get { return thursday; }
            set 
            { 
                thursday = value;
                OnPropertyChanged("Thursday");
            }
        }

        private string friday;

        public string Friday
        {
            get { return friday; }
            set
            {
                friday = value;
                OnPropertyChanged("Friday");
            }
        }

        private string saturday;

        public string Saturday
        {
            get { return saturday; }
            set 
            { 
                saturday = value;
                OnPropertyChanged("Saturday");
            }
        }

        private string sunday;

        public string Sunday
        {
            get { return sunday; }
            set 
            { 
                sunday = value;
                OnPropertyChanged("Sunday");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
