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
    /// Interaction logic for ManagerNotifications.xaml
    /// </summary>
    public partial class ManagerNotifications : Page, INotifyPropertyChanged
    {
        private ObservableCollection<Notification> notifications = new ObservableCollection<Notification>();

        public ObservableCollection<Notification> Notifications
        {
            get
            {
                return notifications;
            }
            set
            {
                if(value!=notifications)
                {
                    notifications = value;
                    OnPropertyChanged("Notifications");
                }
            }
        }

        private NotificationStorage notificationStorage = new NotificationStorage();

        private string searchstr;

        private ICollectionView notificationCollection;

        public ICollectionView NotificationCollection
        {
            get { return notificationCollection; }
            set { notificationCollection = value; }
        }


        public ManagerNotifications()
        {
            InitializeComponent();
            this.DataContext = this;

            /*foreach(Notification n in notificationStorage.GetAll())
            {
                if (n.Upravnik)
                {
                    Notifications.Add(n);
                }
            }*/

            NotificationCollection = CollectionViewSource.GetDefaultView(Notifications);
        }

        private void searchNotifications(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox != null)
            {
                this.searchstr = textbox.Text;
                if (!string.IsNullOrEmpty(searchstr))
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(listBoxNotifications.ItemsSource);
                    view.Filter = new Predicate<object>(filter);
                    this.NotificationCollection.Refresh();
                }
                else
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(Notifications);
                    this.NotificationCollection.Refresh();
                }
            }
        }

        private void btnSearchMouseDown(object sender, RoutedEventArgs e)
        {
            this.NotificationCollection.Refresh();
        }


        private bool filter(object item)
        {
            if (((Notification)item).Id.Contains(searchstr) || ((Notification)item).Title.Contains(searchstr) || ((Notification)item).Date.ToString().Contains(searchstr))
            {
                return true;
            }
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerMainPage());
        }
    }
}
