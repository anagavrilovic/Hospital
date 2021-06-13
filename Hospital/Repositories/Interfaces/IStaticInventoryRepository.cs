using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Interfaces
{
   public interface IStaticInventoryRepository : IGenericRepository<Inventory>
    {
        void EditItem(Inventory editedItem);
        void DeleteItemFromRoom(string id, string roomID);
        List<Inventory> GetAllInventoryFromRoom(string id);
        Inventory GetOneItemFromRoom(string id, string roomId);
    }
}
