using Hospital.Model;
using Hospital.Services;
using Hospital.View.Manager;
using Hospital.ViewModels.Manager;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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

        private void ViewProfile(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerProfil());
        }

        private void ShowRooms(object sender, RoutedEventArgs e)
        {
            RoomsWindow rw = new RoomsWindow();
            NavigationService.Navigate(new RoomsWindow());
        }

        private void ShowMedicines(object sender, RoutedEventArgs e)
        {
            MedicinesWindow medicinesWindow = new MedicinesWindow();
            NavigationService.Navigate(medicinesWindow);
        }

        private void GenerateReport(object sender, RoutedEventArgs e)
        {
            MedicinesReport report = new MedicinesReport(new MedicinesReportViewModel(NavigationService));
            NavigationService.Navigate(report);
        }

        private void ShowTutorial(object sender, RoutedEventArgs e)
        {
            TutorialPage tutorial = new TutorialPage();
            NavigationService.Navigate(tutorial);
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerMainPage());
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
