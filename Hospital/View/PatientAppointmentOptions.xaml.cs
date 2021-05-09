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
    /// Interaction logic for PatientAppointmentOptions.xaml
    /// </summary>
    public partial class PatientAppointmentOptions : Page
    {
        private Appointment selectedAppointment;
        private PatientSettingsStorage patientSettingsStorage = new PatientSettingsStorage();
        private DoctorStorage doctorStorage = new DoctorStorage();
        private AppointmentStorage appointmentStorage = new AppointmentStorage();
        private const int MINIMUM_DAYS_DIFFERENCE = 2;

        public PatientAppointmentOptions(Appointment selectedAppointment)
        {
            InitializeComponent();
            this.selectedAppointment = selectedAppointment;
        }

        private void Reschedule(object sender, RoutedEventArgs e)
        {
            if (!IsGPAppointment())
            {
                MessageBox.Show("Moguće je pomeriti samo termine kod opšte prakse.");
            }
            else if (patientSettingsStorage.IsAntiTrollTriggered())
            {
                MessageBox.Show("Previše puta ste zakazali / pomerili termin u kratkom vremenskom periodu.");
            }
            else if (IsTooLateForAppointmentChange())
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
            if (!IsTooLateForAppointmentChange())
            {
                appointmentStorage.Delete(selectedAppointment.IDAppointment);
                NavigateBack();
            }
            else
            {
                MessageBox.Show("Nije moguce otkazati termin");
            }
        }

        private Boolean IsGPAppointment()
        {
            Hospital.Model.Doctor doctor = doctorStorage.GetOne(selectedAppointment.IDDoctor);
            return (doctor.Specialty == DoctorSpecialty.general);
        }

        private Boolean IsTooLateForAppointmentChange()
        {
            return !((selectedAppointment.DateTime - DateTime.Now).TotalDays >= MINIMUM_DAYS_DIFFERENCE);
        }

        private void NavigateBack()
        {
            PatientAppointments patientAppointments = new PatientAppointments();
            patientAppointments.Refresh();
            this.NavigationService.Navigate(patientAppointments);
        }
    }
}
