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
   public class MedicineRevisionServicePage
    {
        private MedicineRevisionService medicineRevisionService;
        public ObservableCollection<MedicineRevision> SetRevisionList(Doctor doctor)
        {
            medicineRevisionService = new MedicineRevisionService();
            ObservableCollection<MedicineRevision> medicineRevisions=new ObservableCollection<MedicineRevision>();
            foreach (MedicineRevision mRevision in medicineRevisionService.GetAll())
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
