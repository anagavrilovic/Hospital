using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Interfaces
{
   public interface ITransferInventoryRepository : IGenericRepository<TransferInventory>
    {
        List<int> GetAllScheduledTransferIDs();
        void EditTransfer(TransferInventory editedTransfer);
    }
}
