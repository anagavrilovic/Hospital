using Newtonsoft.Json;
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


        [JsonIgnore]
        public MedicalRecord PatientsRecord { get; set; }
        [JsonIgnore]
        public Model.Doctor Doctor { get; set; }


        public Appointment()
        {
            this.Type = AppointmentType.none;
            this.PatientsRecord = new MedicalRecord();
            this.Doctor = new Model.Doctor();
        }

        public Appointment(DateTime dateTime, AppointmentType type, string patientName, string patientSurname, string iDpatient, string iDDoctor, string iDAppointment, 
            string doctrosNameSurname, Room room)
        {
            this.dateTime = dateTime;
            this.type = type;
            this.iDpatient = iDpatient;
            this.iDDoctor = iDDoctor;
            this.iDAppointment = iDAppointment;
            this.room = room;
        }

        public bool IsDoctorAvaliable(Appointment appointment)
        {
            if (this.IsDoctorInAppointment(appointment.IDDoctor))
                if (this.IsOverlappingWith(appointment))
                    return false;

            return true;
        }

        public bool IsDoctorInAppointment(string doctorID)
        {
            return this.IDDoctor.Equals(doctorID);
        }

        public bool IsPatientAvaliable(Appointment appointment)
        {
            if (this.IsPatientInAppointment(appointment.IDpatient))
                if (this.IsOverlappingWith(appointment))
                    return false;

            return true;
        }

        public bool IsPatientInAppointment(string patientID)
        {
            return this.IDpatient.Equals(patientID);
        }

        public bool IsOverlappingWith(Appointment appointment)
        {
            return this.DateTime < appointment.DateTime.AddHours(appointment.DurationInHours) && appointment.DateTime < this.DateTime.AddHours(this.DurationInHours);
        }

        public bool IsOverlappingWith(DateTime startTime, DateTime endTime)
        {
            return this.DateTime < endTime && startTime < this.DateTime.AddHours(this.DurationInHours);
        }

        public DayOfWeek GetAppointmentsDayOfWeek()
        {
            return this.DateTime.DayOfWeek;
        }

        override
        public string ToString()
        {
            if(this.Equals(new Appointment()))
                return "";
            else
            {
                StringBuilder stringBuilder = new StringBuilder("");
                stringBuilder.Append(this.PatientsRecord.Patient.FirstName).Append(" ").Append(this.PatientsRecord.Patient.LastName);
                stringBuilder.AppendLine();
                stringBuilder.Append(this.Doctor.ToString());
                stringBuilder.AppendLine();
                stringBuilder.Append(this.Room.Name);

                return stringBuilder.ToString();
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