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
       


    }
}
