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

        private bool AreRenovationAttributesValid()
        {
            if (Renovation.Room.Status == RoomStatus.RENOVIRA_SE)
            {
                return false;
            }

            if (Renovation.StartDate >= Renovation.EndDate)
            {
                return false;
            }

            return true;
        }

        private bool IsAnyScheduledAppointmentInRoomDuringRenovation(string roomId)
        {
            foreach (Appointment appointment in appointmentRepository.GetAppointmentsFromRoom(roomId))
            {
                if (appointment.DateTime > Renovation.StartDate && appointment.DateTime < Renovation.EndDate + new TimeSpan(23, 59, 59))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsAnyScheduledAppointmentInMergingRoomsAfterRenovationStart(string roomId)
        {
            foreach (Appointment appointment in appointmentRepository.GetAppointmentsFromRoom(roomId))
            {
                if (appointment.DateTime > Renovation.StartDate)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsAnyScheduledAppointmentInRoomsForMergingDuringRenovation()
        {
            foreach (Room room in Renovation.RoomsDestroyedDuringRenovation)
                if (IsAnyScheduledAppointmentInMergingRoomsAfterRenovationStart(room.Id))
                    return true;
            
            return false;
        }

        public string ProccessRenovationRequest()
        {
            if (!AreRenovationAttributesValid())
                return "Niste ispravno uneli podatke o renoviranju!";

            if (IsAnyScheduledAppointmentInRoomDuringRenovation(Renovation.Room.Id))
                return "Za izabrani period renoviranja, u sali postoje zakazani termini!";

            if (IsAnyScheduledAppointmentInRoomsForMergingDuringRenovation())
                return "U salama koje ste izabrali za spajanje postoje zakazani termini tokom i nakon renoviranja!"; 

            SaveScheduledRenovation();
            return "Renoviranje uspešno zakazano!";
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
                TransferInventoryService service = new TransferInventoryService(transfer);
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
