using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class MedicineService
    {
        private IMedicineRepository medicineRepository;

        public MedicineService()
        {
            medicineRepository = new MedicineFileRepository();
        }

        public ObservableCollection<string> GetAllMedicines()
        {
            return medicineRepository.GetAllMedicines();
        }

        public ObservableCollection<string> GetAllIngredients()
        {
            return medicineRepository.GetAllIngredients();
        }

    }
}
