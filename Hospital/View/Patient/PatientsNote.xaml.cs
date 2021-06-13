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
    /// Interaction logic for PatientsNote.xaml
    /// </summary>
    public partial class PatientsNote : Page
    {
        
        public PatientsNote(PatientNote patientNote)
        {
            InitializeComponent();
            BackButton.Focus();
            SubjectLabel.Content = patientNote.Subject;
            TextBlock.Text = patientNote.Text;
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
