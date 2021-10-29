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
using System.Windows.Controls.Primitives;
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
        private PatientNotesService patientNotesService = new PatientNotesService();
        private PatientSettingsService patientSettingsService = new PatientSettingsService(MainWindow.IDnumber);
        public PatientNoteAdd()
        {
            InitializeComponent();
            SubjectTextBox.Focus();
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PatientNotes());
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            String newID = patientNotesService.GetNewID();
            PatientNote patientNote = new PatientNote(newID, SubjectTextBox.Text, ContentTextBox.Text, MainWindow.IDnumber);
            patientNotesService.Save(patientNote);
            this.NavigationService.Navigate(new PatientNotes());
        }

        private void myTestKey(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Escape) && SubjectTextBox.IsFocused)
            {
                Keyboard.ClearFocus();
                ContentTextBox.Focus();
            }

            else if ((e.Key == Key.Escape) && ContentTextBox.IsFocused)
            {
                Keyboard.ClearFocus();
                SaveButton.Focus();
            }
        }

        public void ShowToolTip(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!patientSettingsService.GetByID(MainWindow.IDnumber).ShowTooltips) return;
            ToolTip tt = (ToolTip)(sender as Control).ToolTip;
            tt.PlacementTarget = (UIElement)sender;
            tt.Placement = PlacementMode.Right;
            tt.IsOpen = (sender as Control).IsKeyboardFocusWithin;
        }
    }
}
