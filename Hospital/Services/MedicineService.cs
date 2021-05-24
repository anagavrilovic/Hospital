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

        public ObservableCollection<string> GetAllMedicines()
        {
            return medicineRepository.GetAllMedicines();
        }

        public ObservableCollection<string> GetAllIngredients()
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

        public ObservableCollection<Medicine> GetAll()
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

        private bool IsMedicineIDUnique(string id)
        {
            ObservableCollection<Medicine> medicines = GetAll();
            foreach (Medicine medicine in medicines)
            {
                if (medicine.ID.Equals(id))
                {
                    MessageBox.Show("Vec postoji lek sa unetom oznakom!");
                    return false;
                }
            }
            return true;
        }
    }
}
