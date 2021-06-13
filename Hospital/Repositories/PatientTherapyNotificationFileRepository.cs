using Hospital.Model;
using Newtonsoft.Json;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public class PatientTherapyNotificationFileRepository : IPatientTherapyNotificationRepository
    {
        private string fileName = "patientNotificationsStorage.json";

        public PatientTherapyNotificationFileRepository() { }

        public void Delete(string id)
        {
            List<PatientTherapyMedicineNotification> patientNotifications = GetAll();
            foreach (PatientTherapyMedicineNotification r in patientNotifications)
            {
                if (r.ID.Equals(id))
                {
                    patientNotifications.Remove(r);
                    Serialize(patientNotifications);
                    return;
                }
            }
        }

        public List<PatientTherapyMedicineNotification> GetAll()
        {
            List<PatientTherapyMedicineNotification> patientNotifications;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                patientNotifications = (List<PatientTherapyMedicineNotification>)serializer.Deserialize(file, typeof(List<PatientTherapyMedicineNotification>));
            }

            if (patientNotifications == null)
                patientNotifications = new List<PatientTherapyMedicineNotification>();

            return patientNotifications;
        }

        public PatientTherapyMedicineNotification GetByID(string id)
        {
            List<PatientTherapyMedicineNotification> patientTherapyMedicineNotifications = GetAll();
            foreach (PatientTherapyMedicineNotification notification in patientTherapyMedicineNotifications)
                if (notification.ID.Equals(id))
                    return notification;

            return null;
        }

        public void Save(PatientTherapyMedicineNotification notification)
        {
            List<PatientTherapyMedicineNotification> petientNotifications = GetAll();
            petientNotifications.Add(notification);
            Serialize(petientNotifications);
        }

        public void Serialize(List<PatientTherapyMedicineNotification> patientNotifications)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, patientNotifications);
            }
        }

        public List<PatientTherapyMedicineNotification> GetByPatientID()
        {
            List<PatientTherapyMedicineNotification> notifications = GetAll();
            List<PatientTherapyMedicineNotification> patientsNotifications = new List<PatientTherapyMedicineNotification>();
            foreach (PatientTherapyMedicineNotification notification in notifications)
            {
                if (notification.IDpatient.Equals(MainWindow.IDnumber)) patientsNotifications.Add(notification);
            }
            return patientsNotifications;
        }

        public void Update(PatientTherapyMedicineNotification patientTherapyMedicineNotification)
        {
            Delete(patientTherapyMedicineNotification.ID);
            Save(patientTherapyMedicineNotification);

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
            List<PatientTherapyMedicineNotification> patientTherapyMedicineNotifications = GetAll();
            foreach (PatientTherapyMedicineNotification notification in patientTherapyMedicineNotifications)
            {
                if (notification.ID.Equals(ID)) return true;
            }
            return false;
        }
    }
}
