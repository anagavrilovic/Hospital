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
    public class NotificationsUsersFileRepository : INotificationsUsersRepository
    {
        private string fileName = "notificationsUsers.json";

        public NotificationsUsersFileRepository() { }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void DeleteNotificationsUsersByNotificationID(string id)
        {
            ObservableCollection<NotificationsUsers> notificationsUsers = GetAll();
            foreach (NotificationsUsers n in notificationsUsers.ToList())
                if (n.NotificationID.Equals(id))
                    notificationsUsers.Remove(n);

            Serialize(notificationsUsers);
        }

        public ObservableCollection<NotificationsUsers> GetAll()
        {
            ObservableCollection<NotificationsUsers> notificationsUsers;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                notificationsUsers = (ObservableCollection<NotificationsUsers>)serializer.Deserialize(file, typeof(ObservableCollection<NotificationsUsers>));
            }

            if (notificationsUsers == null)
                notificationsUsers = new ObservableCollection<NotificationsUsers>();

            return notificationsUsers;
        }

        public NotificationsUsers GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<NotificationsUsers> GetNotificationRecipientsByIDNotification(string id)
        {
            ObservableCollection<NotificationsUsers> notificationsUsers = GetAll();
            ObservableCollection<NotificationsUsers> recipients = new ObservableCollection<NotificationsUsers>();
            foreach(NotificationsUsers nu in notificationsUsers)
                if (nu.NotificationID.Equals(id))
                    recipients.Add(nu);

            return recipients;
        }

        public void Save(NotificationsUsers notification)
        {
            ObservableCollection<NotificationsUsers> notificationsUsers = this.GetAll();
            notificationsUsers.Add(notification);
            Serialize(notificationsUsers);
        }

        public void Serialize(ObservableCollection<NotificationsUsers> notificationsUsers)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, notificationsUsers);
            }
        }
    }
}
