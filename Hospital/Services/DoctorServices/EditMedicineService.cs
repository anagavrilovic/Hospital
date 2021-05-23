using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital.Services.DoctorServices
{
    public class EditMedicineService
    {
        private IMedicineRepository medicineRepository;


        public ObservableCollection<Ingredient> ReadIngredients()
        {
            string[] lines2 = File.ReadAllLines("..\\..\\Files\\ingredients.txt");
            ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>();
            foreach (string line in lines2)
            {
                Ingredient ingredient = new Ingredient();
                ingredient.Name = line;
                ingredients.Add(ingredient);
            }
            return ingredients;
        }
        public ObservableCollection<Medicine> SetReplacementMedicine(Medicine medicine)
        {
            medicineRepository = new MedicineFileRepository();
            ObservableCollection<Medicine> supstituteDrugs = new ObservableCollection<Medicine>();
            foreach (string medicID in medicine.ReplacementMedicineIDs)
            {
                supstituteDrugs.Add(medicineRepository.GetByID(medicID));
            }
            return supstituteDrugs;
        }

        public void SaveMedicineSubstitutes(ObservableCollection<Medicine> substituteDrugs,Medicine changedMedicine)
        {
            medicineRepository = new MedicineFileRepository();
            foreach (Medicine medicine in substituteDrugs)
            {
                changedMedicine.AddMedicineID(medicine.ID);
            }
            medicineRepository.EditMedicine(changedMedicine);
        }

        public bool AlreadyInSubstituteDrugs(Medicine selectedMedic,ObservableCollection<Medicine>substituteDrugs)
        {
            foreach (Medicine m in substituteDrugs)
                if (m.ID.Equals(selectedMedic.ID))
                    return true;
            return false;
        }

        public bool ContainsIngredient(Medicine medicine, Ingredient selectedIngredient)
        {
            foreach (Ingredient i in medicine.Ingredient)
                if (i.Name.Equals(selectedIngredient))
                    return true;
            return false;
        }

        public ObservableCollection<Medicine> GetAllMedicine()
        {
            medicineRepository = new MedicineFileRepository();
            return medicineRepository.GetAll();
        }
    }
}
