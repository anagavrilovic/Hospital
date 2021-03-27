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
    /// Interaction logic for KreiranjeKartona.xaml
    /// </summary>
    public partial class KreiranjeKartona : Window
    {
        private ObservableCollection<MedicalRecord> md;
        private MedicalRecord record = new MedicalRecord();
        private Patient patient = new Patient();

        public ObservableCollection<MedicalRecord> Md
        {
            get => md;
            set
            {
                md = value;
            }
        }

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

        public KreiranjeKartona(ObservableCollection<MedicalRecord> md)
        {
            InitializeComponent();
            this.DataContext = this;
            this.md = md;
        }

        private void BtnPotvrdi(object sender, RoutedEventArgs e)
        {
            
            Patient.Password = PasswordText.Password;

            switch (BracnoStanje.SelectedItem.ToString())
            {
                case "neoženjen - neudata": Patient.MaritalStatus = MaritalStatus.neozenjen; break;
                case "oženjen - udata": Patient.MaritalStatus = MaritalStatus.ozenjen; break;
                case "udovac - udovica": Patient.MaritalStatus = MaritalStatus.udovac; break;
                case "razveden - razvedena": Patient.MaritalStatus = MaritalStatus.razveden; break;
            }

            switch (Pol.SelectedItem.ToString())
            {
                case "muški": Patient.Gender = Genders.male; break;
                case "ženski": Patient.Gender = Genders.female; break;
                case "ostalo": Patient.Gender = Genders.other; break;
            }

            if (!(bool)DaButton.IsChecked)
            {
                Record.IsInsured = false;
            }

            Record.Patient = this.Patient;

            Md.Add(Record);
            this.Close();
        }

        private void BtnOdustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
