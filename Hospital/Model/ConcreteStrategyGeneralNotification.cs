using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    class ConcreteStrategyGeneralNotification : Strategy
    {
        private NotificationsUsersAdapter notification;
        private NotificationService notificationService = new NotificationService();
        private MedicalRecordService medicalRecordService = new MedicalRecordService();
        private String username;

        public ConcreteStrategyGeneralNotification(NotificationsUsersAdapter notification)
        {
            this.notification = notification;
            username = medicalRecordService.GetUsernameByIDPatient(MainWindow.IDnumber);
        }

        public ConcreteStrategyGeneralNotification()
        {
            username = medicalRecordService.GetUsernameByIDPatient(MainWindow.IDnumber);
        }

        void Strategy.Update()
        {
            NotificationsUsers notificationsUsers = notificationService.GetUniqueNotificationsUsers(notification.ID, username);
            notificationsUsers.Read = true;
            notificationService.UpdateNotificationsUsers(notificationsUsers);
        }

        void Strategy.Delete()
        {
            notificationService.DeleteUniqueNotificationsUsers(notification.ID, username);
        }

        List<IPatientNotification> Strategy.GetNotifications()
        {
            List<IPatientNotification> notifications = new List<IPatientNotification>();
            List<NotificationsUsers> generalNotifications = notificationService.GetNotificationByUser(username);
            foreach (NotificationsUsers notification in generalNotifications)
            {
                notifications.Add(new NotificationsUsersAdapter(notification));
            }
            return notifications;
        }
    }
}
