using Hospital.Model;
using Hospital.Services;
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
