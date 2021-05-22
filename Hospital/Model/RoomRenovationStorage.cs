using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    class RoomRenovationStorage
    {
        public RoomRenovationStorage()
        {
            this.fileName = "roomRenovation.json";
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

        public void Save(RoomRenovation parameter1)
        {
            ObservableCollection<RoomRenovation> roomRenovations = GetAll();
            roomRenovations.Add(parameter1);

            foreach(RoomRenovation renovation in roomRenovations)
            {
                renovation.Room.SerializeInfo = false;
                renovation.WareHouse.SerializeInfo = false;
            }

            Serialize(roomRenovations);
        }

        public void Delete(RoomRenovation renovation)
        {
            ObservableCollection<RoomRenovation> roomRenovations = GetAll();
            roomRenovations.Remove(renovation);

            foreach (RoomRenovation r in roomRenovations)
            {
                if (r.StartDate == renovation.StartDate && r.EndDate == renovation.EndDate)
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

        public bool isBeingRenovatedNow(Appointment appointment)
        {
            ObservableCollection<RoomRenovation> roomRenovations = GetAll();

            foreach (RoomRenovation r in roomRenovations)
            {
                if (appointment.DateTime > r.StartDate && appointment.DateTime.AddHours(appointment.DurationInHours) < r.EndDate + new TimeSpan(23,59,59) && appointment.Room.Id.Equals(r.Room.Id))
                {
                    return true;
                }
            }

            return false;
        }
        
        public void Serialize(ObservableCollection<RoomRenovation> roomRenovations)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, roomRenovations);
            }
        }

        public String fileName;
    }
}
