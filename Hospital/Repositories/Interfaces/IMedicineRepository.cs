using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Interfaces
{
   public interface IMedicineRepository : IGenericRepository<Medicine>
    {
        List<string> GetAllMedicines();
        List<string> GetAllIngredients();
        void EditMedicine(Medicine editedMedicine);
    }
}
