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
    /// Interaction logic for EditInventory.xaml
    /// </summary>
    public partial class EditInventory : Window
    {
        private ObservableCollection<Inventory> inventory;
        private Inventory inv;

        public ObservableCollection<Inventory> Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        public Inventory Inv
        {
            get { return inv; }
            set { inv = value; }
        }


        public EditInventory(Inventory inventory)
        {
            InitializeComponent();
            this.DataContext = this;
            this.inv = inventory;
        }



        private void accept(object sender, RoutedEventArgs e)
        {
            oznakaTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            nazivTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            cenaTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            kolicinaTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();

 
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + "inventory.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, InventoryStorage.inventory);
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
