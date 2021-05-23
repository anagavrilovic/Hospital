using Newtonsoft.Json;
using System;
using Hospital.Repositories.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public class MedicineFileRepository : IMedicineRepository
    {
        private string fileName = "medicine.json";

        public MedicineFileRepository() { }

        public void Delete(string medicineID)
        {
            ObservableCollection<Medicine> medicines = GetAll();
            foreach (Medicine r in medicines)
            {
                if (r.ID.Equals(medicineID))
                {
                    medicines.Remove(r);
                    Serialize(medicines);
                    return;
                }
            }
        }

        public ObservableCollection<Medicine> GetAll()
        {
            ObservableCollection<Medicine> medicines;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                medicines = (ObservableCollection<Medicine>)serializer.Deserialize(file, typeof(ObservableCollection<Medicine>));
            }

            if (medicines == null)
                medicines = new ObservableCollection<Medicine>();

            return medicines;
        }

        public Medicine GetByID(string medicineID)
        {
            ObservableCollection<Medicine> medicines = GetAll();
            foreach (Medicine m in medicines)
                if (m.ID.Equals(medicineID))
                    return m;

            return null;
        }

        public void Save(Medicine medicine)
        {
            ObservableCollection<Medicine> medicines = GetAll();
            medicines.Add(medicine);
            Serialize(medicines);
        }

        public void Serialize(ObservableCollection<Medicine> medicines)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, medicines);
            }
        }

        public ObservableCollection<string> GetAllMedicines()
        {
            ObservableCollection<string> allMedicines = new ObservableCollection<string>();

            string[] medicines = File.ReadAllLines("..\\..\\Files\\drugs.txt");
            foreach (string medicine in medicines)
                allMedicines.Add(medicine);

            return allMedicines;
        }

        public ObservableCollection<string> GetAllIngredients()
        {
            ObservableCollection<string> allIngredients = new ObservableCollection<string>();

            string[] lines2 = File.ReadAllLines("..\\..\\Files\\ingredients.txt");
            foreach (string line in lines2)
                allIngredients.Add(line);

            return allIngredients;
        }
        public void EditMedicine(Medicine editedMedicine)
        {
            ObservableCollection<Medicine> medicines = GetAll();
            foreach (Medicine medicine in medicines)
            {
                if (editedMedicine.ID.Equals(medicine.ID))
                {
                    medicines.Remove(medicine);
                    medicines.Add(editedMedicine);
                    Serialize(medicines);
                    break;
                }
            }

        }
    }
}
