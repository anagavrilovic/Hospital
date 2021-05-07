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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View
{
    public partial class EditMedicalSupply : Page
    {
        public MedicalSupply MedicalSupplyItem { get; set; }

        public EditMedicalSupply(MedicalSupply supply)
        {
            InitializeComponent();
            this.DataContext = this;
            MedicalSupplyItem = supply;

            switch (MedicalSupplyItem.Units)
            {
                case UnitsType.kutije:  units.SelectedItem = kutije;  break;
                case UnitsType.trake:   units.SelectedItem = trake;   break;
                case UnitsType.flasice: units.SelectedItem = flasice; break;
            }
        }

        private void AcceptButtonClick(object sender, RoutedEventArgs e)
        {
            oznakaTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            nazivTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            cenaTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            kolicinaTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            switch (units.SelectedIndex)
            {
                case 0: MedicalSupplyItem.Units = UnitsType.kutije;  break;
                case 1: MedicalSupplyItem.Units = UnitsType.trake;   break;
                case 2: MedicalSupplyItem.Units = UnitsType.flasice; break;
            }

            MedicalSupplyStorage medicalSupplyStorage = new MedicalSupplyStorage();
            medicalSupplyStorage.doSerialization();

            NavigationService.Navigate(new DynamicInventory(MedicalSupplyItem.RoomID));
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DynamicInventory(MedicalSupplyItem.RoomID));
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DynamicInventory(MedicalSupplyItem.RoomID));
        }
    }
}
