using Newtonsoft.Json;
using System;

namespace Hospital
{
    public class DynamicInventory
    {
        public string Id { get; set; }

        public String Name { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public UnitsType Units { get; set; }

        public string RoomID { get; set; }

        [JsonIgnore]
        public Room Room { get; set; }
    }
}

