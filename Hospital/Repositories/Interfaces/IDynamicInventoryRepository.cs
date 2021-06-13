using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Interfaces
{
    public interface IDynamicInventoryRepository : IGenericRepository<DynamicInventory>
    {
        void EditItem(DynamicInventory editedItem);
        DynamicInventory GetOneItemFromRoom(string id, string roomId);
        List<DynamicInventory> GetAllInventoryFromRoom(string roomId);
        void DeleteFromRoom(string id, string roomID);
    }
}
