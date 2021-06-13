using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Factory
{
   public class MedicineFileFactory : IMedicineRepositoryFactory
    {
        public IMedicineRepository CreateMedicineRepository()
        {
            return new MedicineFileRepository();
        }
    }
}
