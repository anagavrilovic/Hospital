using Hospital.Factory;
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
    /// <summary>
    /// Interaction logic for PatientAppointmentMenu.xaml
    /// </summary>
    public partial class PatientAppointmentMenu : Page
    {
        private PatientSettingsService patientSettingsService = new PatientSettingsService(new PatientSettingsFileFactory());
        public PatientAppointmentMenu()
        {
            InitializeComponent();
            ScheduleButton.Focus();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PatientMenu());
        }

        private void PassedAppointments(object sender, RoutedEventArgs e)
        {
            PatientPassedAppointments patientPassedAppointments = new PatientPassedAppointments();
            this.NavigationService.Navigate(patientPassedAppointments);
        }

        private void Appointments(object sender, RoutedEventArgs e)
        {
            PatientAppointments patientAppointments = new PatientAppointments();
            this.NavigationService.Navigate(patientAppointments);
        }

        private void MakeAnAppointment(object sender, RoutedEventArgs e)
        {
            if (patientSettingsService.IsAntiTrollTriggered())
            {
                MessageBox.Show("Previše puta ste zakazali/pomerili termin u kratkom vremenskom periodu.");
                return;
            }
            PatientMakeAppointment patientMakeAppointment = new PatientMakeAppointment();
            this.NavigationService.Navigate(patientMakeAppointment);
        }
    }
}
