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
        private String fileName;

        public NotificationStorage(String file = "notifications.json")
        {
            this.fileName = file;

        }

        public ObservableCollection<Notification> GetAll()
        {
            ObservableCollection<Notification> notifications;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                notifications = (ObservableCollection<Notification>)serializer.Deserialize(file, typeof(ObservableCollection<Notification>));
            }

            return notifications;
        }

        public void Save(Notification parameter1)
        {
            ObservableCollection<Notification> notifications = this.GetAll();
            notifications.Add(parameter1);
            DoSerialization(notifications);
        }

        /*public Boolean Delete(string id)
        {
              ObservableCollection<Notification> notifications = GetAll();
              foreach(Notification n in notifications)
              {
                  if (n.Id.Equals(id))
                  {
                      notifications.Remove(n);
                      DoSerialization(notifications);
                      return true;
                  }
              }
              return false;
        }*/

        public Notification GetOne(string id)
        {
            ObservableCollection<Notification> notifications = GetAll();
            foreach (Notification n in notifications)
            {
                if (n.Id.Equals(id))
                {
                    return n;
                }
            }

            return null;
        }

        public void DoSerialization(ObservableCollection<Notification> notifications)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, notifications);
            }
        }
    }
}
