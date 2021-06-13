using Hospital.Factory;
using Hospital.Repositories;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DTO.DoctorDTO
{
    public class CreateAppointmentController 
    {
        private MedicalRecordService medicalRecordService;
        private DoctorService doctorService;
        private AppointmentService appointmentService;
        private RoomService roomService;
        private CreateAppointmentDTO DTO;
        private DoctorsShiftService doctorsShiftService;

        public CreateAppointmentController(CreateAppointmentDTO DTO)
        {
            medicalRecordService = new MedicalRecordService();
            doctorService = new DoctorService();
            appointmentService = new AppointmentService();
            roomService = new RoomService();
            doctorsShiftService = new DoctorsShiftService(new DoctorFileFactory());
            this.DTO = DTO;
        }

        public MedicalRecord GetMedicalRecordByPatientId(string id)
        {
            return medicalRecordService.GetByPatientId(id);
        }
        public List<Model.Doctor> GetAllDoctors()
        {
            return doctorService.GetAll();
        }

        public void SaveAppointment()
        {
            appointmentService.Save(DTO.Appointment);
        }

        public bool IsDoctorAvaliableForAppointment()
        {
            return appointmentService.IsDoctorAvaliableForAppointment(DTO.Appointment);
        }
        public bool IsPatientAvaliableForAppointment()
        {
            return appointmentService.IsPatientAvaliableForAppointment(DTO.Appointment);
        }

        public List<Appointment> SetParentAppointments()
        {
          return appointmentService.SetParentAppointments(DTO.Appointment);
        }

        public Model.Doctor GetDoctorById(string id)
        {
            return doctorService.GetDoctorById(id);
        }

        public List<Room> GetAllRooms()
        {
            return roomService.GetAll();
        }

        public string GenerateAppointmentId()
        {
            return appointmentService.GetNewID();
        }

        internal bool IsDoctorWorkingAtSelectedTime()
        {
           return doctorsShiftService.IsDoctorWorkingAtSelectedTime(DTO.Appointment.Doctor, DTO.Appointment.DateTime);
        }
    }
}
