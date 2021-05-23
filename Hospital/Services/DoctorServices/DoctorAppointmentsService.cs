using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services.DoctorServices
{
    public class DoctorAppointmentsService
    {
        DoctorService doctorService = new DoctorService();
        MedicalRecordService medicalRecordService = new MedicalRecordService();
        AppointmentService appointmentService = new AppointmentService();


        public ObservableCollection<Appointment> InitAppointments(string doctorId)
        {
            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
            foreach (Appointment a in appointmentService.GetAll())
            {
                if (a.IDDoctor.Equals(doctorId))
                {
                    a.Doctor = doctorService.GetDoctorById(a.IDDoctor);
                    a.PatientsRecord = medicalRecordService.GetByPatientId(a.IDpatient);
                    appointments.Add(a);
                }
            }
            return appointments;
        }

        public void DeleteAppointmentFromExamination(string appointmentId, MedicalRecord medicalRecord)
        {
            foreach (Examination examination in medicalRecord.Examination)
            {
                if (examination.appointment.IDAppointment   != null)
                {
                    if (examination.appointment.IDAppointment.Equals(appointmentId))
                    {
                        medicalRecord.RemoveExamination(examination);
                        break;
                    }
                }
            }
        }
    }
}
