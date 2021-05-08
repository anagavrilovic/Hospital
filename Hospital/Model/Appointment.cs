using System;
using System.ComponentModel;
using System.Text;

namespace Hospital
{
   public class Appointment : INotifyPropertyChanged
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
                OnPropertyChanged("AppointmentType");
            }
        }

        public string patientName;

        public string PatientName
        {
            get
            {
                return patientName;
            }
            set
            {
                patientName = value;
                OnPropertyChanged("PatientName");
            }
        }

        public string patientSurname;

        public string PatientSurname
        {
            get
            {
                return patientSurname;
            }
            set
            {
                patientSurname = value;
                OnPropertyChanged("PatientSurname");
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
                OnPropertyChanged("IDAppointment");
            } 
        }

        private string doctrosNameSurname;
        public string DoctrosNameSurname 
        {
            get
            {
                return doctrosNameSurname;
            }
            set
            {
                doctrosNameSurname = value;
                OnPropertyChanged("DoctrosNameSurname");
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

        public Appointment()
        {
            this.Type = AppointmentType.none;
        }

        public Appointment(DateTime dateTime, AppointmentType type, string patientName, string patientSurname, string iDpatient, string iDDoctor, string iDAppointment, 
            string doctrosNameSurname, Room room)
        {
            this.dateTime = dateTime;
            this.type = type;
            this.patientName = patientName;
            this.patientSurname = patientSurname;
            this.iDpatient = iDpatient;
            this.iDDoctor = iDDoctor;
            this.iDAppointment = iDAppointment;
            this.doctrosNameSurname = doctrosNameSurname;
            this.room = room;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public bool IsOverlappingWith(Appointment appointment)
        {
            if (this.IsDoctorInAppointment(appointment.IDDoctor))
                if (this.DateTime < appointment.DateTime.AddHours(appointment.DurationInHours) && appointment.DateTime < this.DateTime.AddHours(this.DurationInHours))
                    return true;

            return false;
        }

        public bool IsDoctorInAppointment(string doctorID)
        {
            return this.IDDoctor.Equals(doctorID);
        }

        public DayOfWeek GetAppointmentsDayOfWeek()
        {
            return this.DateTime.DayOfWeek;
        }

        override
        public string ToString()
        {
            if(this.Equals(new Appointment()))
            {
                return "";
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder("");
                stringBuilder.Append(this.patientName).Append(" ").Append(this.patientSurname);
                stringBuilder.AppendLine();
                stringBuilder.Append(this.DoctrosNameSurname);
                stringBuilder.AppendLine();
                stringBuilder.Append(this.Room.Name);

                return stringBuilder.ToString();
            }
        }
   }
}