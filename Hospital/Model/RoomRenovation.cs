using System;
using System.Collections.Generic;

namespace Hospital.Model
{
    public class RoomRenovation 
    {
        public string Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public string Description { get; set; }

        public List<Room> RoomsDestroyedDuringRenovation { get; set; }

        public List<Room> RoomsCreatedDuringRenovation { get; set; }

        public Room Room { get; set; }
        public Room WareHouse { get; set; }

        public  RoomRenovation()
        {
            RoomsCreatedDuringRenovation = new List<Room>();
            RoomsDestroyedDuringRenovation = new List<Room>();
            Room = new Room();
            WareHouse = new Room();
        }
    }
}
