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

        public void Delete(string id)
        {
            List<PatientComment> patientComments = GetAll();
            foreach (PatientComment comment in patientComments)
            {
                if (comment.ID.Equals(id))
                {
                    patientComments.Remove(comment);
                    Serialize(patientComments);
                    return;
                }
            }
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

        public PatientComment GetByID(string id)
        {
            List<PatientComment> patientComments = GetAll();
            foreach (PatientComment comment in patientComments)
                if (comment.ID.Equals(id))
                    return comment;

            return null;
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

        public String GetNewID()
        {
            int newID = 0;
            while (true)
            {
                if (!CheckIfIDExists(newID.ToString())) return newID.ToString();
                newID++;
            }
        }

        private Boolean CheckIfIDExists(String ID)
        {
            List<PatientComment> patientComments = GetAll();
            foreach (PatientComment comment in patientComments)
            {
                if (comment.ID.Equals(ID)) return true;
            }
            return false;
        }

        public List<PatientComment> GetByPatientID()
        {
            List<PatientComment> comments = GetAll();
            List<PatientComment> patientComments = new List<PatientComment>();
            foreach (PatientComment comment in comments)
            {
                if (comment.IDPatient.Equals(MainWindow.IDnumber)) patientComments.Add(comment);
            }
            return patientComments;
        }
    }
}
