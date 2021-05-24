﻿using Newtonsoft.Json;
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
    class DynamicInventoryFileRepository : IDynamicInventoryRepository
    {
        private string fileName = "medicalSupply.json";

        public DynamicInventoryFileRepository() { }

        public void Delete(string dynamicInventoryID)
        {
            List<DynamicInventory> DynamicInventory = GetAll();

            foreach (DynamicInventory ms in DynamicInventory)
            {
                if (ms.Id.Equals(dynamicInventoryID))
                {
                    DynamicInventory.Remove(ms);
                    Serialize(DynamicInventory);
                    return;
                }
            }
        }

        public List<DynamicInventory> GetAll()
        {
            List<DynamicInventory> DynamicInventory = new List<DynamicInventory>();

            using (StreamReader sr = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                DynamicInventory = JsonConvert.DeserializeObject<List<DynamicInventory>>(sr.ReadToEnd());
            }

            if (DynamicInventory == null)
                DynamicInventory = new List<DynamicInventory>();

            return DynamicInventory;
        }

        public DynamicInventory GetByID(string dynamicInventoryID)
        {
            List<DynamicInventory> DynamicInventory = GetAll();

            foreach (DynamicInventory inv in DynamicInventory)
                if (inv.Id.Equals(dynamicInventoryID))
                    return inv;

            return null;
        }

        public void Save(DynamicInventory dynamicInventory)
        {
            List<DynamicInventory> DynamicInventory = GetAll();
            DynamicInventory.Add(dynamicInventory);

            Serialize(DynamicInventory);
        }

        public void Serialize(List<DynamicInventory> dynamicInventories)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, dynamicInventories);
            }
        }
    }
}
