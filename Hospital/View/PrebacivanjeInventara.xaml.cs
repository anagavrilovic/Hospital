using Hospital.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class PrebacivanjeInventara : Window
    {
        private Inventory inventorySeleceted;

        public PrebacivanjeInventara(Inventory inv)
        {
            InitializeComponent();
            this.inventorySeleceted = inv;
            
        }

        private void accept(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(kolicinaTxt.Text))

            {
                InventoryStorage istorage = new InventoryStorage();
                String secondRoomID = typeCB.Text;
                //DateTime date = datumPrebacivanja.SelectedDate.Value.Date;

                TransferInventory transferInventory = new TransferInventory(inventorySeleceted.Id, int.Parse(kolicinaTxt.Text), 
                                                                            inventorySeleceted.RoomID, secondRoomID);
                //istorage.UpdateInventory(this.inventorySeleceted, id, int.Parse(kolicinaTxt.Text));
                transferInventory.updateInventory();
                StaticInventory.Inventory = istorage.GetByRoomID(this.inventorySeleceted.RoomID);
            }

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
