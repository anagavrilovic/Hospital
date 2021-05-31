using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Text;

namespace Hospital
{
   public class Appointment
   {
        private double durationInHours = 0.5;
        public double DurationInHours { get; set; }

        private DateTime dateTime;
        public DateTime DateTime { get; set; }

        public AppointmentType type;
        public AppointmentType Type { get; set; }

        private string iDpatient;
        public string IDpatient { get; set; }

        private string iDDoctor;
        public string IDDoctor { get; set; }

        private string iDAppointment;
        public string IDAppointment { get; set; }


        private Room room = new Room();
        public Room Room { get; set; }


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

        public Appointment(DateTime dateTime, AppointmentType type, string iDpatient, string iDDoctor, string iDAppointment, Room room)
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
    }
}