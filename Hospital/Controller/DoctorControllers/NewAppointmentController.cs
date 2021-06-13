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
    public class NewAppointmentController
    {
        private AddAppointmentDTO DTO;
        private RoomService roomService;
        private DoctorService doctorService;
        private AppointmentService appointmentService;
        private DoctorsShiftService doctorsShiftService;

        public NewAppointmentController(AddAppointmentDTO DTO)
        {
            this.DTO = DTO;
            roomService = new RoomService();
            doctorService = new DoctorService();
            appointmentService = new AppointmentService(new AppointmentFileFactory(), new DoctorFileFactory(), new MedicalRecordFileFactory());
            doctorsShiftService = new DoctorsShiftService(new DoctorFileFactory());
        }

        public Model.Doctor GetDoctorById(string doctorId)
        {
            return doctorService.GetDoctorById(doctorId);
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

        public List<Room> GetAllRooms()
        {
            return roomService.GetAll();
        }

        public string GenereteAppointmentId()
        {
            return appointmentService.GetNewID();
        }

        public bool IsDoctorWorkingAtSelectedTime()
        {
            return doctorsShiftService.IsDoctorWorkingAtSelectedTime(DTO.Appointment.Doctor, DTO.Appointment.DateTime);
        }
    }
}
