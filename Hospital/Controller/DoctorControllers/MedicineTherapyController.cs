using Hospital.DTO.DoctorDTO;
using Hospital.Services;
using Hospital.View.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Controller.DoctorControllers
{
    public class MedicineTherapyController
    {
        private MedicineService medicineService;
        private MedicineTherapyDTO DTO;
        public MedicineTherapyController(MedicineTherapyDTO DTO)
        {
            this.DTO = DTO;
            medicineService = new MedicineService();
        }

        public List<Medicine> GetAllMedics()
        {
            return medicineService.GetAll();
        }

        public bool AlergicToIngredients(Medicine medicToBeAdded,MedicalRecord medicalRecord)
        {
           return medicineService.AlergicToIngredients(medicToBeAdded, medicalRecord);
        }

        public bool AllergicToMedic(Medicine medicToBeAdded, MedicalRecord medicalRecord)
        {
            if (medicineService.AllergicToMedic(medicToBeAdded, medicalRecord))
            {
                ErrorBox errorBox = new ErrorBox("Pacijent je alergican na dati lek");
                return true;
            }
            return false;
        }
    }
}
