using Hospital.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private DateTime _startDate;
        private DateTime _endDate;
        private string _description;
        private ObservableCollection<Room> _roomsDestroyedDuringRenovation;
        private ObservableCollection<Room> _roomsCreatedDuringRenovation;

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged("EndDate");
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public ObservableCollection<Room> RoomsDestroyedDuringRenovation
        {
            get => _roomsDestroyedDuringRenovation;
            set
            {
                _roomsDestroyedDuringRenovation = value;
                OnPropertyChanged("RoomsDestroyedDuringRenovation");
            }
        }

        public ObservableCollection<Room> RoomsCreatedDuringRenovation
        {
            get => _roomsCreatedDuringRenovation;
            set
            {
                _roomsCreatedDuringRenovation = value;
                OnPropertyChanged("RoomsCreatedDuringRenovation");
            }
        }

        public Room Room { get; set; }
        public Room WareHouse { get; set; }

        public  RoomRenovation()
        {
            RoomsCreatedDuringRenovation = new ObservableCollection<Room>();
            RoomsDestroyedDuringRenovation = new ObservableCollection<Room>();
            Room = new Room();
            WareHouse = new Room();
        }
       

        public void StartRenovation()
        {
            Task task = new Task(() => WaitUntilEndDate());
            task.Start();
        }

        public void WaitUntilEndDate()
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
                    TransferInventoryToWarehouse();
                    r.Status = RoomStatus.RENOVIRA_SE;
                    roomStorage.Save(r);
                }
                
                Thread.Sleep(timeSpan);
            }
            else 
            {
                FinishRenovation();
            }
        }

        private void TransferInventoryToWarehouse()
        {
            foreach (Inventory inventory in inventoryStorage.GetByRoomID(Room.Id))
            {
                TransferInventory transfer = new TransferInventory(inventory.Id, inventory.Quantity, Room.Id, WareHouse.Id, DateTime.Now);
                transfer.UpdateInventory();
            }            
        }

        private void FinishRenovation()
        {
            Room room = roomStorage.GetOne(Room.Id);
            room.Status = RoomStatus.SLOBODNA;
            roomStorage.Save(room);
            FinishSeparatingRooms();
            FinishMergingRooms();
            roomRenovationStorage.Delete(this);
        }

        private void FinishSeparatingRooms()
        {
            foreach (Room r in RoomsCreatedDuringRenovation)
            {
                r.Status = RoomStatus.SLOBODNA;
                roomStorage.Save(r);
            }
        }

        private void FinishMergingRooms()
        {
            foreach (Room r in RoomsDestroyedDuringRenovation)
                roomStorage.Delete(r.Id);
        }

       private InventoryStorage inventoryStorage = new InventoryStorage();
       private DynamicInventoryStorage medicalSupplyStorage = new DynamicInventoryStorage();
       private TransferInventoryStorage transferInventoryStorage = new TransferInventoryStorage();
       private RoomStorage roomStorage = new RoomStorage();
       private RoomRenovationStorage roomRenovationStorage = new RoomRenovationStorage();
    }
}
