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
    /// Interaction logic for ObavestenjaSekretar.xaml
    /// </summary>
    public partial class ObavestenjaSekretar : Page
    {
        private ObservableCollection<Notification> notificationList = new ObservableCollection<Notification>();
        private NotificationStorage ns = new NotificationStorage();

        public NotificationStorage Ns
        {
            get { return ns; }
            set { ns = value; }
        }

        public ObservableCollection<Notification> NotificationList
        {
            get { return notificationList; }
            set { notificationList = value; }
        }

        private ICollectionView notificationCollection;

        public ICollectionView NotificationCollection
        {
            get { return notificationCollection; }
            set { notificationCollection = value; }
        }


        public ObavestenjaSekretar()
        {
            InitializeComponent();
            this.DataContext = this;
            UcitajObavestenja();
        }


        private void UcitajObavestenja()
        {
            NotificationList = Ns.GetAllNotifications();

            NotificationCollection = CollectionViewSource.GetDefaultView(NotificationList);
            NotificationCollection.Filter = CustomFilterObavestenja;
        }

        private bool CustomFilterObavestenja(object obj)
        {
            if (string.IsNullOrEmpty(ObavestenjaFilter.Text))
            {
                return true;
            }
            else
            {
                return ((obj as Notification).Title.IndexOf(ObavestenjaFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((obj as Notification).Content.IndexOf(ObavestenjaFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((obj as Notification).Date.ToString("dd.MM.yyyy, HH:mm").IndexOf(ObavestenjaFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }


        private void ObavestenjaFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListBoxNotifications.ItemsSource).Refresh();
        }


        private void NovoObavestenjeClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DodajObavestenje());
        }


        private void BrisanjeObavestenjaClick(object sender, RoutedEventArgs e)
        {
            if (ListBoxNotifications.SelectedItem == null)
            {
                MessageBox.Show("Selektujte obaveštenje koje želite da izbrišete!");
                return;
            }

            if (MessageBox.Show("Da li ste sigurni da želite da izbrišete obaveštenje?",
                        "Potvrda", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                Ns.ClearNotificationsUsersByNotificationID(((Notification)ListBoxNotifications.SelectedItem).Id);
                NotificationList.Remove((Notification)ListBoxNotifications.SelectedItem);
                Ns.SerializeNotifications(NotificationList);
            }

            /*var izbrisi = new IzbrisiObavestenje();
            izbrisi.Show();*/
        }

        private void IzmenaObavestenjaClick(object sender, RoutedEventArgs e)
        {
            if (ListBoxNotifications.SelectedItem == null)
            {
                MessageBox.Show("Selektujte obaveštenje koje želite da izmenite!");
                return;
            }

            NavigationService.Navigate(new IzmenaObavestenja((Notification)ListBoxNotifications.SelectedItem, NotificationList));
        }

        private void PrikazObavestenjaClick(object sender, RoutedEventArgs e)
        {
            if (ListBoxNotifications.SelectedItem == null)
            {
                MessageBox.Show("Selektujte obaveštenje koje želite da pregledate!");
                return;
            }

            var prikaz = new PrikazObavestenja((Notification)ListBoxNotifications.SelectedItem);
            prikaz.Show();
        }
    }
}
