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
using System.Windows.Shapes;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for PatientTherapy.xaml
    /// </summary>
    public partial class PatientTherapy : Window
    {
        private PatientTherapyMedicineNotification PatientTherapyMedicineNotification;
        public PatientTherapy(PatientTherapyMedicineNotification patientTherapyMedicineNotification)
        {
            InitializeComponent();
            this.PatientTherapyMedicineNotification = patientTherapyMedicineNotification;
            label1.Content = patientTherapyMedicineNotification.Name;
        }
    }
}
