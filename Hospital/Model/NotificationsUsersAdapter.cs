using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    class NotificationsUsersAdapter : IPatientNotification
    {
        private readonly NotificationsUsers notification;

        public NotificationsUsersAdapter(NotificationsUsers notification)
        {
            this.notification = notification;
        }

        public String ID
        {
            get
            {
                return notification.NotificationID;
            }

            set
            {
                notification.NotificationID = value;
            }
        }

        public String Times
        {
            get
            {
                return "Samo jednom";
            }
            set
            {

            }
        }

        public String Name
        {
            get
            {
                return notification.Notification.Title;
            }
            set
            {
                notification.Notification.Title = value;
            }
        }

        public String Text
        {
            get
            {
                return notification.Notification.Content;
            }
            set
            {
                notification.Notification.Content = value;
            }
        }

        public String Duration
        {
            get
            {
                return notification.Notification.Date.ToString();
            }
            set
            {
                notification.Notification.Date = DateTime.Parse(value);
            }
        }


        public Boolean Active
        {
            get
            {
                return !notification.Read;
            }
            set
            {
                notification.Read = !value;
            }
        }


        public Boolean Read
        {
            get
            {
                return notification.Read;
            }
            set
            {
                notification.Read = value;
            }
        }
    }
}
