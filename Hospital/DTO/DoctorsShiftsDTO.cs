using Hospital.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DTO
{
    public class DoctorsShiftsDTO : INotifyPropertyChanged
    {
        private Doctor doctor;
        public Doctor Doctor
        {
            get { return doctor; }
            set 
            { 
                doctor = value;
                OnPropertyChanged("Doctor");
            }
        }

        private string shiftDay1;
        public string ShiftDay1
        {
            get { return shiftDay1; }
            set 
            { 
                shiftDay1 = value;
                OnPropertyChanged("ShiftDay1");
            }
        }

        private string shiftDay2;
        public string ShiftDay2
        {
            get { return shiftDay2; }
            set 
            { 
                shiftDay2 = value;
                OnPropertyChanged("ShiftDay2");
            }
        }

        private string shiftDay3;
        public string ShiftDay3
        {
            get { return shiftDay3; }
            set 
            { 
                shiftDay3 = value;
                OnPropertyChanged("ShiftDay3");
            }
        }

        private string shiftDay4;
        public string ShiftDay4
        {
            get { return shiftDay4; }
            set 
            { 
                shiftDay4 = value;
                OnPropertyChanged("ShiftDay4");
            }
        }

        private string shiftDay5;
        public string ShiftDay5
        {
            get { return shiftDay5; }
            set 
            { 
                shiftDay5 = value;
                OnPropertyChanged("ShiftDay5");
            }
        }

        private string shiftDay6;
        public string ShiftDay6
        {
            get { return shiftDay6; }
            set
            {
                shiftDay6 = value;
                OnPropertyChanged("ShiftDay6");
            }
        }

        private string shiftDay7;
        public string ShiftDay7
        {
            get { return shiftDay7; }
            set
            {
                shiftDay7 = value;
                OnPropertyChanged("ShiftDay7");
            }
        }

        public DoctorsShiftsDTO()
        {
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
