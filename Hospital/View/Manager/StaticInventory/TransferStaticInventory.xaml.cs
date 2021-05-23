using Hospital.Model;
using Hospital.Services;
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
        public Inventory ItemForTransfer { get; set; }
        public TransferInventory TransferRequest { get; set; }
        private SchedulingTransferInventoryService _schedulingTransferService;

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
            ItemForTransfer = inv;
            TransferRequest = new TransferInventory();

            AllRoomsIDs = InitializeComboBoxes();
        }

        private void AcceptButtonClick(object sender, RoutedEventArgs e)
        {
            InitializeTransferRequest();
            _schedulingTransferService.ProcessRequest();
         
            NavigationService.Navigate(new StaticInventoryView(ItemForTransfer.RoomID));
        }

        private void InitializeTransferRequest()
        {
            TransferRequest.ItemID = ItemForTransfer.Id;
            TransferRequest.FirstRoomID = ItemForTransfer.RoomID;
            TransferRequest.DestinationRoomID = roomID.Text;
            TransferRequest.TransferDate = datumPrebacivanja.SelectedDate.Value.Date;

            TimeSpan timeSpan = TimeSpan.ParseExact(TransferRequest.TransferTime, "c", null);
            TransferRequest.TransferDate = TransferRequest.TransferDate.Add(timeSpan);
            _schedulingTransferService = new SchedulingTransferInventoryService(TransferRequest);
        }

        private ObservableCollection<string> InitializeComboBoxes()
        {
            ObservableCollection<string> allRoomsIDs = new ObservableCollection<string>();
            RoomStorage roomStorage = new RoomStorage();
            foreach (Room room in roomStorage.GetAll())
                if (!room.Id.Equals(ItemForTransfer.RoomID))
                    allRoomsIDs.Add(room.Id);

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
