using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class MedicineRevisionStorage
    {
            private String fileName;

            public MedicineRevisionStorage()
            {
                this.fileName = "medicineRevision.json";
            }

            public ObservableCollection<MedicineRevision> GetAll()
            {
                ObservableCollection<MedicineRevision> medicineRevisions = new ObservableCollection<MedicineRevision>();

                using (StreamReader sr = File.OpenText(@"..\\..\\Files\\" + fileName))
                {
                    medicineRevisions = JsonConvert.DeserializeObject<ObservableCollection<MedicineRevision>>(sr.ReadToEnd());

                    if (medicineRevisions == null)
                    {
                        return new ObservableCollection<MedicineRevision>();
                    }
                }

                return medicineRevisions;
            }

            public void Save(MedicineRevision parameter1)
            {
                ObservableCollection<MedicineRevision> medicineRevisions = GetAll();
                medicineRevisions.Add(parameter1);
                DoSerialization(medicineRevisions);
            }

            public Boolean Delete(string id)
            {
                ObservableCollection<MedicineRevision> medicineRevisions = GetAll();
                foreach (MedicineRevision medicineRevision in medicineRevisions)
                {
                    if (medicineRevision.Medicine.ID.Equals(id))
                    {
                        medicineRevisions.Remove(medicineRevision);
                        DoSerialization(medicineRevisions);
                        return true;
                    }
                }
                return false;
            }

            public MedicineRevision GetOne(string id)
            {
                ObservableCollection<MedicineRevision> medicineRevisions = GetAll();
                foreach (MedicineRevision medicineRevision in medicineRevisions)
                {
                    if (medicineRevision.Medicine.ID.Equals(id))
                    {
                        return medicineRevision;
                    }
                }

                return null;
            }
            public void EditMedicine(MedicineRevision editedMedicineRevision)
            {
                ObservableCollection<MedicineRevision> medicineRevisions = GetAll();
                foreach (MedicineRevision medicineRevision in medicineRevisions.ToList())
                {
                    if (editedMedicineRevision.Medicine.ID.Equals(medicineRevision.Medicine.ID))
                    {
                        medicineRevisions.Remove(medicineRevision);
                        medicineRevisions.Add(editedMedicineRevision);
                        DoSerialization(medicineRevisions);
                        break;
                    }
                }
            }

            public void DoSerialization(ObservableCollection<MedicineRevision> medicineRevisions)
            {
                using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, medicineRevisions);
                }
            }
        }
    }

