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
    /// Interaction logic for IzmenaKartona.xaml
    /// </summary>
    public partial class IzmenaKartona : Window
    {
        private MedicalRecord record;
        private Patient patient;

        public MedicalRecord Record
        {
            get => record;
            set
            {
                record = value;
            }
        }
        public Patient Patient
        {
            get => patient;
            set
            {
                patient = value;
            }
        }

        public IzmenaKartona(MedicalRecord mr)
        {
            InitializeComponent();
            this.DataContext = this;
            this.record = mr;
            this.patient = mr.Patient;
        }

        private void BtnPotvrdi(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOdustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
