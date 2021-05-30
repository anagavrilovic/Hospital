using Hospital.Model;
using Hospital.ViewModels.Patient;
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
    /// Interaction logic for PatientNotes.xaml
    /// </summary>
    public partial class PatientNotes : Page
    {
        public PatientNotes()
        {
            InitializeComponent();
            this.DataContext = new PatientNotesViewModel();
            dataGridApp.SelectedIndex = 0;
            dataGridApp.Focus();
        }

        private void myTestKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Add)
            {
                this.NavigationService.Navigate(new PatientNoteAdd());

            }

            if (e.Key == Key.Escape)
            {
                this.NavigationService.Navigate(new PatientAdditionalOptions());
            }

            if (e.Key == Key.Space)
            {
                PatientNote selectedItem = (PatientNote)dataGridApp.SelectedItem;
                this.NavigationService.Navigate(new PatientNoteOptions(selectedItem));
            }
        }
    }
}
