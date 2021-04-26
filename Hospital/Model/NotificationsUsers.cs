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
    }
}
