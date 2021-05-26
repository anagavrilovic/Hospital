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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View.Doctor
{
    /// <summary>
    /// Interaction logic for DoktorObavestenja.xaml
    /// </summary>
    public partial class NotificationsPage : Page,INotifyPropertyChanged
    {
        private DoctorService doctorService = new DoctorService();
        private NotificationService notificationService = new NotificationService();
        private NotificationsUsersService notificationsUsersService = new NotificationsUsersService();
        private ObservableCollection<Notification> notifications = new ObservableCollection<Notification>();
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
        public NotificationsPage(string doctorId)
        {
            InitializeComponent();
            this.DataContext = this;
            doctor = doctorService.GetDoctorById(doctorId);
            Notifications=new ObservableCollection<Notification>(notificationService.SetNotificationsProperty(doctor));
            SetFilterProperties();
            SetIcons();
        }

        private void SetIcons()
        {
            var app = (App)Application.Current;
            if (app.DarkTheme)
            {
                deleteButton.Source = new BitmapImage(new Uri("pack://application:,,,/Icon/DoctorIcons/delete-16 (1).png", UriKind.Absolute));
                infoButton.Source = new BitmapImage(new Uri("pack://application:,,,/Icon/DoctorIcons/info-5-16 (1).png", UriKind.Absolute));
            }
            else
            {
                 // infoButton.Source = new BitmapImage(new Uri("pack://application:,,,/Icon/Secretary/deletete.png", UriKind.Absolute));
                   //deleteButton.Source = new BitmapImage(new Uri("pack://application:,,,/Icon/Secretary/deletete.png", UriKind.Absolute));
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

                notificationsUsersService.DeleteNotificationsUsersByNotificationID(((Notification)ListBoxNotifications.SelectedItem).Id);
                notificationService.DeleteNotification((Notification)ListBoxNotifications.SelectedItem);
                Notifications.Remove((Notification)ListBoxNotifications.SelectedItem);
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
