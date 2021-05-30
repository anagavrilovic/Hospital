﻿using Hospital.Model;
using Hospital.Services;
using Hospital.ViewModels.Patient;
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
    /// <summary>
    /// Interaction logic for PatientMenu.xaml
    /// </summary>
    public partial class PatientMenu : Page
    {
        private PatientTherapyNotificationService patientNotificationService = new PatientTherapyNotificationService();
        private PatientNotesNotificationService patientNotesNotificationService = new PatientNotesNotificationService();
        public PatientMenu()
        {
            
            InitializeComponent();
            Boolean newNotification = false;
            if (patientNotesNotificationService.IsThereNewNotification()) newNotification = true;
           
 
            ObservableCollection<PatientTherapyMedicineNotification> lista = new ObservableCollection<PatientTherapyMedicineNotification>(patientNotificationService.GetByPatientID());
            if (lista != null)
            {
                foreach (PatientTherapyMedicineNotification ptm in lista)
                {
                    if ((ptm.FromDate.Date <= DateTime.Now.Date) && (ptm.ToDate.Date >= DateTime.Now.Date))
                    {
                        if (ptm.LastRead.Date <= DateTime.Now.Date)
                        {
                            string[] times = ptm.Times.Split(' ');
                            for (int i = 0; i < times.Length - 1; i += 2)
                            {

                                TimeSpan ts = TimeSpan.Parse(times[i]);


                                DateTime dt = DateTime.Now.Date + ts;
                                if (times[i + 1].Equals("PM")) dt = dt.AddHours(12);
                                if ((ptm.LastRead < dt) && (dt <= DateTime.Now))
                                {
                                    newNotification = true;
                                    ptm.Read = false;
                                    patientNotificationService.Update(ptm);
                                }
                            }
                        }
                    }
                }
                if (newNotification)
                {
                    MessageBox.Show("Imate novo obavestenje!");
                }
            }
            NotificationButton.Focus();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
        }

        private void Settings(object sender, RoutedEventArgs e)
        {
            PatientSettingsPageViewModel vm = new PatientSettingsPageViewModel(this.NavigationService);
            PatientSettingsPage patientSettings = new PatientSettingsPage(vm);
            this.NavigationService.Navigate(patientSettings);
        }

        private void Notifications(object sender, RoutedEventArgs e)
        {
            PatientNotifications patientNotifications = new PatientNotifications();
            this.NavigationService.Navigate(patientNotifications);
        }

        private void Appointments(object sender, RoutedEventArgs e)
        {
            PatientAppointmentMenu patientAppointmentMenu=new PatientAppointmentMenu();
            this.NavigationService.Navigate(patientAppointmentMenu);
        }

        private void ViewAdditionalOptions(object sender, RoutedEventArgs e)
        {
            PatientAdditionalOptions patientAdditionalOptions = new PatientAdditionalOptions();
            this.NavigationService.Navigate(patientAdditionalOptions);
        }
    }
}
