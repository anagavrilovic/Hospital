using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Hospital.Services
{
    public class RoomsFilteringService
    {
        public const int NOT_SELECTED_FLOOR = -1;
        public const int NOT_SELECTED_ROOM_TYPE = -1;
        public const string NOT_SELECTED_STATIC_INVENTORY = "NOT SELECTED";
        public const string NOT_SELECTED_DYNAMIC_INVENTORY = "NOT SELECTED";
        public const int NOT_SELECTED_MIN_QUANTITY = -1;
        public const int NOT_SELECTED_MAX_QUANTITY = -1;

        private IRoomRepository _roomRepository;
        private IStaticInventoryRepository _staticInventoryRepository;
        private IDynamicInventoryRepository _dynamicInventoryRepository;

        public List<Room> FilteredRooms { get; set; }
        public DTO.FilteringRoomsDTO FilteringCriteriaDTO { get; set; }

        public RoomsFilteringService(DTO.FilteringRoomsDTO criteria, IRoomRepository roomRepository, IStaticInventoryRepository staticInventoryRepository, IDynamicInventoryRepository dynamicInventoryRepository)
        {
            _roomRepository = roomRepository;
            _staticInventoryRepository = staticInventoryRepository;
            _dynamicInventoryRepository = dynamicInventoryRepository;
            FilteredRooms = _roomRepository.GetAll();
            FilteringCriteriaDTO = criteria;
        }

        public List<Room> FilterRooms()
        {
            FilterByFloor();
            FilterByRoomType();
            FilterByStaticInventory();
            FilterByDynamicInventory();
            FilterByInventoryQuantitiy();

            return FilteredRooms;
        }

        private void FilterByFloor()
        {
            if (FilteringCriteriaDTO.Floor == NOT_SELECTED_FLOOR)
                return;

            foreach (Room room in FilteredRooms.ToList())
                if (room.Floor != FilteringCriteriaDTO.Floor)
                    FilteredRooms.Remove(room);           
        }

        private void FilterByRoomType()
        {
            if (FilteringCriteriaDTO.Type  == (RoomType) NOT_SELECTED_ROOM_TYPE)
                return;

            foreach (Room room in FilteredRooms.ToList())
                if (room.Type != FilteringCriteriaDTO.Type)
                    FilteredRooms.Remove(room);           
        }

        private void FilterByStaticInventory()
        {
            if (FilteringCriteriaDTO.StaticInventoryId.Equals(NOT_SELECTED_STATIC_INVENTORY))
                return;

            foreach (Room room in FilteredRooms.ToList())
            {
                Inventory item = _staticInventoryRepository.GetOneItemFromRoom(FilteringCriteriaDTO.StaticInventoryId, room.Id);
                if(item == null)
                    FilteredRooms.Remove(room);
            }
        }

        private void FilterByDynamicInventory()
        {
            if (FilteringCriteriaDTO.DynamicInventoryId.Equals(NOT_SELECTED_DYNAMIC_INVENTORY))
                return;

            foreach (Room room in FilteredRooms.ToList())
            {
                DynamicInventory item = _dynamicInventoryRepository.GetOneItemFromRoom(FilteringCriteriaDTO.DynamicInventoryId, room.Id);
                if (item == null)
                    FilteredRooms.Remove(room);
            }
        }

        private void FilterStaticInventoryByQuantity()
        {
            foreach (Room room in FilteredRooms.ToList())
            {
                Inventory itemSelected = _staticInventoryRepository.GetOneItemFromRoom(FilteringCriteriaDTO.StaticInventoryId, room.Id);
                bool condition;
                if (FilteringCriteriaDTO.MaxQuantity == 0)
                     condition = itemSelected.Quantity > FilteringCriteriaDTO.MinQuantity;
                else
                    condition = itemSelected.Quantity >= FilteringCriteriaDTO.MinQuantity && itemSelected.Quantity <= FilteringCriteriaDTO.MaxQuantity;

               if (!condition)
                  FilteredRooms.Remove(room);       
            }
        }

        private void FilterDynamicInventoryByQuantity()
        {
            foreach (Room room in FilteredRooms.ToList())
            {
                DynamicInventory itemSelected = _dynamicInventoryRepository.GetOneItemFromRoom(FilteringCriteriaDTO.DynamicInventoryId, room.Id);
                bool condition;
                    if (FilteringCriteriaDTO.MaxQuantity == 0)
                        condition = itemSelected.Quantity > FilteringCriteriaDTO.MinQuantity;
                    else
                        condition = itemSelected.Quantity >= FilteringCriteriaDTO.MinQuantity && itemSelected.Quantity <= FilteringCriteriaDTO.MaxQuantity;

                    if (!condition)
                        FilteredRooms.Remove(room);  
            }
        }

        private void FilterByInventoryQuantitiy()
        {
            if ((FilteringCriteriaDTO.StaticInventoryId.Equals(NOT_SELECTED_STATIC_INVENTORY) && (FilteringCriteriaDTO.DynamicInventoryId.Equals(NOT_SELECTED_DYNAMIC_INVENTORY)) || 
                (FilteringCriteriaDTO.MinQuantity == NOT_SELECTED_MIN_QUANTITY && FilteringCriteriaDTO.MaxQuantity == NOT_SELECTED_MAX_QUANTITY)))
                return;

            if(!FilteringCriteriaDTO.StaticInventoryId.Equals(NOT_SELECTED_STATIC_INVENTORY))
                  FilterStaticInventoryByQuantity();
            else
                FilterDynamicInventoryByQuantity();
        }        
    }
}
