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
          this._fileName = "inventory.json";
        }
    
       public ObservableCollection<Inventory> GetAll()
       {   
            using (StreamReader sr = File.OpenText(@"..\\..\\Files\\" + _fileName))
            {
                inventory = JsonConvert.DeserializeObject<ObservableCollection<Inventory>>(sr.ReadToEnd());
            }

            if (inventory == null)
                inventory = new ObservableCollection<Inventory>();

            return inventory;
        }
   
       public void Save(Inventory parameter1)
       {
            inventory = GetAll();
            inventory.Add(parameter1);

            DoSerialization();
        }
   
       public Boolean Delete(string id, string roomID)
       {
            inventory = GetAll();
            foreach (Inventory inv in inventory)
            {
                if (inv.Id.Equals(id) && inv.RoomID.Equals(roomID))
                {
                    inventory.Remove(inv);
                    DoSerialization();
                    return true;
                }
            }
            return false;
        }

        public void EditItem(Inventory editedItem)
        {
            inventory = GetAll();
            foreach (Inventory item in inventory)
            {
                if (item.Id.Equals(editedItem.Id) && item.RoomID.Equals(editedItem.RoomID))
                {
                    inventory.Remove(item);
                    inventory.Add(editedItem);
                    DoSerialization();
                    break;
                }
            }
        }

        public Inventory GetOne(string id)
       {
            foreach (Inventory inv in inventory)
            {
                if (inv.Id.Equals(id))
                    return inv;
            }
            return null;
        }

        public Inventory GetOneByRoom(string id, string roomId)
        {
            foreach (Inventory inv in inventory)
            {
                if (inv.Id.Equals(id) && inv.RoomID.Equals(roomId))
                    return inv;
            }
            return null;
        }

        public ObservableCollection<Inventory> GetByRoomID(string id)
        {
            inventory = GetAll();
            ObservableCollection<Inventory> ret = new ObservableCollection<Inventory>();

            foreach (Inventory inv in inventory)
            {
                if (inv.RoomID.Equals(id))
                    ret.Add(inv);
            }

            return ret;
        }
   
        public void DoSerialization()
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + _fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, inventory);
            }
        }

        public static ObservableCollection<Inventory> inventory = new ObservableCollection<Inventory>();
        public String _fileName;
    }
}

