// File:    MedicineStorage.cs
// Author:  Ana Gavrilovic
// Created: Tuesday, March 23, 2021 10:19:32 PM
// Purpose: Definition of Class MedicineStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Hospital
{
    public class MedicineStorage
    {
        private String fileName;

        public MedicineStorage(String file = "medicine.json")
        {
            this.fileName = file;

        }

        public ObservableCollection<Medicine> GetAll()
        {
            ObservableCollection<Medicine> medicines;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                medicines = (ObservableCollection<Medicine>)serializer.Deserialize(file, typeof(ObservableCollection<Medicine>));
            }

            return medicines;
        }

        public void Save(Medicine parameter1)
        {
            ObservableCollection<Medicine> medicines = GetAll();
            medicines.Add(parameter1);
            DoSerialization(medicines);
        }

        public Boolean Delete(string id)
        {
              ObservableCollection<Medicine> records = GetAll();
              foreach(Medicine r in records)
              {
                  if (r.ID.Equals(id))
                  {
                      records.Remove(r);
                      DoSerialization(records);
                      return true;
                  }
              }
              return false;
        }

        public Medicine GetOne(string id)
        {
            ObservableCollection<Medicine> medicines = GetAll();
            foreach (Medicine d in medicines)
            {
                if (d.ID.Equals(id))
                {
                    return d;
                }
            }

            return null;
        }
        public void EditMedicine(Medicine editedMedicine)
        {
            ObservableCollection<Medicine> medicines = GetAll();
            foreach (Medicine medicine in medicines)
            {
                if (editedMedicine.ID.Equals(medicine.ID))
                {
                    medicines.Remove(medicine);
                    medicines.Add(editedMedicine);
                    DoSerialization(medicines);
                    break;
                }
            }
        }

        public void DoSerialization(ObservableCollection<Medicine> medicines)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, medicines);
            }
        }

    }
}
