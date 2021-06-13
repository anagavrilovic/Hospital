using Caliburn.Micro;
using Hospital.Commands.DoctorCommands;
using Hospital.Controller;
using Hospital.Controller.DoctorControllers;
using Hospital.DTO.DoctorDTO;
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
        private Model.Doctor doctor = new Model.Doctor();
        private NavigationController navigationController;
        private DoctorNotificationsDTO dTO;
        public DoctorNotificationsDTO DTO
        {
            get { return dTO; }
            set
            {
                dTO = value;
                OnPropertyChanged("DTO");
            }
        }
        private DoctorNotificationsController controller;
        private BitmapImage deleteImage;
        public BitmapImage DeleteImage
        {
            get { return this.deleteImage; }
            set { this.deleteImage = value; this.OnPropertyChanged("ImageSource"); }
        }
        private string notificationFilterString;
        public string NotificationFilterString
        {
            get { return notificationFilterString; }
            set
            {
                notificationFilterString = value;
                OnPropertyChanged("NotificationFilterString");
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
        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
            set
            {
                deleteCommand = value;
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
            if (DTO.SelectedNotification == null)
            {
                ErrorBox messageBox = new ErrorBox("Selektujte obaveštenje koje želite da pregledate!");
                return;
            }

            var displayNotification = new DisplayNotification(DTO.SelectedNotification, navigationController);
            displayNotification.Show();
        }
        private bool CanExecute_Command(object obj)
        {
            return true;
        }

        public DoctorNotificationViewModel(string doctorId, NavigationController navigationController)
        {
            DTO = new DoctorNotificationsDTO();
            controller = new DoctorNotificationsController(DTO);
            this.navigationController = navigationController;
            doctor = controller.GetDoctorById(doctorId);
            DTO.Notifications = new ObservableCollection<Notification>();
            DTO.Notifications = new ObservableCollection<Notification>(controller.SetNotificationsProperty(doctor));
            SetCommands();
            SetFilterProperties();
            SetIcons();
        }

        private void SetCommands()
        {
            this.DeleteCommand = new RelayCommand(Execute_DeleteNotification, CanExecute_Command);
            this.textChanged = new RelayCommand(Execute_TextChanged, CanExecute_Command);
            this.ShowNotification = new RelayCommand(Execute_ShowNotificationCommand, CanExecute_Command);
        }

        private void SetIcons()
        {
            var app = (App)System.Windows.Application.Current;
            if (!app.DarkTheme)
            {
                deleteImage = new BitmapImage(new Uri("pack://application:,,,/Icon/DoctorIcons/delete_pink.png", UriKind.Absolute));
                infoImage = new BitmapImage(new Uri("pack://application:,,,/Icon/DoctorIcons/info_pink.png", UriKind.Absolute));
            }
            else
            {
                deleteImage = new BitmapImage(new Uri("pack://application:,,,/Icon/DoctorIcons/delete.png", UriKind.Absolute));
                infoImage = new BitmapImage(new Uri("pack://application:,,,/Icon/DoctorIcons/info.png", UriKind.Absolute));
            }
        }
        
        
        private void SetFilterProperties()
        {
            NotificationCollection = CollectionViewSource.GetDefaultView(DTO.Notifications);
            NotificationCollection.Filter = CustomFilterNotifications;
        }

        private bool CustomFilterNotifications(object obj)
        {
            if (string.IsNullOrEmpty(NotificationFilterString))
            {
                return true;
            }
            else
            {
                return (obj.ToString().IndexOf(NotificationFilterString, StringComparison.OrdinalIgnoreCase) >= 0);
            }   
        }

         void Execute_TextChanged(object sender)
        {
            CollectionViewSource.GetDefaultView(DTO.Notifications).Refresh();
        }

        private void Execute_DeleteNotification(object sender)
        {
            if (DTO.SelectedNotification == null)
            {
                ErrorBox messageBox = new ErrorBox("Selektujte obaveštenje koje želite da izbrišete!");
                return;
            }

            if (MessageBox.Show("Da li ste sigurni da želite da izbrišete obaveštenje?",
                        "Potvrda", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                controller.DeleteNotificationsUsersByNotificationID();
                controller.DeleteNotification();
                DTO.Notifications.Remove(DTO.SelectedNotification);
            }

        }
    }
}
