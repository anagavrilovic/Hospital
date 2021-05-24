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
        public StaticInventoryService(Inventory item)
        {
            _inventoryStorage = new InventoryStorage();
            InventoryItem = item;
        }

        public void AddNewItem()
        {
            if (!IsNewItemIdUnique())
                return;

            _inventoryStorage.Save(InventoryItem);
        }

        public void EditItem()
        {
            _inventoryStorage.EditItem(InventoryItem);
        }

        public void DeleteItem()
        {
            _inventoryStorage.Delete(InventoryItem.Id, InventoryItem.RoomID);
        }

        private bool IsNewItemIdUnique()
        {
            foreach (Inventory inv in _inventoryStorage.GetByRoomID(InventoryItem.RoomID))
            {
                if (inv.Id.Equals(InventoryItem.Id))
                {
                    MessageBox.Show("Već postoji stavka sa unetom oznakom!");
                    return false;
                }
            }
            return true;
        }       
        public Inventory InventoryItem { get; set; }
        private InventoryStorage _inventoryStorage;
    }
}
