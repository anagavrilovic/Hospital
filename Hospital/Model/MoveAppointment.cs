using Hospital.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public class MoveAppointment
    {
        public Doctor Doctor { get; set; }
        public MedicalRecord Patient { get; set; }
        public Appointment Appointment { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }

        public MoveAppointment(Appointment appointment)
        {
            this.Appointment = appointment;
            this.FromTime = appointment.DateTime;
            this.Doctor = new DoctorStorage().GetDoctorByID(appointment.IDDoctor);
            this.Patient = new MedicalRecordStorage().GetByPatientID(appointment.IDpatient);
            this.ToTime = new DateTime();
        }

        /*public void SetTimeForRescheduling()
        {
            AppointmentStorage appointmentStorage = new AppointmentStorage();
            Appointment appointment = appointmentStorage.GetByID(AppointmentID);
            ToTime = FromTime.AddHours(appointment.DurationInHours);

            while (true)
            {
               // if(appointmentStorage.CheckIfOverlap())
            }
        }*/
    }
}
