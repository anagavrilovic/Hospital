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
    /// Interaction logic for PatientAdditionalOptions.xaml
    /// </summary>
    public partial class PatientAdditionalOptions : Page
    {
        private PatientCommentsStorage patientCommentsStorage = new PatientCommentsStorage();
        public PatientAdditionalOptions()
        {
            InitializeComponent();
            if (!patientCommentsStorage.IsTimeForHospitalRating()) RateButton.Visibility = Visibility.Collapsed;
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
            if (!patientCommentsStorage.IsTimeForHospitalRating()) RateButton.Visibility = Visibility.Collapsed;
        }

        private void NotesButton_Click(object sender, RoutedEventArgs e)
        {
            PatientNotes patientNotes = new PatientNotes();
            this.NavigationService.Navigate(patientNotes);
        }
    }
}
