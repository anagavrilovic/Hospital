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
using System.Windows.Navigation;
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
            /*
            RoomRenovationStorage roomRenovationStorage = new RoomRenovationStorage();
            foreach (RoomRenovation renovation in roomRenovationStorage.GetAll())
            {
                if (renovation.StartDate < DateTime.Now)
                {
                    //renovation.Room = rs.GetOne(renovation.Room.Id);
                    renovation.Room.SerializeInfo = false;
                    renovation.WareHouse.SerializeInfo = false;
                    renovation.startRenovation();
                }
            }
            */
        }
        /*
        private void getRooms(object sender, RoutedEventArgs e)
        {
            RoomsWindow rw = new RoomsWindow();
            MainFrame.Navigate(new RoomsWindow());
        }

        private void getNotifications(object sender, RoutedEventArgs e)
        {
            ManagerNotifications obavestenja = new ManagerNotifications();
            obavestenja.Owner = Application.Current.MainWindow;
            obavestenja.Show();
        }

        private void getMedicines(object sender, RoutedEventArgs e)
        {
            MedicinesWindow medicinesWindow = new MedicinesWindow();
            medicinesWindow.Owner = Application.Current.MainWindow;
            medicinesWindow.Show();
        }
        */
        private void back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

  
