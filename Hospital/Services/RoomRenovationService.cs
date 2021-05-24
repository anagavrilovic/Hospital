using Hospital.Model;
using Hospital.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital.Services
{
    public class RoomRenovationService
    {
        public RoomRenovationService(RoomRenovation renovation)
        {
            Renovation = renovation;
        }

        private bool AreRenovationAttributesValid()
        {
            if (Renovation.Room.Status == RoomStatus.RENOVIRA_SE)
            {
                MessageBox.Show("Izabrana sala se trenutno renovira!");
                return false;
            }

            if (Renovation.StartDate >= Renovation.EndDate)
            {
                MessageBox.Show("Pogrešan izbor datuma renoviranja!");
                return false;
            }

            return true;
        }

        private bool IsAnyScheduledAppointmentInRoomDuringRenovation()
        {
            AppointmentStorage appointmentStorage = new AppointmentStorage();
            foreach (Appointment appointment in appointmentStorage.GetAll())
            {
                if (appointment.DateTime > Renovation.StartDate && appointment.DateTime < Renovation.EndDate + new TimeSpan(23, 59, 59) && appointment.Room.Id.Equals(Renovation.Room.Id))
                {
                    MessageBox.Show("U izabranom periodu postoje zakazani termini pa nije moguće zakazati renoviranje!");
                    return true;
                }
            }
            return false;
        }

        public void ProccessRenovationRequest()
        {
            if (!AreRenovationAttributesValid())
                return;

            if (IsAnyScheduledAppointmentInRoomDuringRenovation())
                return;

            SaveScheduledRenovation();
        }

        private void SaveScheduledRenovation()
        {
            RoomRenovationStorage renovationStorage = new RoomRenovationStorage();
            Renovation.Id = GenerateRenovationID();
            renovationStorage.Save(Renovation);
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
            Room room = _roomStorage.GetOne(Renovation.Room.Id);
            TimeSpan timeSpan = Renovation.EndDate.Subtract(DateTime.Now);
            if (room.Status != RoomStatus.RENOVIRA_SE)
            {
                TransferInventoryToWarehouse();
                room.Status = RoomStatus.RENOVIRA_SE;
                _roomStorage.EditRoom(room);
            }
            Thread.Sleep(timeSpan);
        }

        private void TransferInventoryToWarehouse()
        {
            InventoryStorage inventoryStorage = new InventoryStorage();
            foreach (Inventory inventory in inventoryStorage.GetByRoomID(Renovation.Room.Id))
            {
                TransferInventory transfer = new TransferInventory(inventory.Id, inventory.Quantity, Renovation.Room.Id, Renovation.WareHouse.Id, DateTime.Now);
                TransferInventoryService service = new TransferInventoryService(transfer);
                service.UpdateInventory();
            }
        }

        private void FinishRenovation()
        {
            Room room = _roomStorage.GetOne(Renovation.Room.Id);
            room.Status = RoomStatus.SLOBODNA;
            _roomStorage.EditRoom(room);
            FinishSeparatingRooms();
            FinishMergingRooms();
            _roomStorage.Delete(Renovation.Id);
        }

        private void FinishSeparatingRooms()
        {
            foreach (Room r in Renovation.RoomsCreatedDuringRenovation)
            {
                r.Status = RoomStatus.SLOBODNA;
                _roomStorage.Save(r);
            }
        }

        private void FinishMergingRooms()
        {
            foreach (Room r in Renovation.RoomsDestroyedDuringRenovation)
                _roomStorage.Delete(r.Id);
        }

        private string GenerateRenovationID()
        {
            List<int> allScheduledRenovationsIDs = _renovationRepository.GetAllScheduledRenovationsIDs();
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
        private RoomStorage _roomStorage = new RoomStorage();
        private RoomRenovationFileRepository _renovationRepository = new RoomRenovationFileRepository();
    }
}
