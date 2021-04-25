// File:    InventoryStorage.cs
// Author:  Marija
// Created: Tuesday, March 23, 2021 10:19:32 PM
// Purpose: Definition of Class InventoryStorage

using Hospital.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

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

            doSerialization();
        }
   
       public Boolean Delete(string id, string roomID)
       {
            inventory = GetAll();
            foreach (Inventory i in inventory)
            {
                if (i.Id.Equals(id) && i.RoomID.Equals(roomID))
                {
                    inventory.Remove(i);
                    doSerialization();
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

        public Inventory GetOneByRoom(string id, string roomId)
        {
            foreach (Inventory i in inventory)
            {
                if (i.Id.Equals(id) && i.RoomID.Equals(roomId))
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
                    ret.Add(i);
                }
            }

            return ret;
        }
   
      
        public void doSerialization()
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, inventory);
            }
        }

        public static ObservableCollection<Inventory> inventory = new ObservableCollection<Inventory>();
        public String fileName;
    }
}

