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
        private ObservableCollection<Notification> notifications = new ObservableCollection<Notification>();
        private DoctorStorage doctorStorage = new DoctorStorage();
        private Hospital.Model.Doctor doctor=new Model.Doctor();
        public ObservableCollection<Notification> Notifications
        {
            get
            {
                return notifications;
            }
            set
            {
                if (value != notifications)
                {
                    notifications = value;
                    OnPropertyChanged("Notifications");
                }
            }
        }
        private NotificationStorage notificationStorage = new NotificationStorage();
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
            SetNotificationsProperty();
            SetFilterProperties();
        }

        private void SetNotificationsProperty()
        {
            foreach (NotificationsUsers notificationsUser in notificationStorage.GetAllNotificationsUsers())
            {
                if (notificationsUser.Username.Equals(doctor.Username))
                {
                    Notifications.Add(notificationStorage.GetOne(notificationsUser.NotificationID));
                }

            }
        }

        private void SetFilterProperties()
        {
            NotificationCollection = CollectionViewSource.GetDefaultView(Notifications);
            NotificationCollection.Filter = CustomFilterNotifications;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private bool CustomFilterNotifications(object obj)
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

        private void DeleteNotification_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxNotifications.SelectedItem == null)
            {
                ErrorBox messageBox = new ErrorBox("Selektujte obaveštenje koje želite da izbrišete!");
                return;
            }

            if (MessageBox.Show("Da li ste sigurni da želite da izbrišete obaveštenje?",
                        "Potvrda", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                notificationStorage.ClearNotificationsUsersByNotificationID(((Notification)ListBoxNotifications.SelectedItem).Id);
                Notifications.Remove((Notification)ListBoxNotifications.SelectedItem);
                notificationStorage.SerializeNotifications(Notifications);
            }

        }
        private void ShowNotification_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxNotifications.SelectedItem == null)
            {
                ErrorBox messageBox = new ErrorBox("Selektujte obaveštenje koje želite da pregledate!");
                return;
            }

            var displayNotification = new PrikazObavestenja((Notification)ListBoxNotifications.SelectedItem);
            displayNotification.Show();
        }
    }

}
