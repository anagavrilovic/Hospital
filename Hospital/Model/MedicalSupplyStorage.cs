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
    public class MedicalSupplyStorage
    {
        public MedicalSupplyStorage()
        {
            this.fileName = "medicalSupply.json";
        }

       public ObservableCollection<MedicalSupply> GetAll()
       {
            using (StreamReader sr = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                supplies = JsonConvert.DeserializeObject<ObservableCollection<MedicalSupply>>(sr.ReadToEnd());
            }

            if (supplies == null)
                supplies = new ObservableCollection<MedicalSupply>();

            return supplies;
        }
   
       public void Save(MedicalSupply parameter1)
       {
            supplies = GetAll();

            if(supplies == null)
            {
                supplies = new ObservableCollection<MedicalSupply>();
            }

            supplies.Add(parameter1);

            doSerialization();
       }
   
       public Boolean Delete(string id, string roomID)
       {
            supplies = GetAll();

            foreach(MedicalSupply ms in supplies)
            {
                if(ms.Id.Equals(id) && ms.RoomID.Equals(roomID))
                {
                    supplies.Remove(ms);
                    doSerialization();
                    return true;
                }
            }
            return false;
       }
   
       public MedicalSupply GetOne(string id)
       {
            supplies = GetAll();

            foreach(MedicalSupply ms in supplies)
            {
                if(ms.Id.Equals(id))
                {
                    return ms;
                }
            }
            
            return null;
       }

        public MedicalSupply GetOneByRoom(string id, string roomId)
        {
            foreach (MedicalSupply ms in supplies)
            {
                if (ms.Id.Equals(id) && ms.RoomID.Equals(roomId))
                {
                    return ms;
                }
            }
            return null;
        }

        public ObservableCollection<MedicalSupply> GetByRoomID(string id)
       {
            ObservableCollection<MedicalSupply> ret = new ObservableCollection<MedicalSupply>();
            supplies = GetAll();

            foreach(MedicalSupply ms in supplies)
            {
                if(ms.RoomID.Equals(id))
                {
                    ret.Add(ms);
                }
            }

            return ret;
       }
   
       public void UpdateSupply(MedicalSupply fromFirstRoom, string secondRoomID, int quantity)
       {

            if (fromFirstRoom.RoomID.Equals(secondRoomID))
            {
                return;
            }
                

            if (fromFirstRoom.Quantity >= quantity)
            {
                ObservableCollection<MedicalSupply> supply = new ObservableCollection<MedicalSupply>();
                supply = this.GetByRoomID(secondRoomID);

                bool found = false;

                foreach (MedicalSupply ms in supply)
                {
                    if (ms.Name.ToLower().Equals(fromFirstRoom.Name.ToLower()))
                    {
                        ms.Quantity += quantity;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    MedicalSupply newItem = new MedicalSupply
                    {
                        Id = fromFirstRoom.Id,
                        Name = fromFirstRoom.Name,
                        Quantity = quantity,
                        Price = fromFirstRoom.Price,
                        RoomID = secondRoomID,
                        Units = fromFirstRoom.Units
                    };
                    supplies.Add(newItem);
                }
            
               if(!DynamicInventory.Supply.Equals(null))
               {
                    foreach (MedicalSupply ms in DynamicInventory.Supply)
                    {
                        if (ms.Id.Equals(fromFirstRoom.Id) && ms.RoomID.Equals(fromFirstRoom.RoomID))
                        {
                            ms.Quantity -= quantity;
                        }
                    }
                }
         
                foreach (MedicalSupply ms in supplies)
                {
                    if (ms.Id.Equals(fromFirstRoom.Id) && ms.RoomID.Equals(fromFirstRoom.RoomID))
                    {
                        ms.Quantity -= quantity;
                    }
                }

                doSerialization();
            }
            else
            {
                MessageBox.Show("Pogrešan unos količine!");
            }
        }

        public void doSerialization()
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, supplies);
            }
        }

       public static ObservableCollection<MedicalSupply> supplies;
       public String fileName;

    }
}

