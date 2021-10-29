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
    /// Interaction logic for PatientPassedAppointmentOptions.xaml
    /// </summary>
    public partial class PatientPassedAppointmentOptions : Page
    {
        private PatientCommentsService patientCommentsService = new PatientCommentsService(MainWindow.IDnumber);
        private ExaminationService examinationService = new ExaminationService(MainWindow.IDnumber);
        Appointment app;
        public PatientPassedAppointmentOptions(Appointment app)
        {
            InitializeComponent();
            this.app = app;
            RateDoctorButton.Focus();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void RateDoctor(object sender, RoutedEventArgs e)
        {
            if (patientCommentsService.IsAppointmentAlreadyGraded(app.IDAppointment))
            {
                MessageBox.Show("Već ste ocenili doktora za ovaj pregled.");
                return;
            }
            PatientRateDoctor patientRateDoctor = new PatientRateDoctor(app);
            this.NavigationService.Navigate(patientRateDoctor);
        }

        private void ViewAnamnesis(object sender, RoutedEventArgs e)
        {
            PatientAnamnesis patientAnamnesis = new PatientAnamnesis(app);
            this.NavigationService.Navigate(patientAnamnesis);
        }

        private void ViewDiagnosis(object sender, RoutedEventArgs e)
        {
            PatientDiagnosis patientDiagnosis = new PatientDiagnosis(app);
            this.NavigationService.Navigate(patientDiagnosis);
        }

        private void ViewTherapy(object sender, RoutedEventArgs e)
        {
            PatientsTherapy patientsTherapy = new PatientsTherapy(app);
            this.NavigationService.Navigate(patientsTherapy);
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da obrišete odabrani pregled?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) return;
            examinationService.PatientDelete(app.IDAppointment);
            this.NavigationService.Navigate(new PatientPassedAppointments());
        }
    }
}
