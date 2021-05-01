using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    class MedicineTherapy : INotifyPropertyChanged
    {
        private int durationInDays;
        public int DurationInDays
        {
            get => durationInDays;
            set
            {
                durationInDays = value;
                OnPropertyChanged("durationInDays");
            }
        }
        private int timesPerDay;
        public int TimesPerDay
        {
            get => timesPerDay;
            set
            {
                timesPerDay = value;
                OnPropertyChanged("timesPerDay");
            }
        }

        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        private string medicineID;
        public string MedicineID
        {
            get => medicineID;
            set
            {
                medicineID = value;
                OnPropertyChanged("MedicineID");
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
