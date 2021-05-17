using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class TransferDynamicInventory : INotifyPropertyChanged
    {
        private string _itemID;
        private int _quantity;
        private string _firstRoomID;
        private string _destinationRoomID;

       private DynamicInventoryStorage _dynamicInventoryStorage = new DynamicInventoryStorage();


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
                if (value != _itemID)
                {
                    _itemID = value;
                    OnPropertyChanged("ItemID");
                }
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value != _quantity)
                {
                    _quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }
        }

        public string FirstRoomID
        {
            get => _firstRoomID;

            set
            {
                if (value != _firstRoomID)
                {
                    _firstRoomID = value;
                    OnPropertyChanged("FirstRoomID");
                }
            }
        }

        public string DestinationRoomID
        {
            get => _destinationRoomID;
            set
            {
                if (value != _destinationRoomID)
                {
                    _destinationRoomID = value;
                    OnPropertyChanged("DestinationRoomID");
                }
            }
        }

        [JsonIgnore]
        public Room FirstRoom { get; set; }
        [JsonIgnore]
        public Room DestinationRoom { get; set; }

        public TransferDynamicInventory() { }

        public TransferDynamicInventory(string itemId, int quantity, string firstRoomID, string destinationRoomID)
        {
            ItemID = itemId;
            Quantity = quantity;
            FirstRoomID = firstRoomID;
            DestinationRoomID = destinationRoomID;
        }

        public void UpdateDynamicInventory()
        {
            if (IsTransferingItemExistsInDestinationRoom())
                IncreaseItemQuantityInDestinationRoom();
            else
                AddTransferingItemDestinationRoom();

            ReduceItemQuantitiyInFirstRoom();
        }

        private bool IsTransferingItemExistsInDestinationRoom()
        {
            foreach(DynamicInventory item in _dynamicInventoryStorage.GetByRoomID(DestinationRoomID))
            {
                if (item.Id.Equals(ItemID))
                    return true;
            }
            return false;
        }

        private void IncreaseItemQuantityInDestinationRoom()
        {
            DynamicInventory itemInDestinationRoom = _dynamicInventoryStorage.GetOneByRoom(ItemID, DestinationRoomID);
            itemInDestinationRoom.Quantity += Quantity;
            _dynamicInventoryStorage.EditItem(itemInDestinationRoom);
        }

        private void AddTransferingItemDestinationRoom()
        {
            DynamicInventory itemFromFirstRoom = _dynamicInventoryStorage.GetOneByRoom(_itemID, _firstRoomID);
            itemFromFirstRoom.Quantity = Quantity;
            itemFromFirstRoom.RoomID = DestinationRoomID;
            _dynamicInventoryStorage.Save(itemFromFirstRoom);
        }

        private void ReduceItemQuantitiyInFirstRoom()
        {
            DynamicInventory itemInFirstRoom = _dynamicInventoryStorage.GetOneByRoom(ItemID, FirstRoomID);
            itemInFirstRoom.Quantity -= _quantity;
            _dynamicInventoryStorage.EditItem(itemInFirstRoom);
        }
    }
}
