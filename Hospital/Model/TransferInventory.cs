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
        private string _transferID;
        private string _itemID;
        private int _quantity;
        private string _firstRoomID;
        private string _destinationRoomID;
        private DateTime _transferDate;
        private string _transferTime;

        public string TransferID
        {
            get => _transferID;
            set
            {
                _transferID = value;
                OnPropertyChanged("TransferID");
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
