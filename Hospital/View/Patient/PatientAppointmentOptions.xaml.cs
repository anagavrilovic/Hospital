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
    /// Interaction logic for PatientAppointmentOptions.xaml
    /// </summary>
    public partial class PatientAppointmentOptions : Page
    {
        private Appointment selectedAppointment;
        private AppointmentService appointmentService = new AppointmentService();
        private PatientSettingsService patientSettingsService = new PatientSettingsService(new PatientSettingsFileFactory());
      

        public PatientAppointmentOptions(Appointment selectedAppointment)
        {
            InitializeComponent();
            this.selectedAppointment = selectedAppointment;
            ChangeAppointmentButton.Focus();
        }

        private void Reschedule(object sender, RoutedEventArgs e)
        {
            if (!IsGPAppointment())
            {
                MessageBox.Show("Moguće je pomeriti samo termine kod opšte prakse.");
            }
            else if (patientSettingsService.IsAntiTrollTriggered())
            {
                MessageBox.Show("Previše puta ste zakazali / pomerili termin u kratkom vremenskom periodu.");
            }
            else if (appointmentService.IsTooLateForAppointmentChange(selectedAppointment))
            {
                MessageBox.Show("Nije moguce otkazati termin tako kasno");

            }
            else
            {
                PatientMakeAppointment patientMakeAppointment = new PatientMakeAppointment(selectedAppointment);
                this.NavigationService.Navigate(patientMakeAppointment);
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            if (!appointmentService.IsTooLateForAppointmentChange(selectedAppointment))
            {
                if (MessageBox.Show("Da li ste sigurni da želite da otkažete termin?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) return;
               
                appointmentService.Delete(selectedAppointment.IDAppointment);
                NavigateBack();
            }
            else
            {
                MessageBox.Show("Nije moguce otkazati termin");
            }
        }

        private Boolean IsGPAppointment()
        {
            return (selectedAppointment.Doctor.Specialty == DoctorSpecialty.general);
        }


        private void NavigateBack()
        {
            PatientAppointments patientAppointments = new PatientAppointments();
            this.NavigationService.Navigate(patientAppointments);
        }
    }
}
