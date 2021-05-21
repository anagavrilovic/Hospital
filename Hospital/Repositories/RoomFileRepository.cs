using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public class RoomFileRepository : IRoomRepository
    {
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Room> GetAll()
        {
            throw new NotImplementedException();
        }

        public Room GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public void Save(Room parameter)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ObservableCollection<Room> parameter)
        {
            throw new NotImplementedException();
        }
    }
}
