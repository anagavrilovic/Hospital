﻿using Hospital.Model;
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
        private ObservableCollection<Notification> _notifications = new ObservableCollection<Notification>();
        public ObservableCollection<Notification> Notifications
        {
            get => _notifications;
            set
            {
                if(value!=_notifications)
                {
                    _notifications = value;
                    OnPropertyChanged("Notifications");
                }
            }
        }

        private NotificationStorage _notificationStorage;
        private string _searchCriterion;

        public ICollectionView NotificationCollection { get; set; }


        public ManagerNotifications()
        {
            InitializeComponent();
            this.DataContext = this;
            _notificationStorage = new NotificationStorage();

            /*foreach(Notification n in notificationStorage.GetAll())
            {
                if (n.Upravnik)
                {
                    Notifications.Add(n);
                }
            }*/

            NotificationCollection = CollectionViewSource.GetDefaultView(Notifications);
        }

        private void SearchNotifications(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox != null)
            {
                this._searchCriterion = textbox.Text;
                if (!string.IsNullOrEmpty(_searchCriterion))
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(listBoxNotifications.ItemsSource);
                    view.Filter = new Predicate<object>(Filter);
                    this.NotificationCollection.Refresh();
                }
                else
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(Notifications);
                    this.NotificationCollection.Refresh();
                }
            }
        }

        private void BtnSearchMouseDown(object sender, RoutedEventArgs e)
        {
            this.NotificationCollection.Refresh();
        }

        private bool Filter(object item)
        {
            if (((Notification)item).Id.Contains(_searchCriterion) || ((Notification)item).Title.Contains(_searchCriterion) || ((Notification)item).Date.ToString().Contains(_searchCriterion))
            {
                return true;
            }
            return false;
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerMainPage());
        }
    

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
