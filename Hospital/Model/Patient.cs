// File:    Patient.cs
// Author:  Marija
// Created: Monday, March 22, 2021 2:35:05 PM
// Purpose: Definition of Class Patient

using System;

public class Patient : User
{
   public Boolean isGuest;
   
   public System.Collections.Generic.List<Appointment> appointment;
   
   /// <summary>
   /// Property for collection of Appointment
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Appointment> Appointment
   {
      get
      {
         if (appointment == null)
            appointment = new System.Collections.Generic.List<Appointment>();
         return appointment;
      }
      set
      {
         RemoveAllAppointment();
         if (value != null)
         {
            foreach (Appointment oAppointment in value)
               AddAppointment(oAppointment);
         }
      }
   }
   
   /// <summary>
   /// Add a new Appointment in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddAppointment(Appointment newAppointment)
   {
      if (newAppointment == null)
         return;
      if (this.appointment == null)
         this.appointment = new System.Collections.Generic.List<Appointment>();
      if (!this.appointment.Contains(newAppointment))
      {
         this.appointment.Add(newAppointment);
         newAppointment.Patient = this;
      }
   }
   
   /// <summary>
   /// Remove an existing Appointment from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveAppointment(Appointment oldAppointment)
   {
      if (oldAppointment == null)
         return;
      if (this.appointment != null)
         if (this.appointment.Contains(oldAppointment))
         {
            this.appointment.Remove(oldAppointment);
            oldAppointment.Patient = null;
         }
   }
   
   /// <summary>
   /// Remove all instances of Appointment from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllAppointment()
   {
      if (appointment != null)
      {
         System.Collections.ArrayList tmpAppointment = new System.Collections.ArrayList();
         foreach (Appointment oldAppointment in appointment)
            tmpAppointment.Add(oldAppointment);
         appointment.Clear();
         foreach (Appointment oldAppointment in tmpAppointment)
            oldAppointment.Patient = null;
         tmpAppointment.Clear();
      }
   }

}