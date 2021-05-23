using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services.DoctorServices
{
   public class MedicineRevisionService
    {
        private IMedicineRevisionRepository medicineRevisionRepository;
        private IMedicineRepository medicineRepository;

        public ObservableCollection<MedicineRevision> SetRevisionList(Doctor doctor)
        {
            ObservableCollection<MedicineRevision> medicineRevisions=new ObservableCollection<MedicineRevision>();
            medicineRevisionRepository = new MedicineRevisionFileRepository();
            foreach (MedicineRevision mRevision in medicineRevisionRepository.GetAll())
            {
                if (mRevision.DoctorID.Equals(doctor.PersonalID) && !mRevision.IsMedicineRevised)
                {
                    medicineRevisions.Add(mRevision);
                }
            }
            return medicineRevisions;
        }
        public void DeleteMedicineRevision(string id)
        {
            medicineRevisionRepository = new MedicineRevisionFileRepository();
            medicineRevisionRepository.Delete(id);
        }
        public void SaveNewMedicine(Medicine medicine)
        {
            medicineRepository = new MedicineFileRepository();
            medicineRepository.Save(medicine);
        }

    }
}
