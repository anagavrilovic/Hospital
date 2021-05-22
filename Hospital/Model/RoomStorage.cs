
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
            ObservableCollection<Room> rooms = new ObservableCollection<Room>();
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
            ObservableCollection<Room> rooms = GetAll();
            rooms.Add(parameter1);

            doSerialization(rooms);
        }

        public void EditRoom (Room editedRoom)
        {
            ObservableCollection<Room> rooms = GetAll();
            foreach(Room room in rooms)
            {
                if(room.Id.Equals(editedRoom.Id))
                {
                    rooms.Remove(room);
                    rooms.Add(editedRoom);
                    doSerialization(rooms);
                    break;
                }
            }
        }

        public Boolean Delete(string id)
        {
            ObservableCollection<Room> rooms = GetAll();

            foreach (Room r in rooms)
            {
                if (r.Id.Equals(id))
                {
                    TransferRoomInventoryToWarehouse(id);

                    rooms.Remove(r);
                    doSerialization(rooms);
                    return true;
                }
            }
            return false;
        }

        private void TransferRoomInventoryToWarehouse (string id)
        {
            InventoryStorage storage = new InventoryStorage();
            ObservableCollection<Inventory> inventory = storage.GetAll();
            foreach (Inventory i in storage.GetByRoomID(id))
            {
                if (i.RoomID.Equals(id))
                    i.RoomID = "M1";
            }
            storage.DoSerialization(inventory);

            DynamicInventoryStorage msStorage = new DynamicInventoryStorage();
            ObservableCollection<DynamicInventory> DynamicInventory = msStorage.GetAll();
            foreach (DynamicInventory ms in DynamicInventory)
            {
                if (ms.RoomID.Equals(id))
                    ms.RoomID = "M1";
            }
            msStorage.DoSerialization(DynamicInventory);
        }

        public Room GetOne(string id)
        {
            ObservableCollection<Room> rooms = GetAll();
            foreach (Room r in rooms)
            {
                if (r.Id.Equals(id))
                {
                    return r;
                }
            }
            return null;
        }

        public void doSerialization(ObservableCollection<Room> rooms)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, rooms);
            }
        }

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
   