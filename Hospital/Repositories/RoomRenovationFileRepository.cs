using Hospital.Model;
using Hospital.Repositories.Interfaces;
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
    public class RoomRenovationFileRepository : IRoomRenovationRepository
    {
        private string fileName = "roomRenovation.json";

        public void Delete(string id)
        {
            ObservableCollection<RoomRenovation> roomRenovations = GetAll();

            foreach (RoomRenovation r in roomRenovations)
            {
                if (r.Id.Equals(id))
                {
                    roomRenovations.Remove(r);
                    break;
                }
            }

            foreach (RoomRenovation r in roomRenovations)
            {
                r.Room.SerializeInfo = false;
                r.WareHouse.SerializeInfo = false;
            }

            Serialize(roomRenovations);
        }

        public ObservableCollection<RoomRenovation> GetAll()
        {
            ObservableCollection<RoomRenovation> roomRenovations = new ObservableCollection<RoomRenovation>();
            using (StreamReader sr = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                roomRenovations = JsonConvert.DeserializeObject<ObservableCollection<RoomRenovation>>(sr.ReadToEnd());

                if (roomRenovations == null)
                    return new ObservableCollection<RoomRenovation>();
            }

            return roomRenovations;
        }

        public RoomRenovation GetByID(string id)
        {
            ObservableCollection<RoomRenovation> roomRenovations = GetAll();
            foreach(RoomRenovation renovation in roomRenovations)
            {
                if (renovation.Id.Equals(id))
                    return renovation;
            }
            return null;
        }

        public void Save(RoomRenovation parameter)
        {
            ObservableCollection<RoomRenovation> roomRenovations = GetAll();
            roomRenovations.Add(parameter);

            foreach (RoomRenovation renovation in roomRenovations)
            {
                renovation.Room.SerializeInfo = false;
                renovation.WareHouse.SerializeInfo = false;
            }

            Serialize(roomRenovations);
        }

        public List<int> GetAllScheduledRenovationsIDs()
        {
            List<int> allIDs = new List<int>();
            ObservableCollection<RoomRenovation> allScheduledRenovations = GetAll();
            foreach (RoomRenovation renovation in allScheduledRenovations)
                allIDs.Add(Int32.Parse(renovation.Id));

            return allIDs;
        }

        public void Serialize(ObservableCollection<RoomRenovation> parameter)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, parameter);
            }
        }
    }
}
