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
            using (StreamReader sr = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                roomRenovation = JsonConvert.DeserializeObject<ObservableCollection<RoomRenovation>>(sr.ReadToEnd());

                if (roomRenovation == null)
                {
                    return new ObservableCollection<RoomRenovation>();
                }
            }

            return roomRenovation;
        }

        public void Save(RoomRenovation parameter1)
        {
            roomRenovation = GetAll();

            if (roomRenovation == null)
            {
                roomRenovation = new ObservableCollection<RoomRenovation>();
            }

            roomRenovation.Add(parameter1);

            doSerialization();
        }

        public void Delete(RoomRenovation renovation)
        {
            roomRenovation = GetAll();
            roomRenovation.Remove(renovation);

            foreach (RoomRenovation r in roomRenovation)
            {
                if (r.StartDate == renovation.StartDate && r.EndDate == renovation.EndDate)
                {
                    roomRenovation.Remove(r);
                    break;
                }
            }

            doSerialization();
        }

        public void doSerialization()
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, roomRenovation);
            }
        }

        public static ObservableCollection<RoomRenovation> roomRenovation = new ObservableCollection<RoomRenovation>();
        public String fileName;
    }
}
