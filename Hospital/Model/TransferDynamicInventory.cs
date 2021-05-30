using Newtonsoft.Json;
using System.ComponentModel;

namespace Hospital.Model
{
    public class TransferDynamicInventory : INotifyPropertyChanged
    {
        private string _itemID;
        private int _quantity;
        private string _firstRoomID;
        private string _destinationRoomID;

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

        [JsonIgnore]
        public Room FirstRoom { get; set; }
        [JsonIgnore]
        public Room DestinationRoom { get; set; }

        public TransferDynamicInventory() { }    

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
