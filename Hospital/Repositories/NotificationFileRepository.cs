using Hospital.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public class NotificationFileRepository : IFileRepository<Notification>
    {
        private string fileName = "notifications.json";

        public NotificationFileRepository() { }

        public void Delete(string notificationID)
        {
            throw new NotImplementedException();
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

        public Notification GetByID(string notificationId)
        {
            ObservableCollection<Notification> notifications = GetAll();
            foreach (Notification n in notifications)
                if (n.Id.Equals(notificationId))
                    return n;

            return null;
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
    }
}
