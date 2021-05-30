namespace Hospital.DTO
{
    public class FilteringRoomsDTO
    {
        public int Floor { get; set; }
        public RoomType Type { get; set; }
        public string StaticInventoryId { get; set; }
        public string DynamicInventoryId { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }

        public FilteringRoomsDTO() { }
    }
}
