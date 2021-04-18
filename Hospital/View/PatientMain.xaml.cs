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
using System.Windows.Shapes;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for PatientMain.xaml
    /// </summary>
    public partial class PatientMain : Window
    {
        public PatientMain()
        {
            InitializeComponent();
        }

        private void AppointmentList(object sender, RoutedEventArgs e)
        {
            PatientAppointmentList patientAppointmentList = new PatientAppointmentList();
            patientAppointmentList.Show();
        }

        private void MakeAnAppointment(object sender, RoutedEventArgs e)
        {
            PatientChooseDatesForAnAppointment patientChooseDatesForAnAppointment = new PatientChooseDatesForAnAppointment();
            patientChooseDatesForAnAppointment.Show();
        }

        private void PassedAppointmentList(object sender, RoutedEventArgs e)
        {
            PatientPassedAppointmentList patientPassedAppointmentList = new PatientPassedAppointmentList();
            patientPassedAppointmentList.Show();
        }

        private void Settings(object sender, RoutedEventArgs e)
        {
            PatientSettingsPage patientSettings = new PatientSettingsPage();
            patientSettings.Show();
        }
    }
}
