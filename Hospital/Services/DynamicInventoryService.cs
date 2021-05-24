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
        public DynamicInventoryService(DynamicInventory item)
        {
            _dynamicInventoryStorage = new DynamicInventoryStorage();
            InventoryItem = item;
        }

        private bool IsItemIdUnique()
        {
            foreach (DynamicInventory inv in _dynamicInventoryStorage.GetByRoomID(InventoryItem.RoomID))
            {
                if (inv.Id.Equals(InventoryItem.Id))
                {
                    MessageBox.Show("Već postoji stavka sa unetom oznakom!");
                    return false;
                }
            }
            return true;
        }

        public void AddNewItem()
        {
            if (!IsItemIdUnique())
                return;

            _dynamicInventoryStorage.Save(InventoryItem);
        }

        public void EditItem()
        {
            _dynamicInventoryStorage.EditItem(InventoryItem);
        }

        public DynamicInventory InventoryItem { get; set; }
        private DynamicInventoryStorage _dynamicInventoryStorage;
    }
}
