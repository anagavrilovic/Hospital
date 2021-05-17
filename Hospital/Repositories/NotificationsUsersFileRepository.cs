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
    public class NotificationsUsersFileRepository : IFileRepository<NotificationsUsers>
    {
        private string fileName = "notificationsUsers.json";

        public NotificationsUsersFileRepository() { }

        public void Delete(string id)
        {
            throw new NotImplementedException();
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
