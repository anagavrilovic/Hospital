using Hospital.Model;
using Hospital.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    class PatientNotesNotificationFileRepository : IPatientNotesNotificationRepository
    {
        private string fileName = "patientNotesNotification.json";
        public void Delete(string id)
        {
            List<PatientNotesNotification> patientNotesNotifications = GetAll();
            foreach (PatientNotesNotification notification in patientNotesNotifications)
            {
                if (notification.ID.Equals(id))
                {
                    patientNotesNotifications.Remove(notification);
                    Serialize(patientNotesNotifications);
                    return;
                }
            }
        }

        public List<PatientNotesNotification> GetAll()
        {
            List<PatientNotesNotification> patientNotesNotifications;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                patientNotesNotifications = (List<PatientNotesNotification>)serializer.Deserialize(file, typeof(List<PatientNotesNotification>));
            }

            if (patientNotesNotifications == null)
                patientNotesNotifications = new List<PatientNotesNotification>();

            return patientNotesNotifications;
        }

        public PatientNotesNotification GetByID(string id)
        {
            List<PatientNotesNotification> patientNotesNotifications = GetAll();
            foreach (PatientNotesNotification notification in patientNotesNotifications)
                if (notification.ID.Equals(id))
                    return notification;

            return null;
        }

        public void Save(PatientNotesNotification patientNotesNotification)
        {
            List<PatientNotesNotification> patientNotesNotifications = GetAll();
            patientNotesNotifications.Add(patientNotesNotification);
            Serialize(patientNotesNotifications);
        }

        public void Serialize(List<PatientNotesNotification> notifications)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, notifications);
            }
        }

        public String GetNewID()
        {
            int newID = 0;
            while (true)
            {
                if (!CheckIfIDExists(newID.ToString())) return newID.ToString();
                newID++;
            }
        }

        private Boolean CheckIfIDExists(String ID)
        {
            List<PatientNotesNotification> patientNotesNotifications = GetAll();
            foreach (PatientNotesNotification notification in patientNotesNotifications)
            {
                if (notification.ID.Equals(ID)) return true;
            }
            return false;
        }

        public List<PatientNotesNotification> GetByPatientID()
        {
            List<PatientNotesNotification> notifications = GetAll();
            List<PatientNotesNotification> patientsNotesNotifications = new List<PatientNotesNotification>();
            foreach (PatientNotesNotification notification in notifications)
            {
                if (notification.patientID.Equals(MainWindow.IDnumber)) patientsNotesNotifications.Add(notification);
            }
            return patientsNotesNotifications;
        }
    }
}
