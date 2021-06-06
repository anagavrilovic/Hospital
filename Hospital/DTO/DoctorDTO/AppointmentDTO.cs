using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DTO.DoctorDTO
{
   public class AppointmentDTO : ViewModel
    {
        private double durationInHours = 0.5;
        public double DurationInHours
        {
            get
            {
                return durationInHours;
            }
            set
            {
                durationInHours = value;
                OnPropertyChanged("DurationInHours");
            }
        }

        private DateTime dateTime;
        public DateTime DateTime
        {
            get
            {
                return dateTime;
            }
            set
            {
                dateTime = value;
                OnPropertyChanged("DateTime");
            }
        }

        public AppointmentType type;
        public AppointmentType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }

        private string iDpatient;
        public string IDpatient
        {
            get
            {
                return iDpatient;
            }
            set
            {
                iDpatient = value;
                OnPropertyChanged("IDpatient");
            }
        }

        private string iDDoctor;
        public string IDDoctor
        {
            get
            {
                return iDDoctor;
            }
            set
            {
                iDDoctor = value;
                OnPropertyChanged("IDDoctor");
            }
        }

        private string iDAppointment;
        public string IDAppointment
        {
            get
            {
                return iDAppointment;
            }
            set
            {
                iDAppointment = value;
                OnPropertyChanged("IDDoctor");
            }
        }


        private Room room = new Room();
        public Room Room
        {
            get
            {
                return room;
            }
            set
            {
                this.room = value;
                OnPropertyChanged("Room");
            }
        }

        public override string ToString()
        {
            return DateTime.ToString();
        }
    }
}
