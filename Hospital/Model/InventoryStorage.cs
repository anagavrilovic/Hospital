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

            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, inventory);
            }
        }
   
       public Boolean Delete(string id, string roomID)
       {
            inventory = GetAll();
            foreach (Inventory i in inventory)
            {
                if (i.Id.Equals(id) && i.RoomID.Equals(roomID))
                {
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
                    ret.Add(i);
                }
            }

            return ret;
        }
   
       public void UpdateInventory(Inventory fromFirstRoom, string secondRoomID, int quantity)
       {
            if (fromFirstRoom.RoomID.Equals(secondRoomID))
            {
                return;
            }

            if (fromFirstRoom.Quantity >= quantity)
            {
                ObservableCollection<Inventory> inventar = new ObservableCollection<Inventory>();
                inventar = this.GetByRoomID(secondRoomID);

                bool found = false;

                foreach (Inventory i in inventar)
                {
                    if (i.Name.ToLower().Equals(fromFirstRoom.Name.ToLower()))
                    {
                        i.Quantity += quantity;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    Inventory newItem = new Inventory
                    {
                        Id = fromFirstRoom.Id,
                        Name = fromFirstRoom.Name,
                        Quantity = quantity,
                        Price = fromFirstRoom.Price,
                        RoomID = secondRoomID
                    };
                    inventory.Add(newItem);
                }


                foreach (Inventory i in StaticInventory.Inventory)
                {
                    if (i.Id.Equals(fromFirstRoom.Id) && i.RoomID.Equals(fromFirstRoom.RoomID))
                    {
                        i.Quantity -= quantity;
                    }
                }

                foreach (Inventory i in inventory)
                {
                    if (i.Id.Equals(fromFirstRoom.Id) && i.RoomID.Equals(fromFirstRoom.RoomID))
                    {
                        i.Quantity -= quantity;
                    }
                }

                using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, inventory);
                }
            }
            else
            {
                MessageBox.Show("Pogrešan unos količine!");
            }
        }

        public static ObservableCollection<Inventory> inventory = new ObservableCollection<Inventory>();
        public String fileName;
    }
}

