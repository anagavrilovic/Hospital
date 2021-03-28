using System;
using System.Collections.Generic;

namespace Hospital
{
    public class Patient : User
    {
        private Boolean isGuest;
        private List<Appointment> appointment;

        public Patient()
        {
            appointment = new List<Appointment>();
        }

        public Boolean IsGuest
        {
            get => isGuest;
            set
            {
                isGuest = value;
            }
        }

        public List<Appointment> Appointment
        {
            get
            {
                if (appointment == null)
                    appointment = new List<Appointment>();
                return appointment;
            }
            set
            {
              //  RemoveAllAppointment();
                if (value != null)
                {
                    foreach (Appointment oAppointment in value)
                        AddAppointment(oAppointment);
                }
            }
        }


        public void AddAppointment(Appointment newAppointment)
        {
            if (newAppointment == null)
                return;
            if (this.appointment == null)
                this.appointment = new List<Appointment>();
            if (!this.appointment.Contains(newAppointment))
            {
                this.appointment.Add(newAppointment);
                newAppointment.IDpatient = this.PersonalID;
            }
        }


     /*   public void RemoveAppointment(Appointment oldAppointment)
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
        } */

    }
}