using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class TransferDynamicInventoryView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public DynamicInventory ItemForTransfer { get; set; }
        public TransferDynamicInventory ItemTransfering { get; set; }

        private ObservableCollection<string> _allRoomsIDs;
        public ObservableCollection<string> AllRoomsIDs
        {
            get => _allRoomsIDs;

            set
            {
                _allRoomsIDs = value;
                OnPropertyChanged("RoomsIDs");
            }
        }

        public TransferDynamicInventoryView(DynamicInventory itemForTransfer)
        {
            InitializeComponent();
            this.DataContext = this;
            ItemForTransfer = itemForTransfer;
            ItemTransfering = new TransferDynamicInventory();

            AllRoomsIDs = GetAllRoomsIDs();
        }

        private void AcceptButtonClick(object sender, RoutedEventArgs e)
        {   
            if (!IsEnoughItemForTransferInFirstRoom())
            {
                MessageBox.Show("Sala ne raspolaže unetom količinom stavke. \n Pokušajte sa manjom količinom.");
                return;
            }
            ExecuteTransfer();

            NavigationService.Navigate(new DynamicInventoryView(ItemForTransfer.RoomID));
        }

        private bool IsEnoughItemForTransferInFirstRoom()
        {
            if (ItemForTransfer.Quantity >= int.Parse(kolicinaTxt.Text))
                return true;
            else
                return false;
        }

        private void ExecuteTransfer()
        {
            ItemTransfering = new TransferDynamicInventory(ItemForTransfer.Id, int.Parse(kolicinaTxt.Text), ItemForTransfer.RoomID, destinationRoomsCB.Text);
            ItemTransfering.UpdateDynamicInventory();
        }

        private ObservableCollection<string> GetAllRoomsIDs()
        {
            ObservableCollection<string> allRoomsIDs = new ObservableCollection<string>();
            RoomStorage roomStorage = new RoomStorage();
            foreach (Room room in roomStorage.GetAll())
            {
                if(!room.Id.Equals(ItemForTransfer.RoomID))
                    allRoomsIDs.Add(room.Id);
            }
            return allRoomsIDs;
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
