using Newtonsoft.Json;

namespace Hospital.Model
{
    public class TransferDynamicInventory
    {
        public string ItemID { get; set; }

        public int Quantity { get; set; }
        
        public string FirstRoomID { get; set; }

        public string DestinationRoomID { get; set; }

        [JsonIgnore]
        public Room FirstRoom { get; set; }
        [JsonIgnore]
        public Room DestinationRoom { get; set; }

        public TransferDynamicInventory() { }    
    }
}
