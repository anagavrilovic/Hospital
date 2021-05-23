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
    class NotificationsService
    {
        INotificationRepository notificationRepository;
        INotificationsUsersRepository notificationsUsersRepository;
        IDoctorRepository doctorRepository;
        public ObservableCollection<Notification> SetNotificationsProperty(Doctor doctor,ObservableCollection<Notification> notifications)
        {
            notificationsUsersRepository = new NotificationsUsersFileRepository();
            notificationRepository = new NotificationFileRepository();
            foreach (NotificationsUsers notificationsUser in notificationsUsersRepository.GetAll())
            {
                if (notificationsUser.Username.Equals(doctor.Username))
                {
                    notifications.Add(notificationRepository.GetByID(notificationsUser.NotificationID));
                }

            }
            return notifications;
        }
        public void ClearNotificationsFromUser(string id)
        {
            notificationsUsersRepository.DeleteNotificationsUsersByNotificationID(id);
        }
        public void SerializeNotifications(ObservableCollection<Notification> notifications)
        {
            notificationRepository.Serialize(notifications);
        }

        public Doctor GetDoctorById(string id)
        {
            doctorRepository = new DoctorFileRepository();
            return doctorRepository.GetByID(id);
        }
    }
}
