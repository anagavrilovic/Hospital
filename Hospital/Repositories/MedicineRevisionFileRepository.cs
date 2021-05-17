using Hospital.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public class MedicineRevisionFileRepository : IFileRepository<MedicineRevision>
    {
        private string fileName = "medicineRevision.json";

        public MedicineRevisionFileRepository() { }

        public void Delete(string medicineID)
        {
            ObservableCollection<MedicineRevision> medicineRevisions = GetAll();
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

        public ObservableCollection<MedicineRevision> GetAll()
        {
            ObservableCollection<MedicineRevision> medicineRevisions = new ObservableCollection<MedicineRevision>();

            using (StreamReader sr = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                medicineRevisions = JsonConvert.DeserializeObject<ObservableCollection<MedicineRevision>>(sr.ReadToEnd());

                if (medicineRevisions == null)
                    return new ObservableCollection<MedicineRevision>();
            }

            return medicineRevisions;
        }

        public MedicineRevision GetByID(string medicineID)
        {
            ObservableCollection<MedicineRevision> medicineRevisions = GetAll();

            foreach (MedicineRevision medicineRevision in medicineRevisions)
                if (medicineRevision.Medicine.ID.Equals(medicineID))
                    return medicineRevision;

            return null;
        }

        public void Save(MedicineRevision medicineRevision)
        {
            ObservableCollection<MedicineRevision> medicineRevisions = GetAll();
            medicineRevisions.Add(medicineRevision);
            Serialize(medicineRevisions);
        }

        public void Serialize(ObservableCollection<MedicineRevision> medicineRevisions)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, medicineRevisions);
            }
        }
    }
}
