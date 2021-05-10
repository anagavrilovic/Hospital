using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
    public partial class KreiranjeKartona : Page
    {
        private MedicalRecord record = new MedicalRecord();

        public MedicalRecord Record
        {
            get { return record; }
            set { record = value; }
        }


        public KreiranjeKartona()
        {
            InitializeComponent();
            this.DataContext = this;
            Record.MedicalRecordID = Record.GetHashCode().ToString();
        }

        private void BtnPotvrdiClick(object sender, RoutedEventArgs e)
        {
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

            switch (KrvnaGrupa.SelectedIndex)
            {
                case 0: Record.BloodType = BloodType.Aplus; break;
                case 1: Record.BloodType = BloodType.Aneg; break;
                case 2: Record.BloodType = BloodType.Bplus; break;
                case 3: Record.BloodType = BloodType.Bneg; break;
                case 4: Record.BloodType = BloodType.ABplus; break;
                case 5: Record.BloodType = BloodType.ABneg; break;
                case 6: Record.BloodType = BloodType.Oplus; break;
                case 7: Record.BloodType = BloodType.Oneg; break;
            }

            if ((bool)DaButton.IsChecked)
            {
                Record.IsInsured = true;
            }
            else
            {
                Record.IsInsured = false;
            }

            RegistratedUser ru = new RegistratedUser { Username = Record.Patient.Username, Password = Record.Patient.Password, Type = UserType.patient };
            RegistratedUserStorage rus = new RegistratedUserStorage();
            rus.Save(ru);

            MedicalRecordStorage mds = new MedicalRecordStorage();
            mds.Save(Record);
            NavigationService.Navigate(new PrikazPacijenata());
        }

        private void BtnOdustaniClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PrikazPacijenata());
        }

    }
}
