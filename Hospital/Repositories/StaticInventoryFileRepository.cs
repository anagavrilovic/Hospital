using Hospital.Repositories.Interfaces;
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
    class StaticInventoryFileRepository : IStaticInventoryRepository
    {
        private string fileName = "inventory.json";

        public void Delete(string id)
        {
            List<Inventory> inventory = GetAll();
            foreach (Inventory inv in inventory)
            {
                if (inv.Id.Equals(id))
                {
                    inventory.Remove(inv);
                    Serialize(inventory);
                    return;
                }
            }
        }

        public List<Inventory> GetAll()
        {
            List<Inventory> inventory = new List<Inventory>();
            using (StreamReader sr = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                inventory = JsonConvert.DeserializeObject<List<Inventory>>(sr.ReadToEnd());
            }

            if (inventory == null)
                inventory = new List<Inventory>();

            return inventory;
        }

        public Inventory GetByID(string id)
        {
            List<Inventory> inventory = GetAll();
            foreach (Inventory inv in inventory)
                if (inv.Id.Equals(id))
                    return inv;
         
            return null;
        }

        public void Save(Inventory parameter)
        {
            List<Inventory> inventory = GetAll();
            inventory.Add(parameter);

            Serialize(inventory);
        }

        public void Serialize(List<Inventory> parameter)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, parameter);
            }
        }
    }
}
