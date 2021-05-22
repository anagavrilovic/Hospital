using Newtonsoft.Json;
using System;
using Hospital.Repositories.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    class MedicalRecordFileRepository : IMedicalRecordRepository
    {
        private string fileName = "medicalRecords.json";

        public MedicalRecordFileRepository() { }

        public void Delete(string medicalRecordID)
        {
            ObservableCollection<MedicalRecord> medicalRecords = GetAll();
            foreach (MedicalRecord r in medicalRecords)
            {
                if (r.MedicalRecordID.Equals(medicalRecordID))
                {
                    medicalRecords.Remove(r);
                    Serialize(medicalRecords);
                    return;
                }
            }
        }

        public ObservableCollection<MedicalRecord> GetAll()
        {
            ObservableCollection<MedicalRecord> medicalRecords;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                medicalRecords = (ObservableCollection<MedicalRecord>)serializer.Deserialize(file, typeof(ObservableCollection<MedicalRecord>));
            }

            if (medicalRecords == null)
                medicalRecords = new ObservableCollection<MedicalRecord>();

            return medicalRecords;
        }

        public MedicalRecord GetByID(string medicalRecordID)
        {
            ObservableCollection<MedicalRecord> records = GetAll();
            foreach (MedicalRecord r in records)
                if (r.MedicalRecordID.Equals(medicalRecordID))
                    return r;

            return null;
        }

        public void Save(MedicalRecord medicalRecord)
        {
            ObservableCollection<MedicalRecord> medicalRecords = GetAll();
            medicalRecords.Add(medicalRecord);
            Serialize(medicalRecords);
        }

        public void Serialize(ObservableCollection<MedicalRecord> medicalRecords)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, medicalRecords);
            }
        }

        public void UpdatePatientsInformation(MedicalRecord updatedRecord)
        {
            ObservableCollection<MedicalRecord> medicalRecords = GetAll();
            foreach(MedicalRecord medicalRecord in medicalRecords)
            {
                if (medicalRecord.HasSameIDAs(updatedRecord))
                {
                    medicalRecord.DeepCopy(updatedRecord);
                    break;
                }
            }
            Serialize(medicalRecords);
        }

        public List<int> GetExistingIDs()
        {
            ObservableCollection<MedicalRecord> medicalRecords = GetAll();
            List<int> existingIDs = new List<int>();

            foreach (MedicalRecord medicalRecord in medicalRecords)
                existingIDs.Add(Int32.Parse(medicalRecord.MedicalRecordID));

            return existingIDs;
        }

        public MedicalRecord GetByUsername(string username)
        {
            ObservableCollection<MedicalRecord> records = GetAll();
            foreach (MedicalRecord r in records)
                if (r.Patient.Username.Equals(username))
                    return r;

            return null;
        }
    }
}
