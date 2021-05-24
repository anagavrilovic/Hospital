using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services.DoctorServices
{
    public class CreateAppointmentService
    {
        private DoctorService doctorService;
        private AppointmentService appointmentService;
        private MedicalRecordService medicalRecordService;
        public ObservableCollection<Appointment> SetParentAppointments(Appointment appointment)
        {
            medicalRecordService = new MedicalRecordService();
            doctorService = new DoctorService();
            appointmentService = new AppointmentService();
            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
            foreach (Appointment appointmentFromStorage in appointmentService.GetAll())
            {
                if (doctorService.GetDoctorById(appointmentFromStorage.IDDoctor).Specialty.Equals(appointment.Doctor.Specialty))
                {
                    appointmentFromStorage.PatientsRecord = medicalRecordService.GetByPatientId(appointmentFromStorage.IDpatient);
                    appointments.Add(appointmentFromStorage);
                }
            }
            return appointments;
        }
    }
}
