using Newtonsoft.Json;
using System;

namespace Hospital.Model
{
    public class TransferInventory
    {
        public string TransferID { get; set; }
        public string ItemID { get; set; }    
        public int Quantity { get; set; }
        public string FirstRoomID { get; set; }
        public string DestinationRoomID { get; set; }
        public DateTime TransferDate { get; set; }

        public string TransferTime { get; set; }

        [JsonIgnore]
        public Room FirstRoom { get; set; }
        [JsonIgnore]
        public Room DestinationRoom { get; set; }

        public TransferInventory() {}

        public TransferInventory(string itemId, int quantity, string firstRoomID, string secondRoomID, DateTime date)
        {
            ItemID = itemId;
            Quantity = quantity;
            FirstRoomID = firstRoomID;
            DestinationRoomID = secondRoomID;
            TransferDate = date;
        }
    }
}
