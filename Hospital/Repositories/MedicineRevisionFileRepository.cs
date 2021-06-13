using Hospital.Model;
using Newtonsoft.Json;
using Hospital.Repositories.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hospital.Repositories
{
    public class MedicineRevisionFileRepository : IMedicineRevisionRepository
    {
        private string fileName = "medicineRevision.json";

        public MedicineRevisionFileRepository() { }

        public void Delete(string medicineID)
        {
            List<MedicineRevision> medicineRevisions = GetAll();
            foreach (MedicineRevision medicineRevision in medicineRevisions)
            {
                if (medicineRevision.Medicine.ID.Equals(medicineID))
                {
                    medicineRevisions.Remove(medicineRevision);
                    Serialize(medicineRevisions);
                    return;
                }
            }
        }

        public List<MedicineRevision> GetAll()
        {
            List<MedicineRevision> medicineRevisions = new List<MedicineRevision>();

            using (StreamReader sr = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                medicineRevisions = JsonConvert.DeserializeObject<List<MedicineRevision>>(sr.ReadToEnd());

                if (medicineRevisions == null)
                    return new List<MedicineRevision>();
            }

            return medicineRevisions;
        }

        public MedicineRevision GetByID(string medicineID)
        {
            List<MedicineRevision> medicineRevisions = GetAll();

            foreach (MedicineRevision medicineRevision in medicineRevisions)
                if (medicineRevision.Medicine.ID.Equals(medicineID))
                    return medicineRevision;

            return null;
        }

        public void Save(MedicineRevision medicineRevision)
        {
            List<MedicineRevision> medicineRevisions = GetAll();
            medicineRevisions.Add(medicineRevision);
            Serialize(medicineRevisions);
        }

        public void EditMedicine(MedicineRevision editedMedicineRevision)
        {
            List<MedicineRevision> medicineRevisions = GetAll();
            foreach (MedicineRevision medicineRevision in medicineRevisions.ToList())
            {
                if (editedMedicineRevision.Medicine.ID.Equals(medicineRevision.Medicine.ID))
                {
                    medicineRevisions.Remove(medicineRevision);
                    medicineRevisions.Add(editedMedicineRevision);
                    Serialize(medicineRevisions);
                    break;
                }
            }
        }

        public void Serialize(List<MedicineRevision> medicineRevisions)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, medicineRevisions);
            }
        }
    }
}
