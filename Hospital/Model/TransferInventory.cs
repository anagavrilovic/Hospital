using Hospital.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital.Model
{
    class TransferInventory : INotifyPropertyChanged
    {
        private string itemID;
        private int quantity;
        private string firstRoomID;
        private string secondRoomID;

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
            get
            {
                return itemID;
            }

            set
            {
                if (value != itemID)
                {
                    itemID = value;
                    OnPropertyChanged("ItemID");
                }
            }
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                if (value != quantity)
                {
                    quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }
        }

        public string FirstRoomID
        {
            get
            {
                return firstRoomID;
            }

            set
            {
                if (value != firstRoomID)
                {
                    firstRoomID = value;
                    OnPropertyChanged("FirstRoomID");
                }
            }
        }

        public string SecondRoomID
        {
            get
            {
                return secondRoomID;
            }

            set
            {
                if (value != secondRoomID)
                {
                    secondRoomID = value;
                    OnPropertyChanged("SecondRoomID");
                }
            }
        }

        public TransferInventory(string itemId, int quantity, string firstRoomID, string secondRoomID)
        {
            this.itemID = itemId;
            this.quantity = quantity;
            this.firstRoomID = firstRoomID;
            this.secondRoomID = secondRoomID;
        }

        public void updateInventory()
        {

            InventoryStorage inventoryStorage = new InventoryStorage();
            Inventory inventoryItem = inventoryStorage.GetOneByRoom(itemID, firstRoomID);

            if (firstRoomID.Equals(secondRoomID))
            {
                return;
            }

            if (inventoryItem.Quantity >= quantity)
            {
                ObservableCollection<Inventory> inventoryInSecondRoom = new ObservableCollection<Inventory>();
                inventoryInSecondRoom = inventoryStorage.GetByRoomID(secondRoomID);

                bool found = false;

                foreach (Inventory i in inventoryInSecondRoom)
                {
                    if (i.Name.ToLower().Equals(inventoryItem.Name.ToLower()))
                    {
                        i.Quantity += quantity;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    Inventory newItem = new Inventory
                    {
                        Id = itemID,
                        Name = inventoryItem.Name,
                        Quantity = quantity,
                        Price = inventoryItem.Price,
                        RoomID = secondRoomID
                    };
                    InventoryStorage.inventory.Add(newItem);
                }


                foreach (Inventory i in StaticInventory.Inventory)
                {
                    if (i.Id.Equals(itemID) && i.RoomID.Equals(firstRoomID))
                    {
                        i.Quantity -= quantity;
                    }
                }

                foreach (Inventory i in InventoryStorage.inventory)
                {
                    if (i.Id.Equals(itemID) && i.RoomID.Equals(firstRoomID))
                    {
                        i.Quantity -= quantity;
                    }
                }

                inventoryStorage.doSerialization();
            }
            else
            {
                MessageBox.Show("Pogrešan unos količine!");
            }
        }
    }
}
