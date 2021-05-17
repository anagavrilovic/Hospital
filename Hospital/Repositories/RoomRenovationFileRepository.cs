using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public class RoomRenovationFileRepository : IFileRepository<RoomRenovation>
    {
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<RoomRenovation> GetAll()
        {
            throw new NotImplementedException();
        }

        public RoomRenovation GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public void Save(RoomRenovation parameter)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ObservableCollection<RoomRenovation> parameter)
        {
            throw new NotImplementedException();
        }
    }
}
