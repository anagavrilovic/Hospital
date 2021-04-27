
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
            rooms.Add(parameter1);
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

                    MedicalSupplyStorage msStorage = new MedicalSupplyStorage();
                    MedicalSupplyStorage.supplies = msStorage.GetAll();
                    foreach(MedicalSupply ms in MedicalSupplyStorage.supplies)
                    {
                        if (ms.RoomID.Equals(id))
                            ms.RoomID = "M1";
                    }
                    msStorage.doSerialization();

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
        }

        public static ObservableCollection<Room> rooms = new ObservableCollection<Room>();
        public String fileName;

    }
}