using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Interfaces
{
    interface IRoomRepository : IGenericRepository<Room>
    {
        List<Room> GetAllRoomsWithoutMagazines();
        void EditRoom(Room editedRoom);
    }
}
