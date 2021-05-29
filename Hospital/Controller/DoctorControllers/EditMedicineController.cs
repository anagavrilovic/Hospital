using Hospital.DTO.DoctorDTO;
using Hospital.Services;
using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Controller.DoctorControllers
{
    public class EditMedicineController : ViewModel
    {
        private EditMedicineDTO editMedicineDTO;
        private MedicineService medicineService;
        public EditMedicineController(EditMedicineDTO editMedicineDTO)
        {
            this.editMedicineDTO = editMedicineDTO;
            medicineService = new MedicineService();
        }

        public bool MedicineContainsIngredient()
        {
            return medicineService.ContainsIngredient(editMedicineDTO.Medicine, editMedicineDTO.SelectedIngredient);
        }

        public bool MedicineAlreadyInSubstituteDrugs()
        {
            return medicineService.AlreadyInSubstituteDrugs(editMedicineDTO.SelectedMedicine, editMedicineDTO.SubstituteDrugs.ToList());
        }

        public void SaveMedicineChanges()
        {
            medicineService.SaveMedicineSubstitutes(editMedicineDTO.SubstituteDrugs.ToList(), editMedicineDTO.Medicine);
        }

        public List<Medicine> SetReplacementMedicine()
        {
            return medicineService.SetReplacementMedicine(editMedicineDTO.Medicine);
        }
    }
}
