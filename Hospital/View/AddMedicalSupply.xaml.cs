using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class AddMedicalSupply : Page
    {
        public DynamicInventory MedicalSupplyItem  { get; set; }
        private DynamicInventoryStorage _medicalSupplyStorage;

        public AddMedicalSupply(string id)
        {
            InitializeComponent();
            this.DataContext = this;

            this._medicalSupplyStorage = new DynamicInventoryStorage();
            MedicalSupplyItem = new DynamicInventory();
            MedicalSupplyItem.RoomID = id;
        }

        private void AcceptAddingButtonClick(object o, RoutedEventArgs e)
        {
            switch (Units.SelectedIndex)
            {
                case 0: MedicalSupplyItem.Units = UnitsType.kutije;   break;
                case 1: MedicalSupplyItem.Units = UnitsType.trake;    break;
                case 2: MedicalSupplyItem.Units = UnitsType.flasice;  break;
            }

            bool isItemIDUnique = CheckUniquenessOfNewItemID();

            if (isItemIDUnique)
                _medicalSupplyStorage.Save(MedicalSupplyItem);
            else
                MessageBox.Show("Već postoji stavka sa unetom oznakom!");
         
            NavigationService.Navigate(new DynamicInventoryView(MedicalSupplyItem.RoomID));
        }

        private bool CheckUniquenessOfNewItemID()
        {
            foreach (DynamicInventory ms in _medicalSupplyStorage.GetByRoomID(MedicalSupplyItem.RoomID))
            {
                if (ms.Id.Equals(MedicalSupplyItem.Id))
                    return false;
            }
            return true;
        }

        private void CancelButtonClick(object o, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DynamicInventoryView(MedicalSupplyItem.RoomID));
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DynamicInventoryView(MedicalSupplyItem.RoomID));
        }
    }
}
