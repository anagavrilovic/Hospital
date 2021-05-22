// File:    MedicalSupplyStorage.cs
// Author:  Marija
// Created: Tuesday, March 23, 2021 10:19:32 PM
// Purpose: Definition of Class MedicalSupplyStorage

using Hospital.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Hospital
{
    public class DynamicInventoryStorage
    {
        public DynamicInventoryStorage()
        {
            this.fileName = "medicalSupply.json";
        }

       public ObservableCollection<DynamicInventory> GetAll()
       {
            ObservableCollection<DynamicInventory> DynamicInventory = new ObservableCollection<DynamicInventory>();

            using (StreamReader sr = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                DynamicInventory = JsonConvert.DeserializeObject<ObservableCollection<DynamicInventory>>(sr.ReadToEnd());
            }

            if (DynamicInventory == null)
                DynamicInventory = new ObservableCollection<DynamicInventory>();

            return DynamicInventory;
        }
   
       public void Save(DynamicInventory parameter1)
       {
            ObservableCollection<DynamicInventory> DynamicInventory = GetAll();
            DynamicInventory.Add(parameter1);

            DoSerialization(DynamicInventory);
       }
   
       public Boolean Delete(string id, string roomID)
       {
            ObservableCollection<DynamicInventory> DynamicInventory = GetAll();

            foreach(DynamicInventory ms in DynamicInventory)
            {
                if(ms.Id.Equals(id) && ms.RoomID.Equals(roomID))
                {
                    DynamicInventory.Remove(ms);
                    DoSerialization(DynamicInventory);
                    return true;
                }
            }
            return false;
       }

        public void EditItem (DynamicInventory editedItem)
        {
            ObservableCollection<DynamicInventory> DynamicInventory = GetAll();
            foreach (DynamicInventory item in DynamicInventory)
            {
                if(item.Id.Equals(editedItem.Id) && item.RoomID.Equals(editedItem.RoomID))
                {
                    DynamicInventory.Remove(item);
                    DynamicInventory.Add(editedItem);
                    DoSerialization(DynamicInventory);
                    break;
                }
            }
        }

        public DynamicInventory GetOne(string id)
       {
            ObservableCollection<DynamicInventory> DynamicInventory = GetAll();

            foreach(DynamicInventory ms in DynamicInventory)
            {
                if(ms.Id.Equals(id))
                    return ms;
            }
            
            return null;
       }

        public DynamicInventory GetOneByRoom(string id, string roomId)
        {
            ObservableCollection<DynamicInventory> DynamicInventory = GetAll();
            foreach (DynamicInventory ms in DynamicInventory)
            {
                if (ms.Id.Equals(id) && ms.RoomID.Equals(roomId))
                    return ms;
            }
            return null;
        }

        public ObservableCollection<DynamicInventory> GetByRoomID(string id)
       {
            ObservableCollection<DynamicInventory> ret = new ObservableCollection<DynamicInventory>();
            ObservableCollection<DynamicInventory> DynamicInventory = GetAll();

            foreach (DynamicInventory ms in DynamicInventory)
            {
                if(ms.RoomID.Equals(id))
                    ret.Add(ms);
            }

            return ret;
       }

        public void DoSerialization(ObservableCollection<DynamicInventory> DynamicInventory)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, DynamicInventory);
            }
        }

    
       public String fileName;

    }
}

