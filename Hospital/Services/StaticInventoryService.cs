using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital.Services
{
    public class StaticInventoryService
    {
        private IStaticInventoryRepository staticInventoryRepository;
        public StaticInventoryService()
        {
            staticInventoryRepository = new StaticInventoryFileRepository();
        }

        public List<Inventory> GetAllInventoryFroomRoom(string roomID)
        {
            return staticInventoryRepository.GetAllInventoryFromRoom(roomID);
        }

        public bool AddNewItem(Inventory InventoryItem)
        {
            if (!IsNewItemIdUnique(InventoryItem))
                return false;

           staticInventoryRepository.Save(InventoryItem);
            return true;
        }

        public void EditItem(Inventory InventoryItem)
        {
            staticInventoryRepository.EditItem(InventoryItem);
        }

        public void DeleteItem(Inventory InventoryItem)
        {
            staticInventoryRepository.DeleteItemFromRoom(InventoryItem.Id, InventoryItem.RoomID);
        }

        private bool IsNewItemIdUnique(Inventory InventoryItem)
        {
            foreach (Inventory inv in staticInventoryRepository.GetAllInventoryFromRoom(InventoryItem.RoomID))
                if (inv.Id.Equals(InventoryItem.Id))
                    return false;
            
            return true;
        }   
    }
}
