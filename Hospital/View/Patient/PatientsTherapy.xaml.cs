using Hospital.Factory;
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
    /// Interaction logic for PatientsTherapy.xaml
    /// </summary>
    public partial class PatientsTherapy : Page
    {
        private ExaminationService examinationService = new ExaminationService(new MedicalRecordFileFactory());
        private Therapy therapy;
        public PatientsTherapy(Appointment appointment)
        {
            InitializeComponent();
            therapy = examinationService.GetExaminationByID(appointment.IDAppointment).therapy;
            Label.Content = therapy.name;
            TextBlock.Text = therapy.description;
            BackButton.Focus();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void ViewMedications(object sender, RoutedEventArgs e)
        {
            PatientTherapysMedications patientTherapysMedications = new PatientTherapysMedications(therapy.Medicine);
            this.NavigationService.Navigate(patientTherapysMedications);
        }


    }
}
