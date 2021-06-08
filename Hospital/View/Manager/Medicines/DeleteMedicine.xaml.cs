using Hospital.Services;
using Hospital.ViewModels.Manager;
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

        public DeleteMedicine(Medicine medicine)
        {
            InitializeComponent();
            MedicineForDeleting = medicine;
        }

        private void AcceptDeleting(object sender, RoutedEventArgs e)
        {
            MedicineService service = new MedicineService();
            service.DeleteMedicine(MedicineForDeleting);

            NavigationService.Navigate(new MedicinesWindow(new MedicinesWindowViewModel(NavigationService)));
        }

        private void CancelDeleting(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MedicinesWindow(new MedicinesWindowViewModel(NavigationService)));
        }
    }
}
