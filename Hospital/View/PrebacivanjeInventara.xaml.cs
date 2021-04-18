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
                /*ObservableCollection<Inventory> inventar = new ObservableCollection<Inventory>();
                InventoryStorage storage = new InventoryStorage();
                inventar = storage.GetByRoomID(oznakaTxt.Text);

                bool found = false;

                foreach(Inventory i in inventar)
                {
                    if (i.Name.Equals(nazivTxt.Text))
                    {
                        i.Quantity += int.Parse(kolicinaTxt.Text);
                        found = true;
                        break;
                    }
                }

                if(!found)
                {
                    Inventory newItem = new Inventory
                    {
                        Id = inventorySeleceted.Id,
                        Name = nazivTxt.Text,
                        Quantity = int.Parse(kolicinaTxt.Text),
                        Price = inventorySeleceted.Price,
                        RoomID = oznakaTxt.Text
                    };
                    storage.Save(newItem);
                }

                
                this.inventorySeleceted.Quantity -= int.Parse(kolicinaTxt.Text);

                using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + "inventory.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, InventoryStorage.inventory);
                }*/

                InventoryStorage istorage = new InventoryStorage();
                String id = typeCB.Text;
                istorage.UpdateInventory(this.inventorySeleceted, id, int.Parse(kolicinaTxt.Text));
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
