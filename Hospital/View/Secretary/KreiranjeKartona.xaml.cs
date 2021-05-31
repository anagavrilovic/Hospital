using Hospital.Model;
using Hospital.Services;
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
    public partial class KreiranjeKartona : Page, INotifyPropertyChanged
    {
        private MedicalRecord newRecord;
        public MedicalRecord NewRecord
        {
            get { return newRecord; }
            set { newRecord = value; OnPropertyChanged("NewRecord"); }
        }

        public MedicalRecordService MedicalRecordService { get; set; }


        public KreiranjeKartona()
        {
            InitializeComponent();
            this.DataContext = this;
            this.MedicalRecordService = new MedicalRecordService();
            InitializeNewRecord();
        }

        private void InitializeNewRecord()
        {
            NewRecord = new MedicalRecord();
            NewRecord.MedicalRecordID = MedicalRecordService.GenerateID();
        }

        private void BtnPotvrdiClick(object sender, RoutedEventArgs e)
        {
            SetPatientsInsurance();
            MedicalRecordService.RegisterNewRecord(NewRecord);
            NavigationService.Navigate(new PrikazPacijenata());
        }

        private void SetPatientsInsurance()
        {
            if ((bool)DaButton.IsChecked)
                NewRecord.IsInsured = true;
            else
                NewRecord.IsInsured = false;
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
