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
                //JsonSerializer serializer = new JsonSerializer();
                // rooms = (ObservableCollection<Room>)serializer.Deserialize(file, typeof(ObservableCollection<Room>));
                rooms = JsonConvert.DeserializeObject<ObservableCollection<Room>>(sr.ReadToEnd());
            }

            return rooms;
      }
      
      public void Save(Room parameter1)
      {

          //  rooms = GetAll();
            rooms.Add(parameter1);

            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, rooms);
            }
          
        }
      
      public Boolean Delete(int id)
      {
          //  rooms = GetAll();

            foreach (Room r in rooms)
            {
                if (r.Id == id)
                {
                    rooms.Remove(r);
                    using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, rooms);
                    }
                    return true;
                }
            }
            return false;
        }
      
      public Room GetOne(int id)
      {
         foreach(Room r in rooms)
            {
                if(r.Id == id)
                {
                    return r;
                }
            }
            return null;
      }
      
        public static ObservableCollection<Room> rooms = new ObservableCollection<Room>();
        public String fileName;
   
   }
}