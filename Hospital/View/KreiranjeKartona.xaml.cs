using Hospital.Model;
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

            switch (BracnoStanje.SelectedIndex)
            {
                case 0: Record.Patient.MaritalStatus = MaritalStatus.neozenjen; break;
                case 1: Record.Patient.MaritalStatus = MaritalStatus.ozenjen; break;
                case 2: Record.Patient.MaritalStatus = MaritalStatus.udovac; break;
                case 3: Record.Patient.MaritalStatus = MaritalStatus.razveden; break;
            }

            switch (Pol.SelectedIndex)
            {
                case 0: Record.Patient.Gender = Genders.male; break;
                case 1: Record.Patient.Gender = Genders.female; break;
                case 2: Record.Patient.Gender = Genders.other; break;
            }

            if ((bool)DaButton.IsChecked)
            {
                Record.IsInsured = true;
            }
            else
            {
                Record.IsInsured = false;
            }

            RegistratedUser ru = new RegistratedUser { Username = UsernameText.Text, Password = PasswordText.Password, Type = UserType.patient };
            RegistratedUserStorage rus = new RegistratedUserStorage();
            rus.Save(ru);

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
