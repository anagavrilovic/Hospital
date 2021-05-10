using System;
using System.Collections.Generic;
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
    public partial class DeleteMedicine : Page
    {
        public Medicine MedicineForDeleting { get; set; }
        private MedicineStorage _medicineStorage;

        public DeleteMedicine(Medicine medicine)
        {
            InitializeComponent();
            MedicineForDeleting = medicine;
            _medicineStorage = new MedicineStorage();
        }

        private void AcceptDeleting(object sender, RoutedEventArgs e)
        {
            _medicineStorage.Delete(MedicineForDeleting.ID);

            NavigationService.Navigate(new MedicinesWindow());
        }

        private void CancelDeleting(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MedicinesWindow());
        }
    }
}
