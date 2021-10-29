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
    /// Interaction logic for PatientDiagnosis.xaml
    /// </summary>
    public partial class PatientDiagnosis : Page
    {
        private ExaminationService examinationService = new ExaminationService(MainWindow.IDnumber);
        public PatientDiagnosis(Appointment appointment)
        {
            InitializeComponent();
            BackButton.Focus();
            TextBlock.Text = examinationService.GetExaminationByID(appointment.IDAppointment).diagnosis;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
