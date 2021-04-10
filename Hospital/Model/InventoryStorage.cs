// File:    InventoryStorage.cs
// Author:  Marija
// Created: Tuesday, March 23, 2021 10:19:32 PM
// Purpose: Definition of Class InventoryStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Hospital
{
    public class InventoryStorage
    { 
        public InventoryStorage()
        {
          this.fileName = "inventory.json";
        }
    
       public ObservableCollection<Inventory> GetAll()
       {   
            using (StreamReader sr = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                inventory = JsonConvert.DeserializeObject<ObservableCollection<Inventory>>(sr.ReadToEnd());
            }

            return inventory;
        }
   
       public void Save(Inventory parameter1)
       {
            inventory = GetAll();

            if (inventory == null)
            {
                inventory = new ObservableCollection<Inventory>();
            }

            inventory.Add(parameter1);

            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, inventory);
            }
        }
   
       public Boolean Delete(string id)
       {
            inventory = GetAll();
            foreach (Inventory i in inventory)
            {
                if (i.Id.Equals(id))
                {
                  //  Console.WriteLine("*********");
                    inventory.Remove(i);
                    using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, inventory);
                    }
                    return true;
                }
            }
            return false;
        }
   
       public Inventory GetOne(string id)
       {
            foreach (Inventory i in inventory)
            {
                if (i.Id.Equals(id))
                {
                    return i;
                }
            }
            return null;
        }
   
       public ObservableCollection<Inventory> GetByRoomID(string id)
       {
            inventory = GetAll();
            ObservableCollection<Inventory> ret = new ObservableCollection<Inventory>();

            foreach (Inventory i in inventory)
            {
                if (i.RoomID.Equals(id))
                {
                  //  Console.WriteLine("++++");
                    ret.Add(i);
                }
            }

            return ret;
        }
   
       public bool UpdateInventory(string firstRoomID, string secondRoomID, int quantity)
       {
          throw new NotImplementedException();
       }

        public static ObservableCollection<Inventory> inventory = new ObservableCollection<Inventory>();
        public String fileName;
    }
}

