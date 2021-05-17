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
    class DynamicInventoryFileRepository : IFileRepository<DynamicInventory>
    {
        private string fileName = "medicalSupply.json";
        public static ObservableCollection<DynamicInventory> DynamicInventory;

        public DynamicInventoryFileRepository() { }

        public void Delete(string dynamicInventoryID)
        {
            DynamicInventory = GetAll();

            foreach (DynamicInventory di in DynamicInventory)
            {
                if (di.Id.Equals(dynamicInventoryID))
                {
                    DynamicInventory.Remove(di);
                    Serialize(DynamicInventory);
                    return;
                }
            }
        }

        public ObservableCollection<DynamicInventory> GetAll()
        {
            using (StreamReader sr = File.OpenText(@"..\\..\\Files\\" + fileName))
                DynamicInventory = JsonConvert.DeserializeObject<ObservableCollection<DynamicInventory>>(sr.ReadToEnd());

            if (DynamicInventory == null)
                DynamicInventory = new ObservableCollection<DynamicInventory>();

            return DynamicInventory;
        }

        public DynamicInventory GetByID(string dynamicInventoryID)
        {
            DynamicInventory = GetAll();

            foreach (DynamicInventory di in DynamicInventory)
                if (di.Id.Equals(dynamicInventoryID))
                    return di;

            return null;
        }

        public void Save(DynamicInventory dynamicInventory)
        {
            DynamicInventory = GetAll();
            DynamicInventory.Add(dynamicInventory);
            Serialize(DynamicInventory);
        }

        public void Serialize(ObservableCollection<DynamicInventory> dynamicInventories)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, DynamicInventory);
            }
        }
    }
}
