using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class MedicineRevisionService
    {

        IMedicineRevisionRepository medicineRevisionRepository;
        public MedicineRevisionService()
        {
            medicineRevisionRepository = new MedicineRevisionFileRepository();
        }
        public void Delete(string id)
        {
            medicineRevisionRepository.Delete(id);
        }
        public void Save(MedicineRevision revision)
        {
            medicineRevisionRepository.Save(revision);
        }
        public List<MedicineRevision> GetAll()
        {
            return medicineRevisionRepository.GetAll();
        }
      
        public void EditMedicine(MedicineRevision medicine)
        {
            medicineRevisionRepository.EditMedicine(medicine);
        }

    }
}
