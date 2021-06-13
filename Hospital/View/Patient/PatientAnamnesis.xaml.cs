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
    /// Interaction logic for PatientAnamnesis.xaml
    /// </summary>
    public partial class PatientAnamnesis : Page
    {
        private ExaminationService examinationService = new ExaminationService();
        public PatientAnamnesis(Appointment appointment)
        {
            InitializeComponent();
            BackButton.Focus();
            TextBlock.Text = examinationService.GetExaminationByID(appointment.IDAppointment).anamnesis;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
