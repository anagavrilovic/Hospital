using Hospital.DTO.DoctorDTO;
using Hospital.Factory;
using Hospital.Repositories;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Controller.DoctorControllers
{
    public class DoctorAppointmentsController
    {
        private AppointmentService appointmentService;
        private MedicalRecordService medicalRecordService;
        private DoctorService doctorService;
        NewAppointmentDTO DTO;
        public DoctorAppointmentsController(NewAppointmentDTO DTO)
        {
            this.DTO = DTO;
            appointmentService = new AppointmentService();
            medicalRecordService = new MedicalRecordService();
            doctorService = new DoctorService(new DoctorFileRepository());
        }

        public Model.Doctor GetDoctorById(string id)
        {
           return doctorService.GetDoctorById(id);
        }
        public List<Appointment> InitAppointments()
        {
            return appointmentService.InitAppointments(DTO.Doctor.PersonalID);
        }
        public void DeleteAppointment()
        {
            appointmentService.Delete(DTO.SelectedAppointment.IDAppointment);
        }
        public MedicalRecord GetByPatientId()
        {
            return medicalRecordService.GetByPatientId(DTO.SelectedAppointment.IDpatient);
        }
    }
}
