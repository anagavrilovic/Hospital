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

        public ObservableCollection<Room> GetAll()
        {
            return roomRepository.GetAll();
        }

        public ObservableCollection<Room> GetAvaliableRoomsForNewAppointment(Appointment newAppointment)
        {
            ObservableCollection<Room> allRooms = roomRepository.GetAllRoomsWithoutMagazines();
            ObservableCollection<Room> avaliableRooms = new ObservableCollection<Room>();

            DateTime startTime = newAppointment.DateTime;
            DateTime endTime = newAppointment.DateTime.AddHours(newAppointment.DurationInHours);

            foreach (Room room in allRooms)
                if (IsRoomAvaliableInSelectedPeriod(room, startTime, endTime) && room.IsSuitableRoom(newAppointment.Type))
                    avaliableRooms.Add(room);

            return avaliableRooms;
        }

        private bool IsRoomAvaliableInSelectedPeriod(Room room, DateTime startTime, DateTime endTime)
        {
            ObservableCollection<Appointment> allAppointments = appointmentRepository.GetAll();

            foreach (Appointment appointment in allAppointments)
                if (appointment.IsOverlappingWith(startTime, endTime) && room.Id.Equals(appointment.Room.Id))
                    return false;

            return true;
        }
    }
}
