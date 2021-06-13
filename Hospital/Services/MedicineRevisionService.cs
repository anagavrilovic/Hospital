using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public List<MedicineRevision> SetRevisionList(Doctor doctor)
        {
            List<MedicineRevision> medicineRevisions = new List<MedicineRevision>();
            foreach (MedicineRevision mRevision in GetAll())
            {
                if (mRevision.DoctorID.Equals(doctor.PersonalID) && !mRevision.IsMedicineRevised)
                {
                    medicineRevisions.Add(mRevision);
                }
            }
            return medicineRevisions;
        }
    }
}
