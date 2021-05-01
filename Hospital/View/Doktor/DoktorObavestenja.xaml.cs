using Hospital.Model;
using Hospital.View.Doktor;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for DoktorObavestenja.xaml
    /// </summary>
    public partial class DoktorObavestenja : Page,INotifyPropertyChanged
    {
        private ObservableCollection<Notification> obavestenja = new ObservableCollection<Notification>();
        private DoctorStorage doctorStorage = new DoctorStorage();
        private Hospital.Model.Doctor doctor=new Model.Doctor();
        public ObservableCollection<Notification> Obavestenja
        {
            get
            {
                return obavestenja;
            }
            set
            {
                if (value != obavestenja)
                {
                    obavestenja = value;
                    OnPropertyChanged("Obavestenja");
                }
            }
        }
        private NotificationStorage nStorage = new NotificationStorage();
        private ICollectionView notificationCollection;

        public ICollectionView NotificationCollection
        {
            get { return notificationCollection; }
            set { notificationCollection = value; }
        }
        public DoktorObavestenja(string doctorId)
        {
            InitializeComponent();
            this.DataContext = this;
            doctor = doctorStorage.GetOne(doctorId);
            initProperties();
        }

        private void initProperties()
        {
            foreach(NotificationsUsers notificationsUser in nStorage.GetAllNotificationsUsers())
            {
                if (notificationsUser.Username.Equals(doctor.Username))
                {
                    Obavestenja.Add(nStorage.GetOne(notificationsUser.NotificationID));
                }
                       
            }
            NotificationCollection = CollectionViewSource.GetDefaultView(Obavestenja);
            NotificationCollection.Filter = CustomFilterObavestenja;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private bool CustomFilterObavestenja(object obj)
        {
            if (string.IsNullOrEmpty(ObavestenjaFilter.Text))
            {
                return true;
            }
            else
            {
                return (obj.ToString().IndexOf(ObavestenjaFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }
        private void ObavestenjaFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListBoxNotifications.ItemsSource).Refresh();
        }

        private void BrisanjeObavestenjaClick(object sender, RoutedEventArgs e)
        {
            if (ListBoxNotifications.SelectedItem == null)
            {
                ErrorBox messageBox = new ErrorBox("Selektujte obaveštenje koje želite da izbrišete!");
                return;
            }

            if (MessageBox.Show("Da li ste sigurni da želite da izbrišete obaveštenje?",
                        "Potvrda", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                nStorage.ClearNotificationsUsersByNotificationID(((Notification)ListBoxNotifications.SelectedItem).Id);
                Obavestenja.Remove((Notification)ListBoxNotifications.SelectedItem);
                nStorage.SerializeNotifications(Obavestenja);
            }

            /*var izbrisi = new IzbrisiObavestenje();
            izbrisi.Show();*/
        }
        private void PrikazObavestenjaClick(object sender, RoutedEventArgs e)
        {
            if (ListBoxNotifications.SelectedItem == null)
            {
                ErrorBox messageBox = new ErrorBox("Selektujte obaveštenje koje želite da pregledate!");
                return;
            }

            var prikaz = new PrikazObavestenja((Notification)ListBoxNotifications.SelectedItem);
            prikaz.Show();
        }

        private void MedicRevision_Click(object sender, RoutedEventArgs e)
        {
         ((DoktorGlavniProzor)Window.GetWindow(this)).Main.Navigate(new ValidnostLeka(doctor));
        }
    }

}
