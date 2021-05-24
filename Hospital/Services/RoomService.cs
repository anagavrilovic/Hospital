using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class RoomService
    {
        private IRoomRepository roomRepository;
        private IAppointmentRepository appointmentRepository;

        public RoomService()
        {
            roomRepository = new RoomFileRepository();
            appointmentRepository = new AppointmentFileRepository();
        }

        public Room GetById(string id)
        {
            return roomRepository.GetByID(id);
        }

        public List<Room> GetAll()
        {
            return roomRepository.GetAll();
        }

        public List<Room> GetAvaliableRoomsForNewAppointment(Appointment newAppointment)
        {
            List<Room> allRooms = roomRepository.GetAllRoomsWithoutMagazines();
            List<Room> avaliableRooms = new List<Room>();

            DateTime startTime = newAppointment.DateTime;
            DateTime endTime = newAppointment.DateTime.AddHours(newAppointment.DurationInHours);

            foreach (Room room in allRooms)
                if (IsRoomAvaliableInSelectedPeriod(room, startTime, endTime) && room.IsSuitableRoom(newAppointment.Type))
                    avaliableRooms.Add(room);

            return avaliableRooms;
        }

        private bool IsRoomAvaliableInSelectedPeriod(Room room, DateTime startTime, DateTime endTime)
        {
            List<Appointment> allAppointments = appointmentRepository.GetAll();

            foreach (Appointment appointment in allAppointments)
                if (appointment.IsOverlappingWith(startTime, endTime) && room.Id.Equals(appointment.Room.Id))
                    return false;

            return true;
        }
    }
}
