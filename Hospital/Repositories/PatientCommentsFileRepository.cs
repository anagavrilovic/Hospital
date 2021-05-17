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
    public class PatientCommentsFileRepository : IFileRepository<PatientComment>
    {
        private string fileName = "patientComments.json";

        public PatientCommentsFileRepository() { }

        public void Delete(string patientCommentsID)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<PatientComment> GetAll()
        {
            ObservableCollection<PatientComment> patientComments;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                patientComments = (ObservableCollection<PatientComment>)serializer.Deserialize(file, typeof(ObservableCollection<PatientComment>));
            }

            if (patientComments == null)
                patientComments = new ObservableCollection<PatientComment>();

            return patientComments;
        }

        public PatientComment GetByID(string patientCommentsID)
        {
            throw new NotImplementedException();
        }

        public void Save(PatientComment patientComment)
        {
            ObservableCollection<PatientComment> patientComments = GetAll();
            patientComments.Add(patientComment);
            Serialize(patientComments);
        }

        public void Serialize(ObservableCollection<PatientComment> patientComments)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, patientComments);
            }
        }
    }
}
