﻿using Hospital.DTO;
using Hospital.Factory;
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
            notificationsUsersRepository.Delete(notification.Id);
            notificationRepository.Delete(notification.Id);
        }

        public void DeleteNotificationsUsersByNotificationID(string id)
        {
            notificationsUsersRepository.Delete(id);
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
            notificationsUsersRepository.Delete(notification.Id);
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

        public void NotifyPatientAboutRescheduledAppointment(Appointment appointment, DateTime newTime)
        {
            Notification notification = GenerateNotificationForPatientsRescheduledAppointment(appointment, newTime);
            NotificationsUsers notificationsUsers = new NotificationsUsers(notification.Id, new MedicalRecordService().GetUsernameByIDPatient(appointment.IDpatient));

            notificationRepository.Save(notification);
            notificationsUsersRepository.Save(notificationsUsers);
        }

        public void NotifyPatientAboutRescheduledAppointmentBeacuseOfRoomRenovation(Appointment appointment)
        {
            Notification notification = GenerateNotificationForPatientsRescheduledAppointmentBecauseOfRenovation(appointment);
            NotificationsUsers notificationsUsers = new NotificationsUsers(notification.Id, new MedicalRecordService().GetUsernameByIDPatient(appointment.IDpatient));

            notificationRepository.Save(notification);
            notificationsUsersRepository.Save(notificationsUsers);
        }

        public void NotifyDoctorAboutAppointmentRoomUpdate(Appointment updatedAppointment)
        {
            Notification notification = GenerateNotificationForDoctorsRescheduledAppointmentBecauseOfRenovation(updatedAppointment);
            NotificationsUsers notificationsUsers = new NotificationsUsers(notification.Id, new DoctorService(new DoctorFileRepository()).GetUsernameByIDDoctor(updatedAppointment.IDDoctor));

            notificationRepository.Save(notification);
            notificationsUsersRepository.Save(notificationsUsers);
        }

        private Notification GenerateNotificationForPatientsRescheduledAppointmentBecauseOfRenovation(Appointment appointment)
        {
            StringBuilder stringBuilder = new StringBuilder("");
            string title = "Izmena prostorije u kojoj se održava pregled zbog zakazanog renoviranja";
            string content = "";
           
             stringBuilder.Append("Poštovani, ").AppendLine().AppendLine().Append("Obaveštavamo Vas da je Vaš pregled koji je zakazan dana ").
             Append(appointment.DateTime.ToString("dd.MM.yyyy.")).Append(" u ").Append(appointment.DateTime.ToString(appointment.DateTime.ToString("HH:mm"))).
             Append(" časova, premešten u prostoriju ").Append(appointment.Room.Name).
             Append(", jer je u vreme Vašeg termina zakazano renoviranje prvobitne prostorije.").AppendLine().AppendLine().
             Append("Za više informacija pozovite našeg sekretara na broj 06485625952 ili nam pišite e-mailom.").AppendLine().AppendLine().
             Append("Srdačan pozdrav, Vaša ZDRAVO bolnica");
             content = stringBuilder.ToString();
             content = stringBuilder.ToString();
          
            return new Notification { Title = title, Content = content, Date = DateTime.Now, Id = GenerateID() };
        }

        private Notification GenerateNotificationForDoctorsRescheduledAppointmentBecauseOfRenovation(Appointment appointment)
        {
            StringBuilder stringBuilder = new StringBuilder("");
            string title = "Izmena prostorije u kojoj se održava pregled zbog zakazanog renoviranja";
            string content = "";

            stringBuilder.Append("Zdravo, ").AppendLine().AppendLine().Append("Vaš pregled koji je zakazan dana ").
            Append(appointment.DateTime.ToString("dd.MM.yyyy.")).Append(" u ").Append(appointment.DateTime.ToString(appointment.DateTime.ToString("HH:mm"))).
            Append(" časova, premešten je u prostoriju ").Append(appointment.Room.Name).
            Append(", jer je u vreme termina zakazano renoviranje prvobitne prostorije.").AppendLine().AppendLine().
            Append("Pozdrav!");
            content = stringBuilder.ToString();
            content = stringBuilder.ToString();

            return new Notification { Title = title, Content = content, Date = DateTime.Now, Id = GenerateID() };
        }

        public void NotifyDoctor(Appointment newUrgentAppointment)
        {
            Notification notification = GenerateNotificationForDoctorsUrgentAppointment(newUrgentAppointment);
            NotificationsUsers notificationsUsers = new NotificationsUsers(notification.Id, new DoctorService(new DoctorFileRepository()).GetUsernameByIDDoctor(newUrgentAppointment.IDDoctor));

            notificationRepository.Save(notification);
            notificationsUsersRepository.Save(notificationsUsers);
        }

        public void NotifyPatientAboutCancelingAppointmentBecauseOfShiftChange(Appointment appointment)
        {
            Notification notification = GenerateNotificationForCancelingAppointmentBecauseOfShiftChange(appointment);
            NotificationsUsers notificationsUsers = new NotificationsUsers(notification.Id, new MedicalRecordService().GetUsernameByIDPatient(appointment.IDpatient));

            notificationRepository.Save(notification);
            notificationsUsersRepository.Save(notificationsUsers);
        }

        private Notification GenerateNotificationForCancelingAppointmentBecauseOfShiftChange(Appointment appointment)
        {
            StringBuilder stringBuilder = new StringBuilder("");
            string title = "Otkazivanje termina zbog promene smene lekara";
            string content = "";

            stringBuilder.Append("Poštovani, ").AppendLine().Append("Obaveštavamo Vas da je Vaš termin, koji se trebao održati dana ")
                .Append(appointment.DateTime.ToString("dd.MM.yyyy.")).Append(" u ").Append(appointment.DateTime.ToString("hh:mm"))
                .Append(", otkazan zbog promene smene lekara koji ga je trebao održati. Za više informacija ili ponovno zakazivanje termina, " +
                "kontaktirajte našeg sekretara na broj 06485625952 ili nam pišite e-mailom.").AppendLine().AppendLine()
                .Append("Srdačan pozdrav, Vaša ZDRAVO bolnica");
            content = stringBuilder.ToString();

            return new Notification { Title = title, Content = content, Date = DateTime.Now, Id = GenerateID() };
        }

        private Notification GenerateNotificationForPatientsRescheduledAppointment(Appointment appointment, DateTime newTime)
        {
            StringBuilder stringBuilder = new StringBuilder("");
            string title = "";
            string content = "";

            if (appointment.Type.Equals(AppointmentType.urgentExamination) || appointment.Type.Equals(AppointmentType.examination))
            {
                title = "Promena zakazanog termina pregleda";

                stringBuilder.Append("Poštovani, ").AppendLine().AppendLine().Append("Obaveštavamo Vas da je Vaš pregled koji se trebao održati dana ").
                    Append(appointment.DateTime.ToString("dd.MM.yyyy.")).Append(" u ").Append(appointment.DateTime.ToString(appointment.DateTime.ToString("HH:mm"))).
                    Append(" časova, odložen na ").Append(newTime.ToString("dd.MM.yyyy.")).Append(" u ").Append(appointment.DateTime.ToString(newTime.ToString("HH:mm"))).
                    Append(" časova, jer se u vreme Vašeg termina dogodio hitan slučaj.").AppendLine().AppendLine().
                    Append("Za više informacija pozovite našeg sekretara na broj 06485625952 ili nam pišite e-mailom.").AppendLine().AppendLine().
                    Append("Srdačan pozdrav, Vaša ZDRAVO bolnica");
                content = stringBuilder.ToString();
            }

            else if (appointment.Type.Equals(AppointmentType.urgentOperation) || appointment.Type.Equals(AppointmentType.operation))
            {
                title = "Promena zakazanog termina operacije";

                stringBuilder.Append("Poštovani, ").AppendLine().AppendLine().Append("Obaveštavamo Vas da je Vaša operacija koja se trebala održati dana ").
                    Append(appointment.DateTime.ToString("dd.MM.yyyy.")).Append(" u ").Append(appointment.DateTime.ToString(appointment.DateTime.ToString("HH:mm"))).
                    Append(" časova, odložena na ").Append(newTime.ToString("dd.MM.yyyy.")).Append(" u ").Append(appointment.DateTime.ToString(newTime.ToString("HH:mm"))).
                    Append(" časova, jer se u vreme Vašeg termina dogodio hitan slučaj.").AppendLine().AppendLine().
                    Append("Za više informacija pozovite našeg sekretara na broj 06485625952 ili nam pišite e-mailom.").AppendLine().AppendLine().
                    Append("Srdačan pozdrav, Vaša ZDRAVO bolnica");
                content = stringBuilder.ToString();
            }

            return new Notification { Title = title, Content = content, Date = DateTime.Now, Id = GenerateID() };
        }

        private Notification GenerateNotificationForDoctorsUrgentAppointment(Appointment newUrgentAppointment)
        {
            StringBuilder stringBuilder = new StringBuilder("");
            string title = "";
            string content = "";

            if (newUrgentAppointment.Type.Equals(AppointmentType.urgentExamination) || newUrgentAppointment.Type.Equals(AppointmentType.examination))
            {
                title = "HITAN PREGLED";
                stringBuilder.Append("Imate hitan pregled u ").Append(newUrgentAppointment.DateTime.ToString("HH:mm")).Append(" , ordinacija \"").Append(newUrgentAppointment.Room.Name).Append("\".");
                content = stringBuilder.ToString();
            }
            else if (newUrgentAppointment.Type.Equals(AppointmentType.urgentOperation) || newUrgentAppointment.Type.Equals(AppointmentType.operation))
            {
                title = "HITNA OPERACIJA";
                stringBuilder.Append("Imate hitanu operaciju u ").Append(newUrgentAppointment.DateTime.ToString("HH:mm")).Append(" , sala \"").Append(newUrgentAppointment.Room.Name).Append("\".");
                content = stringBuilder.ToString();
            }

            return new Notification { Title = title, Content = content, Date = DateTime.Now, Id = GenerateID() };
        }

        public List<NotificationsUsers> GetNotificationByUser(String username)
        {
            List<NotificationsUsers> notificationsUsers = notificationsUsersRepository.GetAll();
            List<NotificationsUsers> notifications = new List<NotificationsUsers>();
            foreach (NotificationsUsers nu in notificationsUsers)
                if (nu.Username.Equals(username))
                {
                    nu.Notification = notificationRepository.GetByID(nu.NotificationID);
                    notifications.Add(nu);
                }
            return notifications;
        }

        public Boolean DoesUserHaveNewNotification(String username)
        {
            List<NotificationsUsers> notifications = GetNotificationByUser(username);
            foreach (NotificationsUsers notification in notifications)
            {
                if (notification.Read == false) return true;
            }
            return false;
        }

        public NotificationsUsers GetUniqueNotificationsUsers(String id,String username)
        {
           return notificationsUsersRepository.GetUniqueNotificationsUsers(id, username);
        }

        public void DeleteUniqueNotificationsUsers(String id, String username)
        {
            notificationsUsersRepository.DeleteUniqueNotificationsUsers(id, username);
        }

        public void UpdateNotificationsUsers(NotificationsUsers notificationsUsers)
        {
            notificationsUsersRepository.UpdateNotificationsUsers(notificationsUsers);
        }

    }
}
