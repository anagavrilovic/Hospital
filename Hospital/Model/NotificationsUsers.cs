using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class NotificationsUsers
    {
        private string notificationID;

        public string NotificationID
        {
            get { return notificationID; }
            set { notificationID = value; }
        }

        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private bool read;

        public bool Read
        {
            get { return read; }
            set { read = value; }
        }

        public NotificationsUsers()
        {

        }

        public NotificationsUsers(string notificationID, string username)
        {
            this.NotificationID = notificationID;
            this.Username = username;
            this.Read = false;
        }

        [JsonIgnore]
        public Notification Notification { get; set; }
        [JsonIgnore]
        public Patient Patient { get; set; }
    }
}
