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
    /// Interaction logic for PatientPassedAppointments.xaml
    /// </summary>
    public partial class PatientPassedAppointments : Page
    {
       
        public PatientPassedAppointments()
        {
            InitializeComponent();
            this.DataContext = new PatientPassedAppointmentsViewModel();
            
            dataGridApp.SelectedIndex = 0;

            dataGridApp.Focus();
        }

        private void myTestKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                Appointment selectedItem = (Appointment)dataGridApp.SelectedItem;
                PatientPassedAppointmentOptions patientPassedAppointmentOptions = new PatientPassedAppointmentOptions(selectedItem);
                this.NavigationService.Navigate(patientPassedAppointmentOptions);

            }

            if (e.Key == Key.Escape)
            {
                this.NavigationService.GoBack();
            }
        }
    }
}
