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
        public ICollectionView NotificationCollection { get; set; }
        public Notification SelectedNotification { get; set; }

        public NotificationService NotificationService { get; set; }


        public ObavestenjaSekretar()
        {
            InitializeComponent();
            this.DataContext = this;
            InitializeEmptyProperties();
            LoadAllNotifications();
        }

        private void InitializeEmptyProperties()
        {
            AllNotifications = new ObservableCollection<Notification>();
            NotificationService = new NotificationService();
        }

        private void LoadAllNotifications()
        {
            AllNotifications = new ObservableCollection<Notification>(NotificationService.GetAllNotificationsSortedDescending());

            NotificationCollection = CollectionViewSource.GetDefaultView(AllNotifications);
            NotificationCollection.Filter = CustomFilterObavestenja;
        }

        private void CreateNotificationClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DodajObavestenje());
        }

        private void DeleteNotificationClick(object sender, RoutedEventArgs e)
        {
            if (!IsNotificationSelected(notificationNotSelectedForDeletingErrorMessage))
                return;

            ConfirmBox confirmBox = new ConfirmBox("da želite da izbrišete obaveštenje?");
            if ((bool)confirmBox.ShowDialog())
            {
                NotificationService.DeleteNotification(SelectedNotification);
                AllNotifications.Remove(SelectedNotification);
            }
        }

        private void UpdateNotificationClick(object sender, RoutedEventArgs e)
        {
            if (!IsNotificationSelected(notificationNotSelectedForUpdatingErrorMessage))
                return;

            NavigationService.Navigate(new IzmenaObavestenja(SelectedNotification));
        }

        private void ShowNotificationClick(object sender, RoutedEventArgs e)
        {
            if (!IsNotificationSelected(notificationNotSelectedForShowingErrorMessage))
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

        private const string notificationNotSelectedForShowingErrorMessage = "Selektujte obaveštenje koje želite da pregledate!";
        private const string notificationNotSelectedForUpdatingErrorMessage = "Selektujte obaveštenje koje želite da izmenite!";
        private const string notificationNotSelectedForDeletingErrorMessage = "Selektujte obaveštenje koje želite da izbrišete!";
    }
}
