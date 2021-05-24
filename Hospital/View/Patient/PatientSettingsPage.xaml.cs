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
    /// Interaction logic for PatientSettingsPage.xaml
    /// </summary>
    public partial class PatientSettingsPage : Page
    {
        public PatientSettingsPage()
        {
            InitializeComponent();
            PatientSettingsStorage patientSettingsStorage = new PatientSettingsStorage();
            PatientSettings patientSettings = patientSettingsStorage.getByID(MainWindow.IDnumber);
            if (patientSettings == null)
            {
                lekar3.IsChecked = true;
            }
            else if (lekar1.Content.ToString().Equals(patientSettings.ChosenDoctor))
            {
                lekar1.IsChecked = true;
            }
            else if (lekar2.Content.ToString().Equals(patientSettings.ChosenDoctor))
            {
                lekar2.IsChecked = true;
            }
            else if (lekar3.Content.ToString().Equals(patientSettings.ChosenDoctor))
            {
                lekar3.IsChecked = true;
            }
            BackButton.Focus();
        }


        private void Nazad(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Sacuvaj(object sender, RoutedEventArgs e)
        {
            PatientSettingsStorage patientSettingsStorage = new PatientSettingsStorage();
            PatientSettings patientSettings = new PatientSettings();
            patientSettings.IDPatient = MainWindow.IDnumber;
            patientSettingsStorage.Delete(MainWindow.IDnumber);
            if (lekar1.IsChecked == true)
            {
                patientSettings.ChosenDoctor = lekar1.Content.ToString();
            }
            else if (lekar2.IsChecked == true)
            {
                patientSettings.ChosenDoctor = lekar2.Content.ToString();
            }
            else if (lekar3.IsChecked == true)
            {
                patientSettings.ChosenDoctor = lekar3.Content.ToString();
            }

            patientSettingsStorage.Save(patientSettings);
            this.NavigationService.GoBack();
        }

    }
}
