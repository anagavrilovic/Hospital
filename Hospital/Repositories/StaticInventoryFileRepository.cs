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

        public List<Inventory> GetAllInventoryFromRoom(string id)
        {
            List<Inventory> inventory = GetAll();
            List<Inventory> ret = new List<Inventory>();

            foreach (Inventory inv in inventory)
            {
                if (inv.RoomID.Equals(id))
                    ret.Add(inv);
            }

            return ret;
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

        public void EditItem(Inventory editedItem)
        {
            List<Inventory> inventory = GetAll();
            foreach (Inventory item in inventory)
            {
                if (item.Id.Equals(editedItem.Id) && item.RoomID.Equals(editedItem.RoomID))
                {
                    inventory.Remove(item);
                    inventory.Add(editedItem);
                    Serialize(inventory);
                    break;
                }
            }
        }

        public void DeleteItemFromRoom(string id, string roomID)
        {
            List<Inventory> inventory = GetAll();
            foreach (Inventory inv in inventory)
            {
                if (inv.Id.Equals(id) && inv.RoomID.Equals(roomID))
                {
                    inventory.Remove(inv);
                    Serialize(inventory);
                    return;
                }
            }
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
