using System;

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
      
      public AppointmentType type { get; set; }

      public string patientName { get; set; }

      public string patientSurname { get; set; }

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
   }
}