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
    /// Interaction logic for PatientNoteOptions.xaml
    /// </summary>
    public partial class PatientNoteOptions : Page
    {
        private PatientNote patientNote;
        public PatientNoteOptions(PatientNote patientNote)
        {
            InitializeComponent();
            this.patientNote = patientNote;
            ShowButton.Focus();
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PatientNotes());
        }

        private void ShowNote(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PatientsNote(patientNote));
        }

        private void AddNotification(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PatientNotificationAdd(patientNote));
        }
    }
}
