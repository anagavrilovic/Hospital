using Hospital.Model;
using Hospital.Services;
using Hospital.View.Secretary;
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
    public partial class ObavestenjaSekretar : Page
    {
        public ObservableCollection<Notification> AllNotifications { get; set; }
        public Notification SelectedNotification { get; set; }
        public ICollectionView NotificationCollection { get; set; }

        public NotificationService NotificationService { get; set; }
        public NotificationStorage Ns { get; set; }

        public ObavestenjaSekretar()
        {
            InitializeComponent();
            this.DataContext = this;
            InitializeEmptyProperties();
            LoadAllNotifications();
        }

        private void InitializeEmptyProperties()
        {
            Ns = new NotificationStorage();
            AllNotifications = new ObservableCollection<Notification>();
            NotificationService = new NotificationService();
        }

        private void LoadAllNotifications()
        {
            AllNotifications = NotificationService.GetAllNotifications();
            SortNotificationList();

            NotificationCollection = CollectionViewSource.GetDefaultView(AllNotifications);
            NotificationCollection.Filter = CustomFilterObavestenja;
        }

        private void SortNotificationList()
        {
            List<Notification> sortedList = AllNotifications.OrderByDescending(n => n.Date).ToList();
            AllNotifications.Clear();
            AllNotifications = new ObservableCollection<Notification>(sortedList);
        }

        private void NovoObavestenjeClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DodajObavestenje());
        }

        private void BrisanjeObavestenjaClick(object sender, RoutedEventArgs e)
        {
            if (!IsNotificationSelected(GetNotificationNotSelectedForDeletingErrorMessage()))
                return;

            ConfirmBox confirmBox = new ConfirmBox("da želite da izbrišete obaveštenje?");
            if ((bool)confirmBox.ShowDialog())
            {
                Ns.ClearNotificationsUsersByNotificationID(((Notification)ListBoxNotifications.SelectedItem).Id);
                AllNotifications.Remove((Notification)ListBoxNotifications.SelectedItem);
                Ns.SerializeNotifications(AllNotifications);
            }
        }

        private void IzmenaObavestenjaClick(object sender, RoutedEventArgs e)
        {
            if (!IsNotificationSelected(GetNotificationNotSelectedForUpdatingErrorMessage()))
                return;

            NavigationService.Navigate(new IzmenaObavestenja(SelectedNotification, AllNotifications));
        }

        private void ShowNotificationClick(object sender, RoutedEventArgs e)
        {
            if (!IsNotificationSelected(GetNotificationNotSelectedForShowingErrorMessage()))
                return;

            PrikazObavestenja viewNotification = new PrikazObavestenja(SelectedNotification);
            viewNotification.Show();
        }

        private bool IsNotificationSelected(string errorMessage)
        {
            if (SelectedNotification == null)
            {
                InformationBox informationBox = new InformationBox(errorMessage);
                informationBox.ShowDialog();
                return false;
            }

            return true;
        }

        private string GetNotificationNotSelectedForShowingErrorMessage()
        {
            return "Selektujte obaveštenje koje želite da pregledate!";
        }

        private string GetNotificationNotSelectedForUpdatingErrorMessage()
        {
            return "Selektujte obaveštenje koje želite da izmenite!";
        }

        private string GetNotificationNotSelectedForDeletingErrorMessage()
        {
            return "Selektujte obaveštenje koje želite da izbrišete!";
        }

        private bool CustomFilterObavestenja(object obj)
        {
            if (string.IsNullOrEmpty(ObavestenjaFilter.Text))
                return true;
            else
                return ((obj as Notification).Title.IndexOf(ObavestenjaFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((obj as Notification).Content.IndexOf(ObavestenjaFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((obj as Notification).Date.ToString("dd.MM.yyyy, HH:mm").IndexOf(ObavestenjaFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void ObavestenjaFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListBoxNotifications.ItemsSource).Refresh();
        }
    }
}
