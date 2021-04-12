using System;
using System.Text;

namespace Hospital
{
   public class Appointment
   {
      public double durationInHours = 0.5;
        
        public double DurationInHours
        {
            get
            {
                return durationInHours;
            }
            set
            {
                durationInHours = value;
            }
        }

      public DateTime DateTime { get; set; }

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
            }
        }

        public string IDpatient { get; set; }
      
      public string IDDoctor { get; set; }

      public string IDAppointment { get; set; }

      public string DoctrosNameSurname { get; set; }
      
      public Room room;
      
      public Room Room
      {
         get
         {
            return room;
         }
         set
         {
            this.room = value;
         }
      }

        public Appointment()
        {
            this.Type = AppointmentType.none;
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
                StringBuilder ret = new StringBuilder("");
                ret.Append(this.patientName);
                ret.Append(" ");
                ret.Append(this.patientSurname);
                ret.AppendLine();
                ret.Append(this.DoctrosNameSurname);
                ret.AppendLine();
                //ret.Append(this.Room.Name);

                return ret.ToString();
            }
        }
   }
}