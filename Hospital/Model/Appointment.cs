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
      
    /*  public System.Collections.Generic.List<Doctor> doctor;
      
      public System.Collections.Generic.List<Doctor> Doctor
      {
         get
         {
            if (doctor == null)
               doctor = new System.Collections.Generic.List<Doctor>();
            return doctor;
         }
         set
         {
            RemoveAllDoctor();
            if (value != null)
            {
               foreach (Doctor oDoctor in value)
                  AddDoctor(oDoctor);
            }
         }
      }
      
      
      public void AddDoctor(Doctor newDoctor)
      {
         if (newDoctor == null)
            return;
         if (this.doctor == null)
            this.doctor = new System.Collections.Generic.List<Doctor>();
         if (!this.doctor.Contains(newDoctor))
         {
            this.doctor.Add(newDoctor);
            newDoctor.AddAppointment(this);      
         }
      }
      
      
      public void RemoveDoctor(Doctor oldDoctor)
      {
         if (oldDoctor == null)
            return;
         if (this.doctor != null)
            if (this.doctor.Contains(oldDoctor))
            {
               this.doctor.Remove(oldDoctor);
               oldDoctor.RemoveAppointment(this);
            }
      }
      
      
      public void RemoveAllDoctor()
      {
         if (doctor != null)
         {
            System.Collections.ArrayList tmpDoctor = new System.Collections.ArrayList();
            foreach (Doctor oldDoctor in doctor)
               tmpDoctor.Add(oldDoctor);
            doctor.Clear();
            foreach (Doctor oldDoctor in tmpDoctor)
               oldDoctor.RemoveAppointment(this);
            tmpDoctor.Clear();
         }
      }*/
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