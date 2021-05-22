using Hospital.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital.Model
{
    public class TransferInventory : INotifyPropertyChanged
    {
        private string _itemID;
        private int _quantity;
        private string _firstRoomID;
        private string _destinationRoomID;
        private DateTime _transferDate;
        private string _transferTime;

        private InventoryStorage _inventoryStorage = new InventoryStorage();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public string ItemID
        {
            get => _itemID;
            set
            {
                _itemID = value;
                OnPropertyChanged("ItemID");
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        public string FirstRoomID
        {
            get => _firstRoomID;
            set
            {
               _firstRoomID = value;
                OnPropertyChanged("FirstRoomID");
            }
        }

        public string DestinationRoomID
        {
            get => _destinationRoomID;
            set
            {
               _destinationRoomID = value;
                OnPropertyChanged("DestinationRoomID");
            }
        }

        public DateTime TransferDate
        {
            get => _transferDate;
            set
            {
               
               _transferDate = value;
               OnPropertyChanged("TransferDate");
            }
        }

        public string TransferTime
        {
            get => _transferTime;
            set
            {
                _transferTime = value;
                OnPropertyChanged("TransferTime");
            }
        }

        [JsonIgnore]
        public Room FirstRoom { get; set; }
        [JsonIgnore]
        public Room DestinationRoom { get; set; }

        public TransferInventory() {}

        public TransferInventory(string itemId, int quantity, string firstRoomID, string secondRoomID, DateTime date)
        {
            this._itemID = itemId;
            this._quantity = quantity;
            this._firstRoomID = firstRoomID;
            this._destinationRoomID = secondRoomID;
            this._transferDate = date;
        }

        public void StartTransfer()
        {
            Task task = new Task(() => this.WaitUntilTransferDate());
            task.Start();
        }

        public void WaitUntilTransferDate()
        {
            TimeSpan timeSpan = this.TransferDate.Subtract(DateTime.Now);

            if (TransferDate > DateTime.Now)
                Thread.Sleep(timeSpan);
       
           FinishTransfer();
        }

        public void FinishTransfer()
        {
            UpdateInventory();
            RemoveTransferRequest();
        }

        public void UpdateInventory()
        {
            Inventory inventoryItem = _inventoryStorage.GetOneByRoom(_itemID, _firstRoomID);
            if (inventoryItem.Quantity >= Quantity)
            {
                if (IsTransferingItemExistsInDestinationRoom())
                    IncreaseItemQuantityInDestinationRoom();
                else
                    AddTransferingItemDestinationRoom();

                ReduceItemQuantitiyInFirstRoom();
            }
        }

        public void RemoveTransferRequest()
        {
            TransferInventoryStorage transferStorage = new TransferInventoryStorage();
            transferStorage.Delete(this);
        }

        private bool IsTransferingItemExistsInDestinationRoom()
        {
            foreach (Inventory item in _inventoryStorage.GetByRoomID(DestinationRoomID))
            {
                if (item.Id.Equals(ItemID))
                    return true;
            }
            return false;
        }

        private void IncreaseItemQuantityInDestinationRoom()
        {
            Inventory itemInDestinationRoom = _inventoryStorage.GetOneByRoom(ItemID, DestinationRoomID);
            itemInDestinationRoom.Quantity += Quantity;
            _inventoryStorage.EditItem(itemInDestinationRoom);
        }

        private void AddTransferingItemDestinationRoom()
        {
            Inventory itemFromFirstRoom = _inventoryStorage.GetOneByRoom(_itemID, _firstRoomID);
            itemFromFirstRoom.Quantity = Quantity;
            itemFromFirstRoom.RoomID = DestinationRoomID;
            _inventoryStorage.Save(itemFromFirstRoom);
        }

        private void ReduceItemQuantitiyInFirstRoom()
        {
            Inventory itemInFirstRoom = _inventoryStorage.GetOneByRoom(ItemID, FirstRoomID);
            itemInFirstRoom.Quantity -= _quantity;
            _inventoryStorage.EditItem(itemInFirstRoom);
        }
    }
}
