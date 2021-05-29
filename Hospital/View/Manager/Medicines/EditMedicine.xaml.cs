using Hospital.Services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View
{
    public partial class EditMedicine : Page
    {
        public Medicine Medicine { get; set; }

        public EditMedicine(Medicine medicine)
        {
            InitializeComponent();
            this.DataContext = this;

            Medicine = medicine;
        }
     
        private void AcceptEditingMedicine(object sender, RoutedEventArgs e)
        {
            priceTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            quantityTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            MedicineService service = new MedicineService();
            service.EditMedicine(Medicine);

            NavigationService.Navigate(new MedicinesWindow());
        }

        private void CancelEditingMedicine(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MedicinesWindow());
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MedicinesWindow());
        }
    }
}
