
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Hospital.View;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Hospital
{
    public class RoomStorage
    {
        public RoomStorage()
        {
            this.fileName = "rooms.json";
        }

        public ObservableCollection<Room> GetAll()
        {
            using (StreamReader sr = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                rooms = JsonConvert.DeserializeObject<ObservableCollection<Room>>(sr.ReadToEnd());
            }

            if (rooms == null)
                return new ObservableCollection<Room>();

            return rooms;
        }

        public void Save(Room parameter1)
        {
            //  rooms = GetAll();

            if (!rooms.Contains(parameter1))
            {
                rooms.Add(parameter1);
            }
            else
            {
                foreach(Room r in rooms)
                {
                    if(r.Id.Equals(parameter1.Id))
                    {
                        r.Name = parameter1.Name;
                        r.Floor = parameter1.Floor;
                        r.Status = parameter1.Status;
                        r.Type = parameter1.Type;
                        r.SerializeInfo = true;
                    }
                }
            }
            doSerialization();
        }

        public Boolean Delete(string id)
        {
            //  rooms = GetAll();

            foreach (Room r in rooms)
            {
                if (r.Id.Equals(id))
                {
                    InventoryStorage storage = new InventoryStorage();
                    InventoryStorage.inventory = storage.GetAll();
                    foreach (Inventory i in InventoryStorage.inventory)
                    {
                        if(i.RoomID.Equals(id))
                            i.RoomID = "M1";
                    }
                    storage.doSerialization();

                    DynamicInventoryStorage msStorage = new DynamicInventoryStorage();
                    DynamicInventoryStorage.DynamicInventory = msStorage.GetAll();
                    foreach(DynamicInventory ms in DynamicInventoryStorage.DynamicInventory)
                    {
                        if (ms.RoomID.Equals(id))
                            ms.RoomID = "M1";
                    }
                    msStorage.DoSerialization();

                    rooms.Remove(r);
                    doSerialization();
                    return true;
                }
            }
            return false;
        }

        public Room GetOne(string id)
        {
            rooms = GetAll();
            foreach (Room r in rooms)
            {
                if (r.Id.Equals(id))
                {
                    return r;
                }
            }
            return null;
        }

        public void doSerialization()
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, rooms);
            }

            RoomsWindow.Rooms = rooms;
        }

        public static ObservableCollection<Room> rooms = new ObservableCollection<Room>();
        public String fileName;

    }
}