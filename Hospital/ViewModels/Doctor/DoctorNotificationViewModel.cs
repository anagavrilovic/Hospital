﻿using Caliburn.Micro;
using Hospital.Commands.DoctorCommands;
using Hospital.Model;
using Hospital.Services;
using Hospital.View.Doctor;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using static System.Net.Mime.MediaTypeNames;

namespace Hospital.ViewModels.Doctor
{
    class DoctorNotificationViewModel : ViewModel
    {
        private DoctorService doctorService = new DoctorService();
        private NotificationService notificationService = new NotificationService();
        private NotificationsUsersService notificationsUsersService = new NotificationsUsersService();
        private ObservableCollection<Notification> notifications = new ObservableCollection<Notification>();
        private Model.Doctor doctor = new Model.Doctor();
        private NavigationService navigationService;

        private Notification selectedNotification;

        private BitmapImage deleteImage;
        public BitmapImage DeleteImage
        {
            get { return this.deleteImage; }
            set { this.deleteImage = value; this.OnPropertyChanged("ImageSource"); }
        }

        public Notification SelectedNotification
        {
            get { return selectedNotification; }
            set
            {
                selectedNotification = value;
                OnPropertyChanged("SelectedNotification");
            }
        }
        private string notificationFilterString;
        public string NotificationFilterString
        {
            get { return notificationFilterString; }
            set
            {
                notificationFilterString = value;
                OnPropertyChanged("NotificationFilterString");
                MessageBox.Show("aaa");
            }
        }

        private BitmapImage infoImage;

        public BitmapImage InfoImage
        {
            get { return infoImage; }
            set
            {
                infoImage = value;
                OnPropertyChanged("DeleteImage");
            }
        }

        private RelayCommand showNotification;
        public RelayCommand ShowNotification
        {
            get { return showNotification; }
            set
            {
                showNotification = value;
            }
        }
        private RelayCommand textChanged;
        public RelayCommand TextChanged
        {
            get { return textChanged; }
            set
            {
                textChanged = value;
            }
        }


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
        private ICollectionView notificationCollection;

        public ICollectionView NotificationCollection
        {
            get { return notificationCollection; }
            set { notificationCollection = value; }
        }

        private void Execute_ShowNotificationCommand(object obj)
        {
            if (SelectedNotification == null)
            {
                ErrorBox messageBox = new ErrorBox("Selektujte obaveštenje koje želite da pregledate!");
                return;
            }

            var displayNotification = new DisplayNotification((Notification)selectedNotification);
            displayNotification.Show();
        }
        private bool CanExecute_Command(object obj)
        {
            return true;
        }

        public DoctorNotificationViewModel(string doctorId,NavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.textChanged = new RelayCommand(Execute_TextChanged, CanExecute_Command);
            this.ShowNotification = new RelayCommand(Execute_ShowNotificationCommand, CanExecute_Command);
            doctor = doctorService.GetDoctorById(doctorId);
            Notifications = new ObservableCollection<Notification>(notificationService.SetNotificationsProperty(doctor));
            SetFilterProperties();
            SetIcons();
        }
        
        private void SetIcons()
        {
            var app = (App)System.Windows.Application.Current;
            if (app.DarkTheme)
            {
                deleteImage = new BitmapImage(new Uri("pack://application:,,,/Icon/DoctorIcons/delete-16 (1).png", UriKind.Absolute));
                infoImage = new BitmapImage(new Uri("pack://application:,,,/Icon/DoctorIcons/info-5-16 (1).png", UriKind.Absolute));
            }
            else
            {
                deleteImage = new BitmapImage(new Uri("pack://application:,,,/Icon/DoctorIcons/delete-16 (1).png", UriKind.Absolute));
                infoImage= new BitmapImage(new Uri("pack://application:,,,/Icon/DoctorIcons/info-5-16 (1).png", UriKind.Absolute));
            }
        }
        
        
        private void SetFilterProperties()
        {
            NotificationCollection = CollectionViewSource.GetDefaultView(Notifications);
            NotificationCollection.Filter = CustomFilterNotifications;
        }

        private bool CustomFilterNotifications(object obj)
        {
            if (string.IsNullOrEmpty(NotificationFilterString))
            {
                MessageBox.Show("aaa");
                return true;
            }
            else
            {
                MessageBox.Show("bbb");
                return (obj.ToString().IndexOf(NotificationFilterString, StringComparison.OrdinalIgnoreCase) >= 0);
            }   
        }

         void Execute_TextChanged(object sender)
        {
            CollectionViewSource.GetDefaultView(Notifications);
        }

        private void DeleteNotification_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedNotification == null)
            {
                ErrorBox messageBox = new ErrorBox("Selektujte obaveštenje koje želite da izbrišete!");
                return;
            }

            if (MessageBox.Show("Da li ste sigurni da želite da izbrišete obaveštenje?",
                        "Potvrda", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                notificationsUsersService.DeleteNotificationsUsersByNotificationID(((Notification)SelectedNotification).Id);
                notificationService.DeleteNotification((Notification)SelectedNotification);
                Notifications.Remove((Notification)SelectedNotification);
            }

        }
    }
}