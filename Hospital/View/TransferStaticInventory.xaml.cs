using Hospital.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hospital.View
{
    public partial class TransferStaticInventory : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public Inventory ItemForTransfer { get; set; }
        public TransferInventory TransferRequest { get; set; }

        private TransferInventoryStorage _transferInventoryStorage;
        private TransferInventory _firstReservedTransfer;
        private int _totalQuantityForEachTransferOfItem;

        private ObservableCollection<string> _allRoomsIDs;
        public ObservableCollection<string> AllRoomsIDs
        {
            get => _allRoomsIDs;

            set
            {
                _allRoomsIDs = value;
                OnPropertyChanged("AllRoomsIDs");
            }
        }

        public TransferStaticInventory(Inventory inv)
        {
            InitializeComponent();
            this.DataContext = this;
            this._transferInventoryStorage = new TransferInventoryStorage();
            ItemForTransfer = inv;
            TransferRequest = new TransferInventory();
            this._totalQuantityForEachTransferOfItem = 0;
            this._firstReservedTransfer = new TransferInventory();

            AllRoomsIDs = GetAllRoomsIDs();
        }

        private void InitializeTransferRequest()
        {
            TransferRequest.ItemID = ItemForTransfer.Id;
            TransferRequest.FirstRoomID = ItemForTransfer.RoomID;
            TransferRequest.DestinationRoomID = roomID.Text;
            TransferRequest.TransferDate = datumPrebacivanja.SelectedDate.Value.Date;

            TimeSpan timeSpan = TimeSpan.ParseExact(TransferRequest.TransferTime, "c", null);
            TransferRequest.TransferDate = TransferRequest.TransferDate.Add(timeSpan);
        }

        private bool IsTransferTimeValid()
        {
            if (TransferRequest.TransferDate < DateTime.Now)
                return false;

            return true;
        }

        private bool IsTransferItemQuantityValid()
        {
            if (ItemForTransfer.Quantity < TransferRequest.Quantity)
                return false;

            return true;
        }

        private int getTotalQuantityForEachTransferOfItem()
        {
            int totalQuantityForTransfer = 0;
            foreach (TransferInventory ti in _transferInventoryStorage.GetAll())
            {
                if (ti.ItemID.Equals(TransferRequest.ItemID) && ti.FirstRoomID.Equals(TransferRequest.FirstRoomID))
                    totalQuantityForTransfer += ti.Quantity;   
            }
            return totalQuantityForTransfer;
        }

        private TransferInventory getFirstReservedTransferOfItem()
        {
            TransferInventory firstReservedTransfer = new TransferInventory();

            foreach (TransferInventory ti in _transferInventoryStorage.GetAll())
            {
                if (ti.ItemID.Equals(TransferRequest.ItemID) && ti.FirstRoomID.Equals(TransferRequest.FirstRoomID))
                {
                    if (ti.TransferDate > firstReservedTransfer.TransferDate)
                        firstReservedTransfer = ti;
                }
            }
            return firstReservedTransfer;
        }

        private bool NotEnoughItemForNewTransfer()
        {
            this._totalQuantityForEachTransferOfItem = getTotalQuantityForEachTransferOfItem();
            if (_totalQuantityForEachTransferOfItem + TransferRequest.Quantity > ItemForTransfer.Quantity)
                return true;

            return false;
        }

        private bool IsTransferDateAfterFirstReservedTransferDate()
        {
            this._firstReservedTransfer = getFirstReservedTransferOfItem();
            if (TransferRequest.TransferDate > _firstReservedTransfer.TransferDate)
                return true;

            return false;
        }

        private bool IsTransferDateBeforeFirstReservedTransferDate()
        {
            this._firstReservedTransfer = getFirstReservedTransferOfItem();
            if (TransferRequest.TransferDate < _firstReservedTransfer.TransferDate)
                return true;

            return false;
        }

        private void ScheduleTemporaryTransferBeforeFirstReservedTransfer()
        {
            if (_firstReservedTransfer.Quantity <= TransferRequest.Quantity)
            {
                _transferInventoryStorage.Delete(_firstReservedTransfer);
                _firstReservedTransfer.FirstRoomID = TransferRequest.DestinationRoomID;
                _transferInventoryStorage.Save(_firstReservedTransfer);
            }
            else
            {
                int newQuantity = _firstReservedTransfer.Quantity - TransferRequest.Quantity;
                _transferInventoryStorage.Delete(_firstReservedTransfer);
                _firstReservedTransfer.FirstRoomID = TransferRequest.DestinationRoomID;
                _firstReservedTransfer.Quantity = TransferRequest.Quantity;
                _transferInventoryStorage.Save(_firstReservedTransfer);

                TransferInventory newTransfer = new TransferInventory(_firstReservedTransfer.ItemID, newQuantity, ItemForTransfer.RoomID, _firstReservedTransfer.DestinationRoomID, _firstReservedTransfer.TransferDate + new TimeSpan(0, 0, 2));
                _transferInventoryStorage.Save(newTransfer);
                newTransfer.StartTransfer();
            }
        }

        private void AcceptButtonClick(object sender, RoutedEventArgs e)
        {
            InitializeTransferRequest();

            if (!IsTransferItemQuantityValid())
            {
                MessageBox.Show("Pogrešan unos količine!");
                return;
            }
            
            if (!IsTransferTimeValid())
            {
                MessageBox.Show("Niste ispravno uneli vreme!");
                return;
            }

            if (NotEnoughItemForNewTransfer() && IsTransferDateBeforeFirstReservedTransferDate())
            {
                ScheduleTemporaryTransferBeforeFirstReservedTransfer();
            }
            else if (NotEnoughItemForNewTransfer() && IsTransferDateAfterFirstReservedTransferDate())
            {
                MessageBox.Show("Sala ne raspolaže traženom količinom stavke. \n Pokušajte sa manjom količinom ili pogledajte stanje u drugim salama.");
                return;
            }

            _transferInventoryStorage.Save(TransferRequest);
            TransferRequest.StartTransfer();

            NavigationService.Navigate(new StaticInventoryView(ItemForTransfer.RoomID));
        }

        private ObservableCollection<string> GetAllRoomsIDs()
        {
            ObservableCollection<string> allRoomsIDs = new ObservableCollection<string>();
            RoomStorage roomStorage = new RoomStorage();
            foreach (Room room in roomStorage.GetAll())
            {
                if (!room.Id.Equals(ItemForTransfer.RoomID))
                    allRoomsIDs.Add(room.Id);
            }
            return allRoomsIDs;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StaticInventoryView(ItemForTransfer.RoomID));
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StaticInventoryView(ItemForTransfer.RoomID));
        }
    }
}
