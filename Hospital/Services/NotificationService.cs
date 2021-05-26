﻿using Hospital.DTO;
using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class NotificationService
    {
        private INotificationRepository notificationRepository;
        private INotificationsUsersRepository notificationsUsersRepository;

        private RegistratedUserService registratedUserService = new RegistratedUserService();
        private MedicalRecordService medicalRecordService = new MedicalRecordService();

        public NotificationService()
        {
            notificationRepository = new NotificationFileRepository();
            notificationsUsersRepository = new NotificationsUsersFileRepository();
        }

        public List<Notification> GetAllNotificationsSortedDescending()
        {
            return notificationRepository.GetAllNotificationsSortedDescending();
        }

        public void DeleteNotification(Notification notification)
        {
            notificationsUsersRepository.DeleteNotificationsUsersByNotificationID(notification.Id);
            notificationRepository.Delete(notification.Id);
        }

        public int CountNotificationByRole(Notification notification, UserType userType)
        {
            List<NotificationsUsers> recipientsOfRequestedNotification = notificationsUsersRepository.GetNotificationRecipientsByIDNotification(notification.Id);
            int count = 0;
            foreach(NotificationsUsers recipient in recipientsOfRequestedNotification)
                if (registratedUserService.GetRoleByUsername(recipient.Username) == userType)
                    count += 1;

            return count;
        }

        public bool IsEveryUserOfRoleReceivingNotification(Notification notification, UserType userType)
        {
            return CountNotificationByRole(notification, userType) == registratedUserService.CountByRole(userType);
        }

        public List<MedicalRecord> GetPatientRecipientsRecords(Notification notification)
        {
            List<NotificationsUsers> recipientsOfRequestedNotification = notificationsUsersRepository.GetNotificationRecipientsByIDNotification(notification.Id);
            List<MedicalRecord> recipientsRecords = new List<MedicalRecord>();

            foreach (NotificationsUsers recipient in recipientsOfRequestedNotification)
                if (registratedUserService.GetRoleByUsername(recipient.Username) == UserType.patient)
                    recipientsRecords.Add(medicalRecordService.GetRecordByUsername(recipient.Username));

            return recipientsRecords;
        }

        public void ClearRecipientsOfNotification(Notification notification)
        {
            notificationsUsersRepository.DeleteNotificationsUsersByNotificationID(notification.Id);
        }

        public void UpdateNotification(NotificationRecipientsDTO recipients, Notification notification)
        {
            notificationRepository.Update(notification);
            ClearRecipientsOfNotification(notification);
            SendNotification(recipients, notification);  
        }

        public void CreateNotification(NotificationRecipientsDTO recipients, Notification notification)
        {
            notificationRepository.Save(notification);
            SendNotification(recipients, notification); 
        }

        private void SendNotification(NotificationRecipientsDTO recipients, Notification notification)
        {
            SendNotificationByRole(recipients, notification);
            SendCustomNotification(recipients, notification);
        }

        private void SendNotificationByRole(NotificationRecipientsDTO recipients, Notification notification)
        {
            List<RegistratedUser> registratedUsers = registratedUserService.GetAllRegistratedUsers();
            if (recipients.IsEverySecretaryRecipient || recipients.IsEveryDoctorRecipient || recipients.IsEveryManagerRecipient || recipients.IsEveryPatientRecipient)
            {
                foreach (RegistratedUser user in registratedUsers)
                {
                    NotificationsUsers notificationsUsers = new NotificationsUsers { NotificationID = notification.Id, Username = user.Username, Read = false };

                    if(IsUserMatching(user, recipients))
                        notificationsUsersRepository.Save(notificationsUsers);
                }
            }
        }

        private bool IsUserMatching(RegistratedUser user, NotificationRecipientsDTO recipients)
        {
            return (recipients.IsEverySecretaryRecipient && user.Type.Equals(UserType.secretary)) ||
                (recipients.IsEveryDoctorRecipient && user.Type.Equals(UserType.doctor)) ||
                (recipients.IsEveryManagerRecipient && user.Type.Equals(UserType.manager)) ||
                (recipients.IsEveryPatientRecipient && user.Type.Equals(UserType.patient));
        }

        public void SendCustomNotification(NotificationRecipientsDTO recipients, Notification notification)
        {
            if (!recipients.IsEveryPatientRecipient)
            {
                foreach (MedicalRecord record in recipients.RecipientsRecords)
                {
                    NotificationsUsers notificationsUsers = new NotificationsUsers { NotificationID = notification.Id, Username = record.Patient.Username, Read = false };
                    notificationsUsersRepository.Save(notificationsUsers);
                }
            }  
        }

        public string GenerateID()
        {
            List<int> existingIDs = notificationRepository.GetExistingIDs();
            int newID = 1;
            while (true)
            {
                if (!existingIDs.Contains(newID))
                    break;

                newID += 1;
            }

            return newID.ToString();
        }

        public Notification GetById(string id)
        {
            return notificationRepository.GetByID(id);
        }

        public List<Notification> SetNotificationsProperty(Doctor doctor)
        {
            notificationsUsersRepository = new NotificationsUsersFileRepository();
            List<Notification> notifications = new List<Notification>();
            foreach (NotificationsUsers notificationsUser in notificationsUsersRepository.GetAll())
            {
                if (notificationsUser.Username.Equals(doctor.Username))
                {
                    notifications.Add(GetById(notificationsUser.NotificationID));
                }

            }
            return notifications;
        }
    }
}
