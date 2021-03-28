using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;


namespace Hospital
{
    public class MedicalRecordStorage
    {
        private String fileName;

        public MedicalRecordStorage(String file = "medicalRecords.json")
        {
            this.fileName = file;

        }

        public ObservableCollection<MedicalRecord> GetAll()
        {
            ObservableCollection<MedicalRecord> records;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                records = (ObservableCollection<MedicalRecord>)serializer.Deserialize(file, typeof(ObservableCollection<MedicalRecord>));
            }

            return records;
        }

        public void Save(MedicalRecord parameter1)
        {
            ObservableCollection<MedicalRecord> records = GetAll();
            records.Add(parameter1);
            DoSerialization(records);
        }

        /*public Boolean Delete(int id)
        {
              ObservableCollection<MedicalRecord> records = GetAll();
              foreach(MedicalRecord r in records)
              {
                  if (r.MedicalRecordID.Equals(id))
                  {
                      records.Remove(r);
                      DoSerialization(records);
                      return true;
                  }
              }
              return false;
        }*/

        public MedicalRecord GetOne(int id)
        {
            ObservableCollection<MedicalRecord> records = GetAll();
            foreach (MedicalRecord r in records)
            {
                if (r.MedicalRecordID.Equals(id))
                {
                    return r;
                }
            }

            return null;
        }

        public void DoSerialization(ObservableCollection<MedicalRecord> mr)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, mr);
            }
        }

        public MedicalRecord GetByPatientID(string id)
        {
            ObservableCollection<MedicalRecord> records = GetAll();
            foreach (MedicalRecord r in records)
            {
                if (r.Patient.PersonalID.Equals(id))
                {
                    return r;
                }
            }

            return null;
        }

    }
}