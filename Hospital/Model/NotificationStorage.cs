using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class NotificationStorage
    {
        private String notificationFileName;
        private String notificationsUsersFileName;

        public NotificationStorage()
        {
            this.notificationFileName = "notifications.json";
            this.notificationsUsersFileName = "notificationsUsers.json";
        }

        public ObservableCollection<Notification> GetAllNotifications()
        {
            ObservableCollection<Notification> notifications;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + notificationFileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                notifications = (ObservableCollection<Notification>)serializer.Deserialize(file, typeof(ObservableCollection<Notification>));
            }

            return notifications;
        }

        public ObservableCollection<NotificationsUsers> GetAllNotificationsUsers()
        {
            ObservableCollection<NotificationsUsers> notificationsUsers;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + notificationsUsersFileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                notificationsUsers = (ObservableCollection<NotificationsUsers>)serializer.Deserialize(file, typeof(ObservableCollection<NotificationsUsers>));
            }

            return notificationsUsers;
        }

        public void SaveNotification(Notification parameter1)
        {
            ObservableCollection<Notification> notifications = this.GetAllNotifications();
            notifications.Add(parameter1);
            SerializeNotifications(notifications);
        }

        public void SaveNotificationUser(NotificationsUsers parameter1)
        {
            ObservableCollection<NotificationsUsers> notificationsUsers = this.GetAllNotificationsUsers();
            notificationsUsers.Add(parameter1);
            SerializeNotificationsUsers(notificationsUsers);
        }

        public void ClearNotificationsUsersByNotificationID(string id)
        {
              ObservableCollection<NotificationsUsers> notificationsUsers = GetAllNotificationsUsers();
              foreach(NotificationsUsers n in notificationsUsers.ToList())
              {
                  if (n.NotificationID.Equals(id))
                  {
                      notificationsUsers.Remove(n);
                  }
              }

              SerializeNotificationsUsers(notificationsUsers);
        }


        public Notification GetOne(string id)
        {
            ObservableCollection<Notification> notifications = GetAllNotifications();
            foreach (Notification n in notifications)
            {
                if (n.Id.Equals(id))
                {
                    return n;
                }
            }

            return null;
        }

        public void SerializeNotifications(ObservableCollection<Notification> notifications)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + notificationFileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, notifications);
            }
        }

        public void SerializeNotificationsUsers(ObservableCollection<NotificationsUsers> notificationsUsers)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + notificationsUsersFileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, notificationsUsers);
            }
        }

        public int CountSecretariesByNotificationID(string id)
        {
            RegistratedUserStorage registratedUserStorage = new RegistratedUserStorage();
            ObservableCollection<NotificationsUsers> notificationsUsers = GetAllNotificationsUsers();
            int retVal = 0;

            foreach (NotificationsUsers nu in notificationsUsers)
            {
                if (registratedUserStorage.GetRoleByUsername(nu.Username).Equals(UserType.secretary) && nu.NotificationID.Equals(id))
                    retVal += 1;
            }

            return retVal;
        }

        public int CountDoctorsByNotificationID(string id)
        {
            RegistratedUserStorage registratedUserStorage = new RegistratedUserStorage();
            ObservableCollection<NotificationsUsers> notificationsUsers = GetAllNotificationsUsers();
            int retVal = 0;

            foreach (NotificationsUsers nu in notificationsUsers)
            {
                if (registratedUserStorage.GetRoleByUsername(nu.Username).Equals(UserType.doctor) && nu.NotificationID.Equals(id))
                    retVal += 1;
            }

            return retVal;
        }

        public int CountManagersByNotificationID(string id)
        {
            RegistratedUserStorage registratedUserStorage = new RegistratedUserStorage();
            ObservableCollection<NotificationsUsers> notificationsUsers = GetAllNotificationsUsers();
            int retVal = 0;

            foreach (NotificationsUsers nu in notificationsUsers)
            {
                if (registratedUserStorage.GetRoleByUsername(nu.Username).Equals(UserType.manager) && nu.NotificationID.Equals(id))
                    retVal += 1;
            }

            return retVal;
        }

        public int CountPatientsByNotificationID(string id)
        {
            RegistratedUserStorage registratedUserStorage = new RegistratedUserStorage();
            ObservableCollection<NotificationsUsers> notificationsUsers = GetAllNotificationsUsers();
            int retVal = 0;

            foreach (NotificationsUsers nu in notificationsUsers)
            {
                if (registratedUserStorage.GetRoleByUsername(nu.Username).Equals(UserType.patient) && nu.NotificationID.Equals(id))
                    retVal += 1;
            }

            return retVal;
        }
    }
}
