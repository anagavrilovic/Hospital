using Hospital.Model;
using Newtonsoft.Json;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public class PatientCommentsFileRepository : IPatientCommentsRepository
    {
        private string fileName = "patientComments.json";

        public PatientCommentsFileRepository() { }

        public void Delete(string patientCommentsID)
        {
            throw new NotImplementedException();
        }

        public List<PatientComment> GetAll()
        {
            List<PatientComment> patientComments;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                patientComments = (List<PatientComment>)serializer.Deserialize(file, typeof(List<PatientComment>));
            }

            if (patientComments == null)
                patientComments = new List<PatientComment>();

            return patientComments;
        }

        public PatientComment GetByID(string patientCommentsID)
        {
            throw new NotImplementedException();
        }

        public void Save(PatientComment patientComment)
        {
            List<PatientComment> patientComments = GetAll();
            patientComments.Add(patientComment);
            Serialize(patientComments);
        }

        public void Serialize(List<PatientComment> patientComments)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, patientComments);
            }
        }
    }
}
