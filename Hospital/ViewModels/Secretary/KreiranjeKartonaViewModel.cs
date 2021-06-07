using Hospital.Commands.Secretary;
using Hospital.Services;
using Hospital.View;
using Hospital.View.Secretary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Hospital.ViewModels.Secretary
{
    public class KreiranjeKartonaViewModel : ViewModel
    {
        #region Properties

        private MedicalRecord newRecord;
        public MedicalRecord NewRecord
        {
            get { return newRecord; }
            set { newRecord = value; OnPropertyChanged("NewRecord"); }
        }

        public MedicalRecordService MedicalRecordService { get; set; }
        public NavigationService NavigationService { get; set; }

        #endregion

        #region Konstruktor

        public KreiranjeKartonaViewModel(NavigationService navigation)
        {
            this.NavigationService = navigation;
            this.MedicalRecordService = new MedicalRecordService();
            AddStudentCommand = new RelayCommand(Execute_AddStudentCommand, CanExecuteCommands);
            CancelCommand = new RelayCommand(Execute_CancelCommand, CanExecuteCommands);
            InitializeNewRecord();
        }

        #endregion

        #region Metode

        private void InitializeNewRecord()
        {
            NewRecord = new MedicalRecord();
            NewRecord.MedicalRecordID = MedicalRecordService.GenerateID();
        }

        #endregion

        #region Komande

        public RelayCommand AddStudentCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        #endregion

        #region Akcije

        public void Execute_AddStudentCommand(object obj)
        {
            if (!NewRecord.Patient.IsGuest)
            {
                if(NewRecord.Patient.FirstName.Equals("") || NewRecord.ParentName.Equals("") || NewRecord.Patient.LastName.Equals("") ||
                    NewRecord.Patient.Address.Street.Equals("") || NewRecord.Patient.Address.StreetNumber.Equals("") || 
                    NewRecord.Patient.Address.City.CityName.Equals("") || NewRecord.Patient.Address.City.PostalCode.Equals("") ||
                    NewRecord.Patient.Address.City.Country.CountryName.Equals("") || NewRecord.HealthCardNumber.Equals("") || 
                    NewRecord.Patient.CardID.Equals("") || NewRecord.Patient.PersonalID.Equals("") || NewRecord.Patient.PhoneNumber.Equals("") || 
                    NewRecord.Patient.Email.Equals("") || NewRecord.Patient.Username.Equals("") || NewRecord.Patient.Password.Equals(""))
                {
                    InformationBox informationBox = new InformationBox("Sva polja moraju biti popunjena!");
                    informationBox.Show();
                    return;
                }
            } 
            else
            {
                if (NewRecord.Patient.FirstName.Equals("") || NewRecord.Patient.LastName.Equals("") ||
                    NewRecord.Patient.CardID.Equals("") || NewRecord.Patient.PersonalID.Equals(""))
                {
                    InformationBox informationBox = new InformationBox("Sva polja moraju biti popunjena!");
                    informationBox.Show();
                    return;
                }
            }

            MedicalRecordService.RegisterNewRecord(NewRecord);
            this.NavigationService.Navigate(new PrikazPacijenata());
        }

        public bool CanExecuteCommands(object obj)
        {
            return true;
        }

        public void Execute_CancelCommand(object obj)
        {
            this.NavigationService.Navigate(new PrikazPacijenata());
        }

        #endregion
    }
}
