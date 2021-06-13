using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using Hospital.Services;
using Hospital.View.Manager;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.View
{
    public partial class TransferStaticInventory : Page
    {
        public Inventory ItemForTransfer { get; set; }
        public TransferInventory TransferRequest { get; set; }
        private SchedulingTransferInventoryService _schedulingTransferService;
     
        public TransferStaticInventory(Inventory inv)
        {
            InitializeComponent();
            this.DataContext = this;
            ItemForTransfer = inv;
            TransferRequest = new TransferInventory();

            InitializeComboBoxes();
        }

        private void AcceptButtonClick(object sender, RoutedEventArgs e)
        {
            InitializeTransferRequest();
            string message = _schedulingTransferService.ProcessRequest();
            MessageWindow messageWindow = new MessageWindow(message);
            messageWindow.Show();
         
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
            ITransferInventoryRepository transferRepo = new TransferInventoryFileRepository();
            IStaticInventoryRepository inventoryRepo = new StaticInventoryFileRepository();
            _schedulingTransferService = new SchedulingTransferInventoryService(TransferRequest, transferRepo, inventoryRepo);
        }

        private void InitializeComboBoxes()
        {
            ObservableCollection<string> AllRoomsIDs = new ObservableCollection<string>();
            RoomService roomService = new RoomService(new RoomFileRepository(), new AppointmentFileRepository());
            foreach (Room room in roomService.GetAll())
                if (!room.Id.Equals(ItemForTransfer.RoomID))
                    AllRoomsIDs.Add(room.Id);

            roomID.ItemsSource = AllRoomsIDs;
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
