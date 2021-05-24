using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services.DoctorServices
{
    class AppointmentInfoService
    {
        private AppointmentService appointmentService;
        private MedicalRecordService medicalRecordService;
        private DoctorService doctorService;
        public ObservableCollection<Appointment> SetAppointmentDataGrid(DoctorSpecialty doctorSpecialty)
        {
            doctorService = new DoctorService();
            medicalRecordService = new MedicalRecordService();
            appointmentService = new AppointmentService();
            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();

            foreach (Appointment appointmentToAdd in appointmentService.GetAll())
            {
                appointmentToAdd.Doctor = doctorService.GetDoctorById(appointmentToAdd.IDDoctor);
                if (appointmentToAdd.Doctor.Specialty.Equals(doctorSpecialty))
                {
                    appointmentToAdd.PatientsRecord = medicalRecordService.GetByPatientId(appointmentToAdd.IDpatient);
                    appointments.Add(appointmentToAdd);
                }
            }
            return appointments;
        }
    }
}
