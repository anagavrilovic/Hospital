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
    /// Interaction logic for PatientNoteAdd.xaml
    /// </summary>
    public partial class PatientNoteAdd : Page
    {
        private PatientNotesService patientNotesService=new PatientNotesService();
        public PatientNoteAdd()
        {
            InitializeComponent();
            SaveButton.Focus();
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PatientNotes());
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            String newID = patientNotesService.GetNewID();
            PatientNote patientNote = new PatientNote(newID,SubjectTextBox.Text,ContentTextBox.Text,MainWindow.IDnumber);
            patientNotesService.Save(patientNote);
            this.NavigationService.Navigate(new PatientNotes());
        }
    }
}
