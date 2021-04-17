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
    /// Interaction logic for MenagerWindow.xaml
    /// </summary>
    public partial class MenagerWindow : Window
    {
        public MenagerWindow()
        {
            InitializeComponent();
        }

        private void getRooms(object sender, RoutedEventArgs e)
        {
            RoomsWindow rw = new RoomsWindow();
            rw.Owner = Application.Current.MainWindow;
            rw.Show();
        }

        private void getEmployees(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void back(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }


    }
}

  
