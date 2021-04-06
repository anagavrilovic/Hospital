// File:    InventoryStorage.cs
// Author:  Marija
// Created: Tuesday, March 23, 2021 10:19:32 PM
// Purpose: Definition of Class InventoryStorage

using System;
using System.Collections.Generic;

namespace Hospital
{
    public class InventoryStorage
    {
       public List<Inventory> GetAll()
       {
          throw new NotImplementedException();
       }
   
       public void Save(Inventory parameter1)
       {
          throw new NotImplementedException();
       }
   
       public Boolean Delete(int id)
       {
          throw new NotImplementedException();
       }
   
       public Inventory GetOne(int id)
       {
          throw new NotImplementedException();
       }
   
       public List<Inventory> GetByRoomID(string id)
       {
          throw new NotImplementedException();
       }
   
       public bool UpdateInventory(string firstRoomID, string secondRoomID, int quantity)
       {
          throw new NotImplementedException();
       }
   
       public String fileName;

    }
}

