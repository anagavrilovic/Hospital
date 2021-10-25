using Hospital.Commands.DoctorCommands;
using Hospital.Factory;
using Hospital.Model;
using Hospital.Services;
using Hospital.View;
using Hospital.View.Secretary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Hospital.ViewModels.Secretary
{
    public class UpdateMedicalRecordViewModel : ViewModel
    {
        #region Properties

        private MedicalRecord patientsRecord;
        public MedicalRecord PatientsRecord
        {
            get { return patientsRecord; }
            set { patientsRecord = value; OnPropertyChanged("PatientsRecord"); }
        }

        private int maritalStatus;
        public int MaritalStatus
        {
            get { return maritalStatus; }
            set { maritalStatus = value; OnPropertyChanged("MaritalStatus"); }
        }

        private int gender;
        public int Gender
        {
            get { return gender; }
            set { gender = value; OnPropertyChanged("Gender"); }
        }

        private int bloodType;
        public int BloodType
        {
            get { return bloodType; }
            set { bloodType = value; OnPropertyChanged("BloodType"); }
        }


        public MedicalRecordService MedicalRecordService { get; set; }
        public NavigationService NavigationService { get; set; }

        #endregion

        #region Konstruktor

        public UpdateMedicalRecordViewModel(NavigationService navigation, string selectedPatientID)
        {
            this.NavigationService = navigation;
            MedicalRecordService = new MedicalRecordService();
            UpdatePatientCommand = new RelayCommand(Execute_UpdatePatientCommand, CanExecuteCommands);
            CancelCommand = new RelayCommand(Execute_CancelCommand, CanExecuteCommands);
            FillPatientsRecord(selectedPatientID); 
        }


        #endregion

        #region Metode

        private void FillPatientsRecord(string selectedPatientID)
        {
            this.PatientsRecord = MedicalRecordService.GetRecordByID(selectedPatientID);
            MaritalStatus = (int)PatientsRecord.Patient.MaritalStatus;
            BloodType = (int)PatientsRecord.BloodType;
            Gender = (int)PatientsRecord.Patient.Gender;
        }

        private void UpdatePatientsRecord()
        {
            PatientsRecord.Patient.MaritalStatus = (MaritalStatus)MaritalStatus;
            PatientsRecord.Patient.Gender = (Genders)Gender;
            PatientsRecord.BloodType = (BloodType)BloodType;
        }

        #endregion

        #region Komande

        public RelayCommand UpdatePatientCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        #endregion

        #region Akcije

        public void Execute_UpdatePatientCommand(object obj)
        {
            UpdatePatientsRecord();

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
            this.NavigationService.Navigate(new AllPatients());
        }

        public bool CanExecuteCommands(object obj)
        {
            return true;
        }

        public void Execute_CancelCommand(object obj)
        {
            this.NavigationService.Navigate(new AllPatients());
        }

        #endregion
    }
}
