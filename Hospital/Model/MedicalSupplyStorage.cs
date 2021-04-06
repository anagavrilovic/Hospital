// File:    MedicalSupplyStorage.cs
// Author:  Marija
// Created: Tuesday, March 23, 2021 10:19:32 PM
// Purpose: Definition of Class MedicalSupplyStorage

using System;
using System.Collections.Generic;

namespace Hospital
{
    public class MedicalSupplyStorage
    {
       public List<MedicalSupply> GetAll()
       {
          throw new NotImplementedException();
       }
   
       public void Save(MedicalSupply parameter1)
       {
          throw new NotImplementedException();
       }
   
       public Boolean Delete(int id)
       {
          throw new NotImplementedException();
       }
   
       public MedicalSupply GetOne(int id)
       {
          throw new NotImplementedException();
       }
   
       public List<MedicalSupply> GetByRoomID(string id)
       {
          throw new NotImplementedException();
       }
   
       public bool UpdateSupply(string firstRoomID, string secondRoomID, int quantity)
       {
          throw new NotImplementedException();
       }
   
       public String fileName;

    }
}

