using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services.DoctorServices
{
    class NotificationsServicePage
    {
        private NotificationsUsersService notificationsUsersService;
        private NotificationService notificationService;
        public ObservableCollection<Notification> SetNotificationsProperty(Doctor doctor,ObservableCollection<Notification> notifications)
        {
            notificationService = new NotificationService();
            notificationsUsersService = new NotificationsUsersService();
            foreach (NotificationsUsers notificationsUser in notificationsUsersService.GetAll())
            {
                if (notificationsUser.Username.Equals(doctor.Username))
                {
                    notifications.Add(notificationService.GetById(notificationsUser.NotificationID));
                }

            }
            return notifications;
        }

    }
}
