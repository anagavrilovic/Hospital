using Hospital.Repositories.Interfaces;
using Hospital.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public class RoomFileRepository : IRoomRepository
    {
        private string fileName = "rooms.json";

        public RoomFileRepository() { }

        public void Delete(string id)
        {
            List<Room> rooms = GetAll();
            foreach (Room r in rooms)
            {
                if (r.Id.Equals(id))
                {
                    //TransferRoomInventoryToWarehouse(id);
                    rooms.Remove(r);
                    Serialize(rooms);
                    return;
                }
            }
        }

        public List<Room> GetAll()
        {
            List<Room> rooms = new List<Room>();
            using (StreamReader sr = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                rooms = JsonConvert.DeserializeObject<List<Room>>(sr.ReadToEnd());
            }

            if (rooms == null)
                return new List<Room>();

            return rooms;
        }

        public List<Room> GetAllRoomsWithoutMagazines()
        {
            List<Room> allRooms = GetAll();
            List<Room> requestedRooms = new List<Room>();

            foreach (Room room in allRooms)
                if (!room.IsMagazine())
                    requestedRooms.Add(room);

            return requestedRooms;
        }

        public Room GetByID(string id)
        {
            List<Room> rooms = GetAll();
            foreach (Room r in rooms)
                if (r.Id.Equals(id))
                    return r;

            return null;
        }

        public void Save(Room parameter)
        {
            List<Room> rooms = GetAll();
            rooms.Add(parameter);
            Serialize(rooms);
        }

        public void Serialize(List<Room> parameter)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, parameter);
            }
        }
    }
}
