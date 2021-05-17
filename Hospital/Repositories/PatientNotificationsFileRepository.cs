using Hospital.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public class PatientNotificationsFileRepository : IFileRepository<PatientTherapyMedicineNotification>
    {
        private string fileName = "patientNotificationsStorage.json";

        public PatientNotificationsFileRepository() { }

        public void Delete(string id)
        {
            ObservableCollection<PatientTherapyMedicineNotification> patientNotifications = GetAll();
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

        public ObservableCollection<PatientTherapyMedicineNotification> GetAll()
        {
            ObservableCollection<PatientTherapyMedicineNotification> patientNotifications;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                patientNotifications = (ObservableCollection<PatientTherapyMedicineNotification>)serializer.Deserialize(file, typeof(ObservableCollection<PatientTherapyMedicineNotification>));
            }

            if (patientNotifications == null)
                patientNotifications = new ObservableCollection<PatientTherapyMedicineNotification>();

            return patientNotifications;
        }

        public PatientTherapyMedicineNotification GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public void Save(PatientTherapyMedicineNotification notification)
        {
            ObservableCollection<PatientTherapyMedicineNotification> petientNotifications = GetAll();
            petientNotifications.Add(notification);
            Serialize(petientNotifications);
        }

        public void Serialize(ObservableCollection<PatientTherapyMedicineNotification> patientNotifications)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, patientNotifications);
            }
        }
    }
}
