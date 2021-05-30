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

namespace Hospital.Services
{
    public class MedicineService
    {
        private IMedicineRepository medicineRepository;

        public MedicineService()
        {
            medicineRepository = new MedicineFileRepository();
        }

        public List<string> GetAllMedicines()
        {
            return medicineRepository.GetAllMedicines();
        }

        public List<string> GetAllIngredients()
        {
            return medicineRepository.GetAllIngredients();
        }
        public Medicine GetById(string id)
        {
           return medicineRepository.GetByID(id);
        }

        public void UpdateMedicine(Medicine medicine)
        {
            medicineRepository.EditMedicine(medicine);
        }

        public List<Medicine> GetAll()
        {
            return medicineRepository.GetAll();
        }
        public void SaveMedicine(Medicine medicine)
        {
            medicineRepository.Save(medicine);
        }
        public void DeleteMedicine(Medicine medicine)
        {
            medicineRepository.Delete(medicine.ID);
        }

        public void EditMedicine(Medicine medicine)
        {
            medicineRepository.EditMedicine(medicine);
        }

        public bool IsMedicineIDUnique(string id)
        {
            List<Medicine> medicines = GetAll();
            foreach (Medicine medicine in medicines)
                if (medicine.ID.Equals(id))
                    return false;
            
            return true;
        }

        public List<Ingredient> ReadIngredients()
        {
            string[] lines2 = File.ReadAllLines("..\\..\\Files\\ingredients.txt");
            List<Ingredient> ingredients = new List<Ingredient>();
            foreach (string line in lines2)
            {
                Ingredient ingredient = new Ingredient();
                ingredient.Name = line;
                ingredients.Add(ingredient);
            }
            return ingredients;
        }

        public List<Medicine> SetReplacementMedicine(Medicine medicine)
        {
            List<Medicine> supstituteDrugs = new List<Medicine>();
            foreach (string medicID in medicine.ReplacementMedicineIDs)
            {
                supstituteDrugs.Add(GetById(medicID));
            }
            return supstituteDrugs;
        }

        public void SaveMedicineSubstitutes(List<Medicine> substituteDrugs, Medicine changedMedicine)
        {
            foreach (Medicine medicine in substituteDrugs)
            {
                changedMedicine.AddMedicineID(medicine.ID);
            }
            UpdateMedicine(changedMedicine);
        }

        public bool AlreadyInSubstituteDrugs(Medicine selectedMedic, List<Medicine> substituteDrugs)
        {
            foreach (Medicine m in substituteDrugs)
                if (m.ID.Equals(selectedMedic.ID))
                    return true;
            return false;
        }

        public bool ContainsIngredient(Medicine medicine, Ingredient selectedIngredient)
        {
            foreach (Ingredient i in medicine.Ingredient)
                if (i.Name.Equals(selectedIngredient.Name))
                    return true;
            return false;
        }
    }
}
