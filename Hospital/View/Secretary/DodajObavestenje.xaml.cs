﻿using Hospital.DTO;
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
    public partial class DodajObavestenje : Page
    {
        public Notification NewNotification { get; set; }
        public NotificationRecipientsDTO Recipients { get; set; }

        public ObservableCollection<MedicalRecord> AllMedicalRecords { get; set; }
        public ICollectionView PatientsRecordCollection { get; set; }

        public MedicalRecord SelectedForAdding { get; set; }
        public MedicalRecord SelectedForRemoving { get; set; }

        public NotificationService NotificationService { get; set; }
        public MedicalRecordService MedicalRecordService { get; set; }


        public DodajObavestenje()
        {
            InitializeComponent();
            this.DataContext = this;
            InitializeEmptyProperties();
            LoadAllMedicalRecords();
            InitializeNewNotification();
        }

        private void InitializeEmptyProperties()
        {
            this.NewNotification = new Notification();
            this.AllMedicalRecords = new ObservableCollection<MedicalRecord>();
            this.Recipients = new NotificationRecipientsDTO();
            this.NotificationService = new NotificationService();
            this.MedicalRecordService = new MedicalRecordService();
        }

        private void InitializeNewNotification()
        {
            this.NewNotification.Date = DateTime.Now;
            this.NewNotification.Id = this.NewNotification.GetHashCode().ToString();
        }

        public void LoadAllMedicalRecords()
        {
            AllMedicalRecords = MedicalRecordService.GetAllRecords();

            PatientsRecordCollection = CollectionViewSource.GetDefaultView(AllMedicalRecords);
            PatientsRecordCollection.Filter = CustomFilterPacijenti;
        }

        private void BtnPotvrdiClick(object sender, RoutedEventArgs e)
        {
            NotificationService.CreateNotification(Recipients, NewNotification);
            /*NotificationStorage ns = new NotificationStorage();
            ns.SaveNotification(Notification);

            RegistratedUserStorage rus = new RegistratedUserStorage();
            ObservableCollection<RegistratedUser> regUsers = rus.GetAll();
            ObservableCollection<NotificationsUsers> notificationsUsers = ns.GetAllNotificationsUsers();

            if (IsEverySecretaryRecipient || IsEveryDoctorRecipient || IsEveryManagerRecipient || IsEveryPatientRecipient)
            {
                foreach (RegistratedUser user in regUsers)
                {
                    if (IsEverySecretaryRecipient && user.Type.Equals(UserType.secretary))
                    {
                        notificationsUsers.Add(new NotificationsUsers { NotificationID = Notification.Id, Username = user.Username, Read = false });
                    }
                    if (IsEveryDoctorRecipient && user.Type.Equals(UserType.doctor))
                    {
                        notificationsUsers.Add(new NotificationsUsers { NotificationID = Notification.Id, Username = user.Username, Read = false });
                    }
                    if (IsEveryManagerRecipient && user.Type.Equals(UserType.manager))
                    {
                        notificationsUsers.Add(new NotificationsUsers { NotificationID = Notification.Id, Username = user.Username, Read = false });
                    }
                    if (IsEveryPatientRecipient && user.Type.Equals(UserType.patient))
                    {
                        notificationsUsers.Add(new NotificationsUsers { NotificationID = Notification.Id, Username = user.Username, Read = false });
                    }
                }
            }
            
            foreach(MedicalRecord record in AllMedicalRecords)
            {
                notificationsUsers.Add(new NotificationsUsers { NotificationID = Notification.Id, Username = record.Patient.Username, Read = false });
            }
            
            ns.SerializeNotificationsUsers(notificationsUsers);*/

            NavigationService.Navigate(new ObavestenjaSekretar());
        }

        private void BtnOdustaniClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ObavestenjaSekretar());
        }

        private void AddPatientRecipientClick(object sender, RoutedEventArgs e)
        {
            if(SelectedForAdding != null)
            {
                Recipients.RecipientsRecords.Add(SelectedForAdding);
                AllMedicalRecords.Remove(SelectedForAdding);
                UpdatePatientCheck();
            }   
        }

        private void RemovePatientRecipientClick(object sender, RoutedEventArgs e)
        {
            if(SelectedForRemoving != null)
            {
                AllMedicalRecords.Add(SelectedForRemoving);
                Recipients.RecipientsRecords.Remove(SelectedForRemoving);
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

        private bool CustomFilterPacijenti(object obj)
        {
            if (string.IsNullOrEmpty(PacijentiFilter.Text))
                return true;
            else
                return (obj.ToString().IndexOf(PacijentiFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void PacijentiFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(SviPacijentiListBox.ItemsSource).Refresh();
        }
    }
}
