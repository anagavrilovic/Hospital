using Hospital.Model;
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
    /// <summary>
    /// Interaction logic for IzmenaObavestenja.xaml
    /// </summary>
    public partial class IzmenaObavestenja : Page
    {
        private Notification notification = new Notification();
        private ObservableCollection<Notification> notificationList = new ObservableCollection<Notification>();
        private ObservableCollection<MedicalRecord> allRecords = new ObservableCollection<MedicalRecord>();
        private ObservableCollection<MedicalRecord> addedRecords = new ObservableCollection<MedicalRecord>();

        public ObservableCollection<Notification> NotificationList
        {
            get { return notificationList; }
            set { notificationList = value; }
        }

        public Notification Notification
        {
            get { return notification; }
            set { notification = value; }
        }

        public ObservableCollection<MedicalRecord> AllRecords
        {
            get { return allRecords; }
            set { allRecords = value; }
        }

        public ObservableCollection<MedicalRecord> AddedRecords
        {
            get { return addedRecords; }
            set { addedRecords = value; }
        }

        private bool sekretar;
        private bool lekar;
        private bool upravnik;
        private bool pacijent;

        public bool Pacijent
        {
            get { return pacijent; }
            set { pacijent = value; }
        }

        public bool Upravnik
        {
            get { return upravnik; }
            set { upravnik = value; }
        }

        public bool Lekar
        {
            get { return lekar; }
            set { lekar = value; }
        }

        public bool Sekretar
        {
            get { return sekretar; }
            set { sekretar = value; }
        }

        private ICollectionView pacijentiCollection;

        public ICollectionView PacijentiCollection
        {
            get { return pacijentiCollection; }
            set { pacijentiCollection = value; }
        }

        public IzmenaObavestenja(Notification n, ObservableCollection<Notification> notifications)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Notification = n;
            this.NotificationList = notifications;
            CekirajCheckBoxove();
            UcitajDodatePacijente();
        }

        private void CekirajCheckBoxove()
        {
            RegistratedUserStorage registratedUserStorage = new RegistratedUserStorage();
            NotificationStorage notificationStorage = new NotificationStorage();

            if (notificationStorage.CountSecretariesByNotificationID(Notification.Id) == registratedUserStorage.CountSecretaries())
                Sekretar = true;

            if (notificationStorage.CountDoctorsByNotificationID(Notification.Id) == registratedUserStorage.CountDoctors())
                Lekar = true;

            if (notificationStorage.CountManagersByNotificationID(Notification.Id) == registratedUserStorage.CountManagers())
                Upravnik = true;

            if (notificationStorage.CountPatientsByNotificationID(Notification.Id) == registratedUserStorage.CountPatients())
                Pacijent = true;
        }

        private void UcitajDodatePacijente()
        {
            MedicalRecordStorage mrs = new MedicalRecordStorage();
            if (Pacijent)
            {
                foreach (MedicalRecord mr in mrs.GetAll())
                    AddedRecords.Add(mr);

                return;
            }

            RegistratedUserStorage rus = new RegistratedUserStorage();
            NotificationStorage ns = new NotificationStorage();
            ObservableCollection<NotificationsUsers> notificationsUsers = ns.GetAllNotificationsUsers();

            foreach (NotificationsUsers nu in notificationsUsers)
            {
                if (Notification.Id.Equals(nu.NotificationID) && rus.GetRoleByUsername(nu.Username).Equals(UserType.patient))
                    AddedRecords.Add(mrs.GetPatientByUsername(nu.Username));
            }

            UcitajPreostalePacijente();
        }

        private void UcitajPreostalePacijente()
        {
            MedicalRecordStorage mrs = new MedicalRecordStorage();
            ObservableCollection<MedicalRecord> medicalRecords = mrs.GetAll();
            
            DodajOneKojiNisuVecDodati(medicalRecords);

            PacijentiCollection = CollectionViewSource.GetDefaultView(AllRecords);
            PacijentiCollection.Filter = CustomFilterPacijenti;
        }

        private void DodajOneKojiNisuVecDodati(ObservableCollection<MedicalRecord> allRec)
        {
            foreach(MedicalRecord mr in allRec)
            {
                bool exists = false;

                foreach(MedicalRecord mr2 in AddedRecords)
                {
                    if (mr2.MedicalRecordID.Equals(mr.MedicalRecordID))
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                    AllRecords.Add(mr);
            }
        }

        private bool CustomFilterPacijenti(object obj)
        {
            if (string.IsNullOrEmpty(PacijentiFilter.Text))
            {
                return true;
            }
            else
            {
                return (obj.ToString().IndexOf(PacijentiFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private void BtnOdustaniClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ObavestenjaSekretar());
        }

        private void BtnPotvrdiClick(object sender, RoutedEventArgs e)
        {
            NaslovText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            SadrzajText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            Notification.Date = DateTime.Now;

            NotificationStorage ns = new NotificationStorage();
            ns.ClearNotificationsUsersByNotificationID(Notification.Id);

            RegistratedUserStorage rus = new RegistratedUserStorage();
            ObservableCollection<RegistratedUser> regUsers = rus.GetAll();
            ObservableCollection<NotificationsUsers> notificationsUsers = ns.GetAllNotificationsUsers();

            if (Sekretar || Lekar || Upravnik || Pacijent)
            {
                foreach (RegistratedUser user in regUsers)
                {
                    if (Sekretar && user.Type.Equals(UserType.secretary))
                    {
                        notificationsUsers.Add(new NotificationsUsers { NotificationID = Notification.Id, Username = user.Username, Read = false });
                    }
                    if (Lekar && user.Type.Equals(UserType.doctor))
                    {
                        notificationsUsers.Add(new NotificationsUsers { NotificationID = Notification.Id, Username = user.Username, Read = false });
                    }
                    if (Upravnik && user.Type.Equals(UserType.manager))
                    {
                        notificationsUsers.Add(new NotificationsUsers { NotificationID = Notification.Id, Username = user.Username, Read = false });
                    }
                    if (Pacijent && user.Type.Equals(UserType.patient))
                    {
                        notificationsUsers.Add(new NotificationsUsers { NotificationID = Notification.Id, Username = user.Username, Read = false });
                    }
                }
            }

            if (!Pacijent)
            {
                foreach (MedicalRecord record in AddedRecords)
                {
                    notificationsUsers.Add(new NotificationsUsers { NotificationID = Notification.Id, Username = record.Patient.Username, Read = false });
                }
            }

            ns.SerializeNotifications(NotificationList);
            ns.SerializeNotificationsUsers(notificationsUsers);
            NavigationService.Navigate(new ObavestenjaSekretar());
        }

        private void PacijentiFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(SviPacijentiListBox.ItemsSource).Refresh();
        }

        private void DodajPacijentaClick(object sender, RoutedEventArgs e)
        {
            AddedRecords.Add((MedicalRecord)SviPacijentiListBox.SelectedItem);
            AllRecords.Remove((MedicalRecord)SviPacijentiListBox.SelectedItem);
        }

        private void UkloniPacijentaClick(object sender, RoutedEventArgs e)
        {
            AllRecords.Add((MedicalRecord)DodatiPacijentiListBox.SelectedItem);
            AddedRecords.Remove((MedicalRecord)DodatiPacijentiListBox.SelectedItem);
        }
    }
}
