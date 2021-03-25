using System;

namespace Hospital
{
   public class Appointment
   {
      public DateTime dateTime;
      public double durationInHours = 0.5;
      public AppointmentType type;
      
      public Patient patient;
      
      
      public Patient Patient
      {
         get
         {
            return patient;
         }
         set
         {
            if (this.patient == null || !this.patient.Equals(value))
            {
               if (this.patient != null)
               {
                  Patient oldPatient = this.patient;
                  this.patient = null;
                  oldPatient.RemoveAppointment(this);
               }
               if (value != null)
               {
                  this.patient = value;
                  this.patient.AddAppointment(this);
               }
            }
         }
      }
      public System.Collections.Generic.List<Doctor> doctor;
      
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
      }
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