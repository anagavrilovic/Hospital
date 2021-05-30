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
    public class DynamicInventoryService
    {
        private IDynamicInventoryRepository dynamicInventoryRepository;
        public DynamicInventoryService()
        {
            dynamicInventoryRepository = new DynamicInventoryFileRepository();
        }

        public List<DynamicInventory> GetAll()
        {
            return dynamicInventoryRepository.GetAll();
        }

        public List<DynamicInventory> GetAllDynamicInventoryFroomRoom(string roomID)
        {
            return dynamicInventoryRepository.GetAllInventoryFromRoom(roomID);
        }

        public void DeleteItemFromRoom(string itemId, string roomId)
        {
            dynamicInventoryRepository.DeleteFromRoom(itemId, roomId);
        }

        private bool IsItemIdUnique(DynamicInventory InventoryItem)
        {
            foreach (DynamicInventory inv in dynamicInventoryRepository.GetAllInventoryFromRoom(InventoryItem.RoomID))
            {
                if (inv.Id.Equals(InventoryItem.Id))
                {
                    MessageBox.Show("Već postoji stavka sa unetom oznakom!");       //TODO: throw exception
                    return false;
                }
            }
            return true;
        }

        public void AddNewItem(DynamicInventory InventoryItem)
        {
            if (!IsItemIdUnique(InventoryItem))
                return;

            dynamicInventoryRepository.Save(InventoryItem);
        }

        public void EditItem(DynamicInventory InventoryItem)
        {
            dynamicInventoryRepository.EditItem(InventoryItem);
        }
    }
}
