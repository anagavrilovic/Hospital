using Hospital.Model;
using Hospital.ViewModels;
using System.Collections.ObjectModel;


namespace Hospital.DTO.DoctorDTO
{
    public class DoctorNotificationsDTO : ViewModel
    {
        private ObservableCollection<Notification> notifications;
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

        private Notification selectedNotification;
        public Notification SelectedNotification
        {
            get { return selectedNotification; }
            set
            {
                selectedNotification = value;
                OnPropertyChanged("SelectedNotification");
            }
        }
    }
}
