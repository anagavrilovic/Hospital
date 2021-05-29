using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Interfaces
{
    interface IStaticInventoryRepository : IGenericRepository<Inventory>
    {
        void EditItem(Inventory editedItem);
        void DeleteItemFromRoom(string id, string roomID);
        List<Inventory> GetAllInventoryFromRoom(string id);
    }
}
