using Hospital.Model;
using Newtonsoft.Json;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public class NotificationFileRepository : INotificationRepository
    {
        private string fileName = "notifications.json";

        public NotificationFileRepository() { }

        public void Delete(string notificationID)
        {
            ObservableCollection<Notification> notifications = GetAll();
            foreach (Notification n in notifications)
            {
                if (n.Id.Equals(notificationID))
                {
                    notifications.Remove(n);
                    Serialize(notifications);
                    return;
                }
            }
        }

        public ObservableCollection<Notification> GetAll()
        {
            ObservableCollection<Notification> notifications;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                notifications = (ObservableCollection<Notification>)serializer.Deserialize(file, typeof(ObservableCollection<Notification>));
            }

            if (notifications == null)
                notifications = new ObservableCollection<Notification>();

            return notifications;
        }

        public ObservableCollection<Notification> GetAllNotificationsSortedDescending()
        {
            ObservableCollection<Notification> allNotifications = GetAll();
            List<Notification> sortedNotifications = allNotifications.OrderByDescending(n => n.Date).ToList();
            return new ObservableCollection<Notification>(sortedNotifications);
        }

        public Notification GetByID(string notificationId)
        {
            ObservableCollection<Notification> notifications = GetAll();
            foreach (Notification n in notifications)
                if (n.Id.Equals(notificationId))
                    return n;

            return null;
        }

        public List<int> GetExistingIDs()
        {
            ObservableCollection<Notification> notifications = GetAll();
            List<int> existingIDs = new List<int>();

            foreach (Notification notification in notifications)
                existingIDs.Add(Int32.Parse(notification.Id));

            return existingIDs;
        }

        public void Save(Notification notification)
        {
            ObservableCollection<Notification> notifications = this.GetAll();
            notifications.Add(notification);
            Serialize(notifications);
        }

        public void Serialize(ObservableCollection<Notification> notifications)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, notifications);
            }
        }

        public void Update(Notification newNotification)
        {
            List<Notification> notifications = GetAll().ToList();
            notifications[notifications.FindIndex(notification => notification.Id.Equals(newNotification.Id))] = newNotification;
            Serialize(new ObservableCollection<Notification>(notifications));
        }


    }
}
