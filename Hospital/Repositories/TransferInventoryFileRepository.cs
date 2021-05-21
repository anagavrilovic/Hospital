using Hospital.Model;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public class TransferInventoryFileRepository : ITransferInventoryRepository
    {
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<TransferInventory> GetAll()
        {
            throw new NotImplementedException();
        }

        public TransferInventory GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public void Save(TransferInventory parameter)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ObservableCollection<TransferInventory> parameter)
        {
            throw new NotImplementedException();
        }
    }
}
