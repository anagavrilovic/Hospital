using Hospital.Model;
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

namespace Hospital.View.Doctor
{
    /// <summary>
    /// Interaction logic for DisplayNotification.xaml
    /// </summary>
    public partial class DisplayNotification : Window
    {
        public Notification Notification { get; set; }

        public DisplayNotification(Notification selectedNotification)
        {
            InitializeComponent();
            this.DataContext = this;
            Notification = selectedNotification;
        }

        private void BtnNazadClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
