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
    public partial class PrebacivanjeInventara : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private Inventory inventorySeleceted;
        private TransferInventory transferItem;

        public Inventory InventorySeleceted
        {
            get { return inventorySeleceted; }
            set { inventorySeleceted = value; }
        }


        public TransferInventory TransferItem
        {
            get { return transferItem; }
            set { transferItem = value; }
        }

        private ObservableCollection<string> roomsIDs;
        public ObservableCollection<string> RoomsIDs
        {
            get
            {
                return roomsIDs;
            }

            set
            {
                roomsIDs = value;
                OnPropertyChanged("RoomsIDs");
            }
        }

        public PrebacivanjeInventara(Inventory inv)
        {
            InitializeComponent();
            this.DataContext = this;
            RoomsIDs = new ObservableCollection<string>();
            RoomStorage roomStorage = new RoomStorage();
            foreach (Room room in roomStorage.GetAll())
            {
                RoomsIDs.Add(room.Id);
            }
            this.inventorySeleceted = inv;
            this.transferItem = new TransferInventory();
        }

        private void accept(object sender, RoutedEventArgs e)
        {

            if (inventorySeleceted.Quantity < transferItem.Quantity)
            {
                MessageBox.Show("Pogrešan unos količine!");
                return;
            }

            InventoryStorage istorage = new InventoryStorage();
            transferItem.ItemID = inventorySeleceted.Id;
            transferItem.FirstRoomID = inventorySeleceted.RoomID;
            transferItem.SecondRoomID = roomID.Text;
            transferItem.Date = datumPrebacivanja.SelectedDate.Value.Date;

            TimeSpan timeSpan = TimeSpan.ParseExact(transferItem.Time, "c", null);
            transferItem.Date = transferItem.Date.Add(timeSpan);

            if (transferItem.Date < DateTime.Now)
            {
                MessageBox.Show("Niste ispravno uneli vreme!");
                return;
            }

            TransferInventoryStorage transferStorage = new TransferInventoryStorage();

            int totalQuantityForTransfer = 0;
            TransferInventory lastTransferItem = new TransferInventory();

            foreach (TransferInventory ti in transferStorage.GetAll())
            {
                if (ti.ItemID.Equals(transferItem.ItemID) && ti.FirstRoomID.Equals(transferItem.FirstRoomID))
                {
                    totalQuantityForTransfer += ti.Quantity;

                    if (ti.Date > lastTransferItem.Date)
                    {
                        lastTransferItem = ti;
                    }
                }
            }

            if (totalQuantityForTransfer + transferItem.Quantity > inventorySeleceted.Quantity && lastTransferItem.Date > transferItem.Date)
            {

                if (lastTransferItem.Quantity <= transferItem.Quantity)
                {
                    transferStorage.Delete(lastTransferItem);
                    lastTransferItem.FirstRoomID = transferItem.SecondRoomID;
                    transferStorage.Save(lastTransferItem);
                }
                else
                {
                    int newQuantity = lastTransferItem.Quantity - transferItem.Quantity;
                    transferStorage.Delete(lastTransferItem);
                    lastTransferItem.FirstRoomID = transferItem.SecondRoomID;
                    lastTransferItem.Quantity = transferItem.Quantity;
                    transferStorage.Save(lastTransferItem);

                    TransferInventory newTransfer = new TransferInventory(lastTransferItem.ItemID, newQuantity, inventorySeleceted.RoomID, lastTransferItem.SecondRoomID, lastTransferItem.Date + new TimeSpan(0, 0, 2));
                    transferStorage.Save(newTransfer);
                }
            }
            else if (totalQuantityForTransfer + transferItem.Quantity > inventorySeleceted.Quantity && lastTransferItem.Date < transferItem.Date)
            {
                MessageBox.Show("Sala ne raspolaže traženom količinom stavke. \n Pokušajte sa manjom količinom ili pogledajte stanje u drugim salama.");
                return;
            }

            transferStorage.Save(transferItem);
            transferItem.doTransfer();

            StaticInventoryView.Inventory = istorage.GetByRoomID(this.inventorySeleceted.RoomID);

            NavigationService.Navigate(new StaticInventoryView(InventorySeleceted.RoomID));
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StaticInventoryView(InventorySeleceted.RoomID));
        }

        private void back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StaticInventoryView(InventorySeleceted.RoomID));
        }
    }
}
