using Hospital.Model;
using Hospital.Services;
using Hospital.View.Secretary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class IzmenaKartona : Page, INotifyPropertyChanged
    {
        private MedicalRecord patientsRecord;
        public MedicalRecord PatientsRecord
        {
            get { return patientsRecord; }
            set { patientsRecord = value; OnPropertyChanged("PatientsRecord"); }
        }

        public MedicalRecordService MedicalRecordService { get; set; }

        public IzmenaKartona(string selectedPatientID)
        {
            InitializeComponent();
            this.DataContext = this;
            this.MedicalRecordService = new MedicalRecordService();
            FillPatientsRecord(selectedPatientID);
        }

        private void FillPatientsRecord(string selectedPatientID)
        {
            this.PatientsRecord = MedicalRecordService.GetRecordByID(selectedPatientID);

            BracnoStanje.SelectedIndex = (int)PatientsRecord.Patient.MaritalStatus;
            Pol.SelectedIndex = (int)PatientsRecord.Patient.Gender;
            KrvnaGrupa.SelectedIndex = (int)PatientsRecord.BloodType;

            if (PatientsRecord.IsInsured)
                DaButton.IsChecked = true;
            else
                NeButton.IsChecked = true;
        }

        private void BtnPotvrdiClick(object sender, RoutedEventArgs e)
        {
            UpdatePatientsInsurance();

            if (!PatientsRecord.Patient.IsGuest)
            {
                if (PatientsRecord.Patient.FirstName.Equals("") || PatientsRecord.ParentName.Equals("") || PatientsRecord.Patient.LastName.Equals("") ||
                    PatientsRecord.Patient.Address.Street.Equals("") || PatientsRecord.Patient.Address.StreetNumber.Equals("") ||
                    PatientsRecord.Patient.Address.City.CityName.Equals("") || PatientsRecord.Patient.Address.City.PostalCode.Equals("") ||
                    PatientsRecord.Patient.Address.City.Country.CountryName.Equals("") || PatientsRecord.HealthCardNumber.Equals("") ||
                    PatientsRecord.Patient.CardID.Equals("") || PatientsRecord.Patient.PersonalID.Equals("") || PatientsRecord.Patient.PhoneNumber.Equals("") ||
                    PatientsRecord.Patient.Email.Equals("") || PatientsRecord.Patient.Username.Equals("") || PatientsRecord.Patient.Password.Equals(""))
                {
                    InformationBox informationBox = new InformationBox("Sva polja moraju biti popunjena!");
                    informationBox.Show();
                    return;
                }
            }
            else
            {
                if (PatientsRecord.Patient.FirstName.Equals("") || PatientsRecord.Patient.LastName.Equals("") ||
                    PatientsRecord.Patient.CardID.Equals("") || PatientsRecord.Patient.PersonalID.Equals(""))
                {
                    InformationBox informationBox = new InformationBox("Sva polja moraju biti popunjena!");
                    informationBox.Show();
                    return;
                }
            }

            MedicalRecordService.UpdateMedicalRecord(PatientsRecord);
            NavigationService.Navigate(new PrikazPacijenata());
        }

        private void UpdatePatientsInsurance()
        {
            if ((bool)DaButton.IsChecked)
                PatientsRecord.IsInsured = true;
            else
                PatientsRecord.IsInsured = false;
        }

        private void BtnOdustaniClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PrikazPacijenata());
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
