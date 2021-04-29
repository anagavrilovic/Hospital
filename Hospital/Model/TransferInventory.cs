using Hospital.View;
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
        private string itemID;
        private int quantity;
        private string firstRoomID;
        private string secondRoomID;
        private DateTime date;
        private string time;

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

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                if (value != date)
                {
                    date = value;
                    OnPropertyChanged("Date");
                }
            }
        }

        public string Time
        {
            get
            {
                return time;
            }

            set
            {
                if (value != time)
                {
                    time = value;
                    OnPropertyChanged("Time");
                }
            }
        }

        public TransferInventory() {}

        public TransferInventory(string itemId, int quantity, string firstRoomID, string secondRoomID, DateTime date)
        {
            this.itemID = itemId;
            this.quantity = quantity;
            this.firstRoomID = firstRoomID;
            this.secondRoomID = secondRoomID;
            this.date = date;
        }

        public void doTransfer()
        {
            Task task = new Task(() => this.waitUntilChosenDate());
            task.Start();
        }

        public void waitUntilChosenDate()
        {
            TimeSpan timeSpan = this.Date.Subtract(DateTime.Now);

            if (Date > DateTime.Now)
            {
                Thread.Sleep(timeSpan);
            }
       
            updateInventory();
        }

        public void updateInventory(bool doRefresh = true)
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

                if(doRefresh)
                {
                    refreshView();
                }
               
                foreach (Inventory i in InventoryStorage.inventory)
                {
                    if (i.Id.Equals(itemID) && i.RoomID.Equals(firstRoomID))
                    {
                        i.Quantity -= quantity;
                    }
                }

                inventoryStorage.doSerialization();
                StaticInventory.Inventory = inventoryStorage.GetByRoomID(this.FirstRoomID);

                TransferInventoryStorage transferStorage = new TransferInventoryStorage();
                transferStorage.Delete(this);

            }
            else
            {
               // MessageBox.Show("Pogrešan unos količine!");
               // return;
            }
        }

        public void refreshView()
        {
            if (!StaticInventory.Inventory.Equals(null))
            {
                foreach (Inventory i in StaticInventory.Inventory)
                {
                    if (i.Id.Equals(itemID) && i.RoomID.Equals(firstRoomID))
                    {
                        i.Quantity -= quantity;
                    }
                }
            }
        }
    }
}
