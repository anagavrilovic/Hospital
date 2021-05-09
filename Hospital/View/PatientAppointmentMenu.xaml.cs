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
        public PatientAppointmentMenu()
        {
            InitializeComponent();
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
    }
}
