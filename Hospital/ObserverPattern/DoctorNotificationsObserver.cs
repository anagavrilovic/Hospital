using Hospital.Model;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ObserverPattern
{
    public class DoctorNotificationsObserver : Observer
    {
        private List<Notification> notifications;
        private Doctor doctor;
        private NotificationService service;
        public DoctorNotificationsObserver(Subject subject,Doctor doctor)
        {
            this.subject = subject;
            this.doctor = doctor;
            this.notifications = service.SetNotificationsProperty(doctor);
            service = new NotificationService();
        }
        public override void Update()
        {
            notifications=service.SetNotificationsProperty(doctor);
        }

        public List<Notification> GetNotifications()
        {
            return notifications;
        }
    }
}
