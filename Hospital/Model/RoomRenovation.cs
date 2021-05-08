using Hospital.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class RoomRenovation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private DateTime startDate;
        private DateTime endDate;
        private string description;

        public DateTime StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        public DateTime EndDate
        {
            get => endDate;
            set
            {
                endDate = value;
                OnPropertyChanged("EndDate");
            }
        }

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public Room Room { get; set; }
        public Room WareHouse { get; set; }
       

        public void startRenovation()
        {
            Task task = new Task(() => waitUntilEndDate());
            task.Start();
        }

        public void waitUntilEndDate()
        {
            Room r = roomStorage.GetOne(Room.Id);

            if (StartDate > DateTime.Now)
            {
                TimeSpan timeSpan = StartDate.Subtract(DateTime.Now);
                Thread.Sleep(timeSpan);
            }
            else if (EndDate > DateTime.Now && DateTime.Now > StartDate)
            {
                TimeSpan timeSpan = EndDate.Subtract(DateTime.Now);
                if (r.Status != RoomStatus.RENOVIRA_SE)
                {
                    transferInventoryToWarehouse();
                    r.Status = RoomStatus.RENOVIRA_SE;
                    roomStorage.Save(r);
                }
                
                Thread.Sleep(timeSpan);
            }
            else 
            {
                finishRenovation();
            }
        }

        private void transferInventoryToWarehouse()
        {
            foreach (Inventory inventory in inventoryStorage.GetByRoomID(Room.Id))
            {
                TransferInventory transfer = new TransferInventory(inventory.Id, inventory.Quantity, Room.Id, WareHouse.Id, DateTime.Now);
                transfer.UpdateInventory();
            }            
        }

        private void finishRenovation()
        {
            Room room = roomStorage.GetOne(Room.Id);
            room.Status = RoomStatus.SLOBODNA;
            roomStorage.Save(room);
            roomRenovationStorage.Delete(this);
        }

       private InventoryStorage inventoryStorage = new InventoryStorage();
       private DynamicInventoryStorage medicalSupplyStorage = new DynamicInventoryStorage();
       private TransferInventoryStorage transferInventoryStorage = new TransferInventoryStorage();
       private RoomStorage roomStorage = new RoomStorage();
       private RoomRenovationStorage roomRenovationStorage = new RoomRenovationStorage();
    }
}
