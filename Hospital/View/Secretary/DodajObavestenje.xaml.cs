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
    /// Interaction logic for DodajObavestenje.xaml
    /// </summary>
    public partial class DodajObavestenje : Page
    {
        private Notification notification = new Notification();
        private ObservableCollection<MedicalRecord> allRecords = new ObservableCollection<MedicalRecord>();
        private ObservableCollection<MedicalRecord> addedRecords = new ObservableCollection<MedicalRecord>();
        
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

        public DodajObavestenje()
        {
            InitializeComponent();
            this.DataContext = this;
            UcitajPacijente();
            this.Notification.Date = DateTime.Now;
            this.Notification.Id = this.Notification.GetHashCode().ToString();
        }

        public void UcitajPacijente()
        {
            MedicalRecordStorage mds = new MedicalRecordStorage();
            AllRecords = mds.GetAll();

            PacijentiCollection = CollectionViewSource.GetDefaultView(AllRecords);
            PacijentiCollection.Filter = CustomFilterPacijenti;
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

        private void PacijentiFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(SviPacijentiListBox.ItemsSource).Refresh();
        }

        private void BtnPotvrdiClick(object sender, RoutedEventArgs e)
        {
            NotificationStorage ns = new NotificationStorage();
            ns.SaveNotification(Notification);

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
            
            foreach(MedicalRecord record in AddedRecords)
            {
                notificationsUsers.Add(new NotificationsUsers { NotificationID = Notification.Id, Username = record.Patient.Username, Read = false });
            }
            
            ns.SerializeNotificationsUsers(notificationsUsers);

            NavigationService.Navigate(new ObavestenjaSekretar());
        }

        private void BtnOdustaniClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ObavestenjaSekretar());
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
