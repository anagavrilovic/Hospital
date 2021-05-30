using Hospital.Model;
using Hospital.Services;
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
            this.Doctor = new DoctorService().GetDoctorById(appointment.IDDoctor);
            this.Patient = new MedicalRecordService().GetByPatientId(appointment.IDpatient);
            this.ToTime = new DateTime();
        }

        public bool HasUrgentAppointment()
        {
            return this.Appointment.Type.Equals(AppointmentType.urgentExamination) || this.Appointment.Type.Equals(AppointmentType.urgentOperation);
        }
    }
}
