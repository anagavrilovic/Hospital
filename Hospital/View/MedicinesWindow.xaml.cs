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
using System.Windows.Shapes;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for MedicinesWindow.xaml
    /// </summary>
    public partial class MedicinesWindow : Window
    {
        public MedicinesWindow()
        {
            InitializeComponent();
        }

        private void addMedicine(object sender, RoutedEventArgs e)
        {
            AddMedicine addMedicine = new AddMedicine();
            addMedicine.Owner = Application.Current.MainWindow;
            addMedicine.Show();
        }

        private void editMedicine(object sender, RoutedEventArgs e)
        {

        }

        private void deleteMedicine(object sender, RoutedEventArgs e)
        {

        }

        private void medicinesRevision(object sender, RoutedEventArgs e)
        {

        }

        private void searchMedicine(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearchMouseDown(object sender, RoutedEventArgs e)
        {

        }

        private void back(object sender, RoutedEventArgs e)
        {

        }
    }
}


