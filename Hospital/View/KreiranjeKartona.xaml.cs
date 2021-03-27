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
        private ObservableCollection<MedicalRecord> md = new ObservableCollection<MedicalRecord>();
        private MedicalRecord record = new MedicalRecord();

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

        public KreiranjeKartona(ObservableCollection<MedicalRecord> md)
        {
            InitializeComponent();
            this.DataContext = this;
            if (md != null)
            {
                this.md = md;
            }
        }

        private void BtnPotvrdi(object sender, RoutedEventArgs e)
        {
            Record.Patient.Password = PasswordText.Password;

            switch (BracnoStanje.SelectedItem.ToString())
            {
                case "neoženjen - neudata": Record.Patient.MaritalStatus = MaritalStatus.neozenjen; break;
                case "oženjen - udata": Record.Patient.MaritalStatus = MaritalStatus.ozenjen; break;
                case "udovac - udovica": Record.Patient.MaritalStatus = MaritalStatus.udovac; break;
                case "razveden - razvedena": Record.Patient.MaritalStatus = MaritalStatus.razveden; break;
            }

            switch (Pol.SelectedItem.ToString())
            {
                case "muški": Record.Patient.Gender = Genders.male; break;
                case "ženski": Record.Patient.Gender = Genders.female; break;
                case "ostalo": Record.Patient.Gender = Genders.other; break;
            }

            if ((bool)DaButton.IsChecked)
            {
                Record.IsInsured = true;
            }
            else
            {
                Record.IsInsured = false;
            }

            Md.Add(Record);
            MedicalRecordStorage mds = new MedicalRecordStorage();
            mds.Save(Record);
            this.Close();
        }

        private void BtnOdustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
