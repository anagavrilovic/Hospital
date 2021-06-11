using Hospital.DTO;
using Hospital.Model;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ObserverPattern
{
    public class Subject
    {
        private List<Observer> observers = new List<Observer>();
        private NotificationService service;

        public Subject()
        {
            service = new NotificationService();
        }

        public void Attach(Observer observer)
        {
            observers.Add(observer);
        }

        public void Deattach(Observer observer)
        {
            observers.Remove(observer);
        }

        public void UpdateNotification(NotificationRecipientsDTO notificationRecipients,Notification notification)
        {
            service.UpdateNotification(notificationRecipients, notification);
            NotifyAllObservers();
        }

        public void CreateNotification(NotificationRecipientsDTO notificationRecipients, Notification notification)
        {
            service.CreateNotification(notificationRecipients, notification);
            NotifyAllObservers();
        }

        public void DeleteNotification(Notification notification)
        {
            service.DeleteNotification(notification);
            NotifyAllObservers();
        }


        public void NotifyAllObservers()
        {
            foreach (Observer observer in observers)
                observer.Update();
        }
    }
}
