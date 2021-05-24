using Hospital.Model;
using Hospital.Services;
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
    public partial class ManagerMainPage : Page
    {
        public ManagerMainPage()
        {
            InitializeComponent();
            CheckScheduledRenovationsStatus();
        }

        private void CheckScheduledRenovationsStatus()
        {
            RoomRenovationService renovationService;
            RoomRenovationStorage roomRenovationStorage = new RoomRenovationStorage();
            foreach (RoomRenovation renovation in roomRenovationStorage.GetAll())
            {
                if (renovation.StartDate < DateTime.Now)
                {
                    renovation.Room.SerializeInfo = false;
                    renovation.WareHouse.SerializeInfo = false;
                    renovationService = new RoomRenovationService(renovation);
                    renovationService.ScheduleRenovation();
                }
            }
        }

        private void ShowRooms(object sender, RoutedEventArgs e)
        {
            RoomsWindow rw = new RoomsWindow();
            NavigationService.Navigate(new RoomsWindow());
        }

        private void ShowNotifications(object sender, RoutedEventArgs e)
        {
            ManagerNotifications notifications = new ManagerNotifications();
            NavigationService.Navigate(notifications);
        }

        private void ShowMedicines(object sender, RoutedEventArgs e)
        {
            MedicinesWindow medicinesWindow = new MedicinesWindow();
            NavigationService.Navigate(medicinesWindow);
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerMainPage());
        }
    }
}
