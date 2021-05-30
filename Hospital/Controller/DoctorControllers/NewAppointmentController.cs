using Hospital.DTO.DoctorDTO;
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

        public NewAppointmentController(AddAppointmentDTO DTO)
        {
            this.DTO = DTO;
            roomService = new RoomService();
            doctorService = new DoctorService();
            appointmentService = new AppointmentService();
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
            return appointmentService.GenerateID();
        }
    }
}
