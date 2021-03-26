using System;

namespace Hospital
{
    public class Room
    {
        public int id { get; set; }
        public String name { get; set; }
        public int floor { get; set; }
        public Boolean isAvaliable { get; set; }
        public RoomType type { get; set; }

        public Room(int id, String name, int floor, Boolean free, RoomType type)
        {
            this.id = id;
            this.name = name;
            this.floor = floor;
            this.isAvaliable  = free;
            this.type = type;
        }

    }
}