
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
                foreach (Room r in rooms)
                {
                    if (r.Id.Equals(parameter1.Id))
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
                    foreach (Inventory i in storage.GetByRoomID(id))
                            i.RoomID = "M1";

                    storage.DoSerialization();

                    DynamicInventoryStorage msStorage = new DynamicInventoryStorage();
                    foreach(DynamicInventory ms in msStorage.GetByRoomID(id))
                            ms.RoomID = "M1";

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

        public ObservableCollection<Room> GetAvaliableRooms(Appointment appointment)
        {
            ObservableCollection<Room> allRooms = GetAllRoomsWithoutMagazines();
            ObservableCollection<Room> avaliableRooms = new ObservableCollection<Room>();

            foreach (Room room in allRooms)
                if (room.IsRoomAvaliableInSelectedPeriod(appointment) && room.IsSuitableRoom(appointment.Type))
                    avaliableRooms.Add(room);

            return avaliableRooms;
        }

        public ObservableCollection<Room> GetAllRoomsWithoutMagazines()
        {
            ObservableCollection<Room> allRooms = GetAll();
            ObservableCollection<Room> requestedRooms = new ObservableCollection<Room>();

            foreach(Room room in allRooms)
            {
                if (!room.IsMagazine())
                    requestedRooms.Add(room);
            }

            return requestedRooms;
        }

    }
}
   