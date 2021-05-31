using Hospital.DTO;
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
    public partial class IzmenaObavestenja : Page, INotifyPropertyChanged
    {
        private Notification notification;
        public Notification Notification
        {
            get { return notification; }
            set { notification = value; OnPropertyChanged("Notification"); }
        }

        public NotificationRecipientsDTO Recipients { get; set; }

        public ObservableCollection<MedicalRecord> AllMedicalRecords { get; set; }
        public ICollectionView PatientsRecordsCollection { get; set; }

        public MedicalRecord SelectedRecordForAdding { get; set; }
        public MedicalRecord SelectedRecordForRemoving { get; set; }

        public NotificationService NotificationService { get; set; }
        public MedicalRecordService MedicalRecordService { get; set; }


        public IzmenaObavestenja(Notification selectedNotification)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Notification = selectedNotification;
            InitializeEmptyProperties();
            CheckExistingNotificationRecipients();
            LoadExistingPatientsRecipiants();
        }

        private void InitializeEmptyProperties()
        {
            this.NotificationService = new NotificationService();
            this.MedicalRecordService = new MedicalRecordService();
            this.AllMedicalRecords = new ObservableCollection<MedicalRecord>();
            this.Recipients = new NotificationRecipientsDTO();
        }

        private void CheckExistingNotificationRecipients()
        {
            Recipients.IsEverySecretaryRecipient = NotificationService.IsEveryUserOfRoleReceivingNotification(Notification, UserType.secretary);
            Recipients.IsEveryDoctorRecipient = NotificationService.IsEveryUserOfRoleReceivingNotification(Notification, UserType.doctor);
            Recipients.IsEveryManagerRecipient = NotificationService.IsEveryUserOfRoleReceivingNotification(Notification, UserType.manager);
            Recipients.IsEveryPatientRecipient = NotificationService.IsEveryUserOfRoleReceivingNotification(Notification, UserType.patient);
        }

        private void LoadExistingPatientsRecipiants()
        {
            if (Recipients.IsEveryPatientRecipient)
            {
                Recipients.RecipientsRecords = new ObservableCollection<MedicalRecord>(MedicalRecordService.GetAllRecords());
                return;
            }

            Recipients.RecipientsRecords = new ObservableCollection<MedicalRecord>(NotificationService.GetPatientRecipientsRecords(Notification));
            LoadPatientsForAdding();
        }

        private void LoadPatientsForAdding()
        {
            ObservableCollection<MedicalRecord> medicalRecords = new ObservableCollection<MedicalRecord>(MedicalRecordService.GetAllRecords());
            
            LoadPatientsWhoAreNotReceivers(medicalRecords);

            PatientsRecordsCollection = CollectionViewSource.GetDefaultView(AllMedicalRecords);
            PatientsRecordsCollection.Filter = CustomFilterPacijenti;
        }

        private void LoadPatientsWhoAreNotReceivers(ObservableCollection<MedicalRecord> allRecords)
        {
            foreach(MedicalRecord medicalRecord in allRecords)
            {
                bool exists = false;

                foreach(MedicalRecord mr in Recipients.RecipientsRecords)
                {
                    if (mr.MedicalRecordID.Equals(medicalRecord.MedicalRecordID))
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                    AllMedicalRecords.Add(medicalRecord);
            }
        }

        private bool CustomFilterPacijenti(object obj)
        {
            if (string.IsNullOrEmpty(PacijentiFilter.Text))
                return true;
            else
                return (obj.ToString().IndexOf(PacijentiFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void BtnOdustaniClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ObavestenjaSekretar());
        }

        private void BtnPotvrdiClick(object sender, RoutedEventArgs e)
        {
            Notification.Date = DateTime.Now;
            NotificationService.UpdateNotification(Recipients, Notification);
            NavigationService.Navigate(new ObavestenjaSekretar());
        }

        private void PatientsFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(SviPacijentiListBox.ItemsSource).Refresh();
        }

        private void AddPatientRecipientClick(object sender, RoutedEventArgs e)
        {
            if(SelectedRecordForAdding != null)
            {
                Recipients.RecipientsRecords.Add(SelectedRecordForAdding);
                AllMedicalRecords.Remove(SelectedRecordForAdding);
                UpdatePatientCheck();
            }
        }

        private void RemovePatientRecipientClick(object sender, RoutedEventArgs e)
        {
            if(SelectedRecordForRemoving != null)
            {
                AllMedicalRecords.Add(SelectedRecordForRemoving);
                Recipients.RecipientsRecords.Remove(SelectedRecordForRemoving);
                UpdatePatientCheck();
            }
        }

        private void UpdatePatientCheck()
        {
            if (Recipients.RecipientsRecords.Count() != MedicalRecordService.GetAllRecords().Count())
                Recipients.IsEveryPatientRecipient = false;
            else
                Recipients.IsEveryPatientRecipient = true;
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
