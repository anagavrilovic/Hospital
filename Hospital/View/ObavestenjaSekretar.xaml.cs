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
    public partial class ObavestenjaSekretar : Window
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
            NotificationList = Ns.GetAll();

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
                return (obj.ToString().IndexOf(ObavestenjaFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }


        private void ObavestenjaFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListBoxNotifications.ItemsSource).Refresh();
        }


        private void NovoObavestenjeClick(object sender, RoutedEventArgs e)
        {
            DodajObavestenje dodajObavestenje = new DodajObavestenje(NotificationList);
            dodajObavestenje.Show();
        }


        private void BrisanjeClick(object sender, RoutedEventArgs e)
        {
            if (ListBoxNotifications.SelectedItem == null)
            {
                MessageBox.Show("Selektujte obaveštenje koje želite da izbrišete!");
                return;
            }

            if (MessageBox.Show("Da li ste sigurni da želite da izbrišete obaveštenje?",
                        "Potvrda", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                NotificationList.Remove((Notification)ListBoxNotifications.SelectedItem);
                Ns.DoSerialization(NotificationList);
            }
        }

        private void IzmenaObavestenjaClick(object sender, RoutedEventArgs e)
        {
            if (ListBoxNotifications.SelectedItem == null)
            {
                MessageBox.Show("Selektujte obaveštenje koje želite da izmenite!");
                return;
            }

            var io = new IzmenaObavestenja((Notification)ListBoxNotifications.SelectedItem, NotificationList);
            io.Show();
        }

        private void PrikazObavestenjaClick(object sender, RoutedEventArgs e)
        {
            if (ListBoxNotifications.SelectedItem == null)
            {
                MessageBox.Show("Selektujte obaveštenje koje želite da pregledate!");
                return;
            }

            var po = new PrikazObavestenja((Notification)ListBoxNotifications.SelectedItem);
            po.Show();
        }
    }
}
