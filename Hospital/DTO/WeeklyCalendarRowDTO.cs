using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Hospital.DTO
{
    public class WeeklyCalendarRowDTO : INotifyPropertyChanged
    {
        private string timeInRow;
        private Appointment monday = new Appointment();
        private Appointment tuesday = new Appointment();
        private Appointment wednesday = new Appointment();
        private Appointment thursday = new Appointment();
        private Appointment friday = new Appointment();
        private Appointment saturday = new Appointment();
        private Appointment sunday = new Appointment();

        public string TimeInRow
        {
            get { return timeInRow; }
            set 
            { 
                timeInRow = value;
                OnPropertyChanged("Vreme");
            }
        }

        public Appointment Monday
        {
            get { return monday; }
            set 
            { 
                monday = value;
                OnPropertyChanged("Ponedeljak");
            }
        }

        public Appointment Tuesday
        {
            get { return tuesday; }
            set 
            { 
                tuesday = value;
                OnPropertyChanged("Utorak");
            }
        }

        public Appointment Wednesday
        {
            get { return wednesday; }
            set 
            { 
                wednesday = value;
                OnPropertyChanged("Sreda");
            }
        }

        public Appointment Thursday
        {
            get { return thursday; }
            set 
            { 
                thursday = value;
                OnPropertyChanged("Cetvrtak");
            }
        }

        public Appointment Friday
        {
            get { return friday; }
            set 
            { 
                friday = value;
                OnPropertyChanged("Petak");
            }
        }

        public Appointment Saturday
        {
            get { return saturday; }
            set 
            { 
                saturday = value;
                OnPropertyChanged("Subota");
            }
        }

        public Appointment Sunday
        {
            get { return sunday; }
            set 
            { 
                sunday = value;
                OnPropertyChanged("Nedelja");
            }
        }

        public WeeklyCalendarRowDTO()
        {
            this.TimeInRow = "";
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
