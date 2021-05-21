using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    class StaticInventoryFileRepository : IStaticInventoryRepository
    {
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Inventory> GetAll()
        {
            throw new NotImplementedException();
        }

        public Inventory GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public void Save(Inventory parameter)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ObservableCollection<Inventory> parameter)
        {
            throw new NotImplementedException();
        }
    }
}
