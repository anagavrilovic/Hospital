using Hospital.View;
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

namespace Hospital
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SecretaryClick(object sender, RoutedEventArgs e)
        {
            var s = new SecretaryWindow();
            s.Show();
        }

        private void DoctorClick(object sender, RoutedEventArgs e)
        {

        }

        private void ManagerClick(object sender, RoutedEventArgs e)
        {

        }

        private void PatientClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
