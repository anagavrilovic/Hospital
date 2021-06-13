using Hospital.Model;
using Hospital.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.View
{
    public partial class TransferDynamicInventoryView : Page
    {
        public DynamicInventory ItemForTransfer { get; set; }
        public TransferDynamicInventory TransferRequest { get; set; }

        public TransferDynamicInventoryView(DynamicInventory itemForTransfer)
        {
            InitializeComponent();
            this.DataContext = this;
            ItemForTransfer = itemForTransfer;
            TransferRequest = new TransferDynamicInventory();

            InitializeComboBoxItems();
        }

        private void AcceptButtonClick(object sender, RoutedEventArgs e)
        {
            InitializeTransferRequest();
            TransferDynamicInventoryService service = new TransferDynamicInventoryService(TransferRequest);
            service.ProcessRequest();

            NavigationService.Navigate(new DynamicInventoryView(ItemForTransfer.RoomID));
        }

        private void InitializeTransferRequest()
        {
            TransferRequest.ItemID = ItemForTransfer.Id;
            TransferRequest.FirstRoomID = ItemForTransfer.RoomID;
            TransferRequest.Quantity = int.Parse(kolicinaTxt.Text);
            TransferRequest.DestinationRoomID = destinationRoomsCB.Text;
        }

        private void InitializeComboBoxItems()
        {
            ObservableCollection<string> AllRoomsIDs = new ObservableCollection<string>();
            RoomService roomService = new RoomService();
            foreach (Room room in roomService.GetAll())
            {
                if(!room.Id.Equals(ItemForTransfer.RoomID))
                    AllRoomsIDs.Add(room.Id);
            }
            destinationRoomsCB.ItemsSource = AllRoomsIDs;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DynamicInventoryView(ItemForTransfer.RoomID));
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DynamicInventoryView(ItemForTransfer.RoomID));
        }
    }
}
