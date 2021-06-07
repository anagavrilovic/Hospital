using Hospital.Model;
using Hospital.Services;
using Hospital.View.Patient.Help;
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
using System.Windows.Shapes;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for PatientMain.xaml
    /// </summary>
    public partial class PatientMain : Window
    {
        private PatientSettingsService patientSettingsService = new PatientSettingsService();
        public PatientMain()
        {
            InitializeComponent();
            PatientSettings patientSettings = patientSettingsService.GetByID(MainWindow.IDnumber);
            if (patientSettings.ShowWizard || patientSettings.IsFirstLogin)
            {
                patientSettings.IsFirstLogin = false;
                patientSettingsService.Update(patientSettings);
                Help help = new Help();
                help.Show();
            }
            
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Help help = new Help();
            help.Show();
        }


    }
}
