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
            List<Notification> notifications = GetAll();
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

        public List<Notification> GetAll()
        {
            List<Notification> notifications;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                notifications = (List<Notification>)serializer.Deserialize(file, typeof(List<Notification>));
            }

            if (notifications == null)
                notifications = new List<Notification>();

            return notifications;
        }

        public List<Notification> GetAllNotificationsSortedDescending()
        {
            List<Notification> allNotifications = GetAll();
            List<Notification> sortedNotifications = allNotifications.OrderByDescending(n => n.Date).ToList();
            return new List<Notification>(sortedNotifications);
        }

        public Notification GetByID(string notificationId)
        {
            List<Notification> notifications = GetAll();
            foreach (Notification n in notifications)
                if (n.Id.Equals(notificationId))
                    return n;

            return null;
        }

        public List<int> GetExistingIDs()
        {
            List<Notification> notifications = GetAll();
            List<int> existingIDs = new List<int>();

            foreach (Notification notification in notifications)
                existingIDs.Add(Int32.Parse(notification.Id));

            return existingIDs;
        }

        public void Save(Notification notification)
        {
            List<Notification> notifications = this.GetAll();
            notifications.Add(notification);
            Serialize(notifications);
        }

        public void Serialize(List<Notification> notifications)
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
            Serialize(new List<Notification>(notifications));
        }


    }
}
