using Hospital.DTO.DoctorDTO;
using Hospital.Factory;
using Hospital.Model;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Controller.DoctorControllers
{
    public class MedicineValidityController
    {
        private MedicineValidityDTO DTO;

        private MedicineService medicineService;
        private MedicineRevisionService medicineRevisionService;
        public MedicineValidityController(MedicineValidityDTO DTO)
        {
            this.DTO = DTO;
            medicineService = new MedicineService(new MedicineFileFactory(), new MedicalRecordFileFactory());
            medicineRevisionService = new MedicineRevisionService();
        }

        public List<MedicineRevision> SetRevisionList(Doctor doctor)
        {
            return medicineRevisionService.SetRevisionList(doctor);
        }

        public void DeleteMedicineRevision()
        {
            medicineRevisionService.Delete((DTO.SelectedRevision).Medicine.ID);
        }

        public void SaveMedicine()
        {
            medicineService.SaveMedicine((DTO.SelectedRevision).Medicine);
        }
    }
}
