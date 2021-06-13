using Hospital.Factory;
using Hospital.Model;
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
        private PatientTherapyNotificationService patientTherapyNotificationService = new PatientTherapyNotificationService(new PatientTherapyNotificationFileFactory());
        private PatientNotesNotificationService patientNotesNotificationService = new PatientNotesNotificationService(new PatientNotesNotificationFileFactory());
        private NotificationService notificationService = new NotificationService();
        private MedicalRecordService medicalRecordService = new MedicalRecordService(new MedicalRecordFileFactory(), new AppointmentFileFactory(), new HospitalTreatmentFileFactory());
        public PatientMenu()
        {
            InitializeComponent();
            String username = medicalRecordService.GetUsernameByIDPatient(MainWindow.IDnumber);
            if (patientNotesNotificationService.IsThereNewNotification() || patientTherapyNotificationService.IsThereNewNotification() || notificationService.DoesUserHaveNewNotification(username)) MessageBox.Show("Imate novo obavestenje!");
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
