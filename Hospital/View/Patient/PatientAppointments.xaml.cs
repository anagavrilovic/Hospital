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
    /// Interaction logic for PatientAppointments.xaml
    /// </summary>
    public partial class PatientAppointments : Page
    {
     
        public PatientAppointments()
        {
            InitializeComponent();
            this.DataContext = new PatientAppointmentsViewModel();
            dataGridApp.SelectedIndex = 0;
            dataGridApp.Focus();
        }
        private void myTestKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                Appointment selectedItem = (Appointment)dataGridApp.SelectedItem;
                PatientAppointmentOptions patientAppointmentOptions = new PatientAppointmentOptions(selectedItem);
                this.NavigationService.Navigate(patientAppointmentOptions);
                
            }

            if (e.Key == Key.Escape)
            {
                this.NavigationService.Navigate(new PatientAppointmentMenu());
            }
        }

        public void Refresh()
        {
            //dataGridApp.ItemsSource = null;
           // dataGridApp.ItemsSource = Lista;
        }
    }
}
