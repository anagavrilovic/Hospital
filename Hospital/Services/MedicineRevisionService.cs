using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class MedicineRevisionService
    {
        public MedicineRevisionService()
        {
            _medicineRevisionRepository = new MedicineRevisionFileRepository();
        }

        public void EditMedicine(MedicineRevision medicine)
        {
            _medicineRevisionRepository.EditMedicine(medicine);
        }

        private IMedicineRevisionRepository _medicineRevisionRepository;
    }
}
