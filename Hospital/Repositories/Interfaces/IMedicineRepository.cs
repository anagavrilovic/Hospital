using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Interfaces
{
    interface IMedicineRepository : IGenericRepository<Medicine>
    {
        ObservableCollection<string> GetAllMedicines();
        ObservableCollection<string> GetAllIngredients();
        void EditMedicine(Medicine editedMedicine);
    }
}
