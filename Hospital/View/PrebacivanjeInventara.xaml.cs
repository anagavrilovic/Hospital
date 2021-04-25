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
    /// <summary>
    /// Interaction logic for PrebacivanjeInventara.xaml
    /// </summary>
    public partial class PrebacivanjeInventara : Window, INotifyPropertyChanged
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
            get { return inventorySeleceted;  }
            set { inventorySeleceted = value; }
        }


        public TransferInventory TransferItem
        {
            get
            {
                return transferItem;
            }

            set
            {
                if (value != transferItem)
                {
                    transferItem = value;
                   // OnPropertyChanged("TransferItem");
                }
            }
        }

        public PrebacivanjeInventara(Inventory inv)
        {
            InitializeComponent();
            this.DataContext = this;
            this.inventorySeleceted = inv;
            this.transferItem = new TransferInventory();
        }

        private void accept(object sender, RoutedEventArgs e)
        {
           
            InventoryStorage istorage = new InventoryStorage();
            String secondRoomID = roomID.Text;
            transferItem.ItemID = inventorySeleceted.Id;
            transferItem.FirstRoomID = inventorySeleceted.RoomID;
            transferItem.SecondRoomID = roomID.Text;
            transferItem.Date = datumPrebacivanja.SelectedDate.Value.Date;
            transferItem.DateString = datumPrebacivanja.SelectedDate.Value.ToShortDateString();

            
            TimeSpan timeSpan = TimeSpan.ParseExact(transferItem.Time, "c", null);
            transferItem.Date = transferItem.Date.Add(timeSpan);

            if (transferItem.Date < DateTime.Now)
            {
                MessageBox.Show("Niste ispravno uneli vreme!");
                return;
            }

            TransferInventoryStorage transferStorage = new TransferInventoryStorage();
            transferStorage.Save(transferItem);
            transferItem.doTransfer();
        
            StaticInventory.Inventory = istorage.GetByRoomID(this.inventorySeleceted.RoomID);
          
            this.Close();
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
