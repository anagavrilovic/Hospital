// File:    MedicalSupplyStorage.cs
// Author:  Marija
// Created: Tuesday, March 23, 2021 10:19:32 PM
// Purpose: Definition of Class MedicalSupplyStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

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
   
       public Boolean Delete(string id)
       {
            supplies = GetAll();

            foreach(MedicalSupply ms in supplies)
            {
                if(ms.Id.Equals(id))
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
          throw new NotImplementedException();
       }

        private void doSerialization()
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, supplies);
            }
        }

       private static ObservableCollection<MedicalSupply> supplies;
       public String fileName;

    }
}

