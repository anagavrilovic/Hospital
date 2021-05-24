﻿using Hospital.Model;
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
            List<NotificationsUsers> notificationsUsers = GetAll();
            foreach (NotificationsUsers n in notificationsUsers.ToList())
                if (n.NotificationID.Equals(id))
                    notificationsUsers.Remove(n);

            Serialize(notificationsUsers);
        }

        public List<NotificationsUsers> GetAll()
        {
            List<NotificationsUsers> notificationsUsers;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                notificationsUsers = (List<NotificationsUsers>)serializer.Deserialize(file, typeof(List<NotificationsUsers>));
            }

            if (notificationsUsers == null)
                notificationsUsers = new List<NotificationsUsers>();

            return notificationsUsers;
        }

        public NotificationsUsers GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public List<NotificationsUsers> GetNotificationRecipientsByIDNotification(string id)
        {
            List<NotificationsUsers> notificationsUsers = GetAll();
            List<NotificationsUsers> recipients = new List<NotificationsUsers>();
            foreach(NotificationsUsers nu in notificationsUsers)
                if (nu.NotificationID.Equals(id))
                    recipients.Add(nu);

            return recipients;
        }

        public void Save(NotificationsUsers notification)
        {
            List<NotificationsUsers> notificationsUsers = this.GetAll();
            notificationsUsers.Add(notification);
            Serialize(notificationsUsers);
        }

        public void Serialize(List<NotificationsUsers> notificationsUsers)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, notificationsUsers);
            }
        }
    }
}
