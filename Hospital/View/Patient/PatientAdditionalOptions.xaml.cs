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
    /// Interaction logic for PatientAdditionalOptions.xaml
    /// </summary>
    public partial class PatientAdditionalOptions : Page
    {
        private PatientCommentsService patientCommentsService = new PatientCommentsService();
        public PatientAdditionalOptions()
        {
            InitializeComponent();
            if (!patientCommentsService.IsTimeForHospitalRating()) RateButton.Visibility = Visibility.Collapsed;
            NotesButton.Focus();
        }

        private void RateHospital(object sender, RoutedEventArgs e)
        {
            PatientRateDoctor patientRateDoctor = new PatientRateDoctor();
            this.NavigationService.Navigate(patientRateDoctor);
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PatientMenu());
        }

        public void Refresh()
        {
            if (!patientCommentsService.IsTimeForHospitalRating()) RateButton.Visibility = Visibility.Collapsed;
        }

        private void NotesButton_Click(object sender, RoutedEventArgs e)
        {
            PatientNotes patientNotes = new PatientNotes();
            this.NavigationService.Navigate(patientNotes);
        }
    }
}
