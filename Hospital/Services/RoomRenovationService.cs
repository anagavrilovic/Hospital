using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class RoomRenovationService
    {
        private IRoomRenovationRepository renovationRepository;
        private IRoomRepository roomRepository;
        private IAppointmentRepository appointmentRepository;

        public RoomRenovationService(RoomRenovation renovation)
        {
            renovationRepository = new RoomRenovationFileRepository();
            roomRepository = new RoomFileRepository();
            appointmentRepository = new AppointmentFileRepository();
            Renovation = renovation;
        }

        public string ProccessRenovationRequest()
        {
            string ret = "Renoviranje je uspešno zakazano!";
            if (!AreRenovationAttributesValid())
                ret = "Niste ispravno uneli podatke o renoviranju!";

            if (IsAnyScheduledAppointmentInRoomDuringRenovation(Renovation.Room.Id))
                ret = "\n Renoviranje je uspešno zakazano! \n Za izabrani period renoviranja, u sali su postojali zakazani termini " +
                       "koji su premešteni u druge sale!";

            if (IsAnyScheduledAppointmentInRoomsForMergingDuringAndAfterRenovation())
                ret = "\n Renoviranje je uspešno zakazano! \n U salama koje ste izabrali za spajanje su postojali zakazani termini  " +
                       "nakon početka renoviranja pa su oni premešteni u druge sale!"; 

            SaveScheduledRenovation();
            return ret;
        }

        private bool AreRenovationAttributesValid()
        {
            if (Renovation.Room.Status == RoomStatus.RENOVIRA_SE)
                return false;           

            if (Renovation.StartDate >= Renovation.EndDate)
                return false;

            return true;
        }

        private bool IsAnyScheduledAppointmentInRoomDuringRenovation(string roomId)
        {
            bool ret = false;
            foreach (Appointment appointment in appointmentRepository.GetAppointmentsFromRoom(roomId))
            {
                if (appointment.DateTime > Renovation.StartDate && appointment.DateTime < Renovation.EndDate + new TimeSpan(23, 59, 59))
                {
                    MoveAppointmentToAnotherRoom(appointment);
                    NotifyPatientAndDoctorAboutAppointmentChange(appointment);
                    ret = true;
                }
            }
            return ret;
        }

        private bool IsAnyScheduledAppointmentInRoomsForMergingDuringAndAfterRenovation()
        {
            foreach (Room room in Renovation.RoomsDestroyedDuringRenovation)
                if (IsAnyScheduledAppointmentInRoomAfterRenovationStart(room.Id))
                    return true;

            return false;
        }

        private bool IsAnyScheduledAppointmentInRoomAfterRenovationStart(string roomId)
        {
            bool ret = false;
            foreach (Appointment appointment in appointmentRepository.GetAppointmentsFromRoom(roomId))
            {
                if (appointment.DateTime > Renovation.StartDate)
                {
                    MoveAppointmentToAnotherRoomOnAnotherFloor(appointment);
                    NotifyPatientAndDoctorAboutAppointmentChange(appointment);
                    ret = true;
                }
            }
            return ret;
        }

        private void MoveAppointmentToAnotherRoom(Appointment movingAppointment)
        {
            RoomService roomService = new RoomService(new RoomFileRepository(), new AppointmentFileRepository());
            Room newRoom = roomService.GetAvaliableRoomsForNewAppointment(movingAppointment)[0];
            movingAppointment.Room = newRoom;
            appointmentRepository.Update(movingAppointment);
        }

        private void MoveAppointmentToAnotherRoomOnAnotherFloor(Appointment appointment)
        {
            appointment.Room = GetNewRoomForAppointment(appointment);
            appointmentRepository.Update(appointment);
        }

        private Room GetNewRoomForAppointment(Appointment movingAppointment)
        {
            RoomService roomService = new RoomService(new RoomFileRepository(), new AppointmentFileRepository());
            Room newRoom = roomService.GetAvaliableRoomsForNewAppointment(movingAppointment)[0];
            List<Room> availableRooms = roomService.GetAvaliableRoomsForNewAppointment(movingAppointment);
            foreach (Room room in availableRooms)
            {
                if (room.Floor != roomService.GetById(movingAppointment.Room.Id).Floor)
                {
                    newRoom = room;
                    break;
                }
            }

            return newRoom;
        }

        private void NotifyPatientAndDoctorAboutAppointmentChange(Appointment appointment)
        {
            NotificationService notificationService = new NotificationService();
            notificationService.NotifyPatientAboutRescheduledAppointmentBeacuseOfRoomRenovation(appointment);
            notificationService.NotifyDoctorAboutAppointmentRoomUpdate(appointment);
        }

        private void SaveScheduledRenovation()
        {
            Renovation.Id = GenerateRenovationID();
            renovationRepository.Save(Renovation);
        }

        public void ScheduleRenovation()
        {
            Task task = new Task(() => CheckRenovationStatus());
            task.Start();
        }

        public void CheckRenovationStatus()
        {
            if (Renovation.StartDate > DateTime.Now)
                StartRenovation();
            else if (Renovation.EndDate > DateTime.Now && DateTime.Now > Renovation.StartDate)
                RenovationInProgress();
            else
                FinishRenovation();
        }

        private void StartRenovation()
        {
            TimeSpan timeSpan = Renovation.StartDate.Subtract(DateTime.Now);
            Thread.Sleep(timeSpan);
        }

        private void RenovationInProgress()
        {
            Room room = roomRepository.GetByID(Renovation.Room.Id);
            TimeSpan timeSpan = Renovation.EndDate.Subtract(DateTime.Now);
            if (room.Status != RoomStatus.RENOVIRA_SE)
            {
                TransferInventoryToWarehouse();
                room.Status = RoomStatus.RENOVIRA_SE;
                roomRepository.EditRoom(room);
            }
            Thread.Sleep(timeSpan);
        }

        private void TransferInventoryToWarehouse()
        {
            IStaticInventoryRepository inventoryRepository = new StaticInventoryFileRepository();
            foreach (Inventory inventory in inventoryRepository.GetAllInventoryFromRoom(Renovation.Room.Id))
            {
                TransferInventory transfer = new TransferInventory(inventory.Id, inventory.Quantity, Renovation.Room.Id, Renovation.WareHouse.Id, DateTime.Now);
                TransferInventoryService service = new TransferInventoryService(transfer, new StaticInventoryFileRepository(), new RoomFileRepository());
                service.UpdateInventory();
            }
        }

        private void FinishRenovation()
        {
            Room room = roomRepository.GetByID(Renovation.Room.Id);
            room.Status = RoomStatus.SLOBODNA;
            roomRepository.EditRoom(room);
            FinishSeparatingRooms();
            FinishMergingRooms();
            renovationRepository.Delete(Renovation.Id);
        }

        private void FinishSeparatingRooms()
        {
            foreach (Room r in Renovation.RoomsCreatedDuringRenovation)
            {
                r.Status = RoomStatus.SLOBODNA;
                roomRepository.Save(r);
            }
        }

        private void FinishMergingRooms()
        {
            foreach (Room r in Renovation.RoomsDestroyedDuringRenovation)
                roomRepository.Delete(r.Id);
        }

        private string GenerateRenovationID()
        {
            List<int> allScheduledRenovationsIDs = renovationRepository.GetAllScheduledRenovationsIDs();
            int id = 1;
            while (true)
            {
                if (!allScheduledRenovationsIDs.Contains(id))
                    break;

                id += 1;
            }
            return id.ToString();
        }

        public RoomRenovation Renovation { get; set; }
    }
}
