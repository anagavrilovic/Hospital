using Hospital.Model;
using Hospital.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    class PatientNotesFileRepository : IPatientNotesRepository
    {
        private string fileName = "patientNotes.json";
        public void Delete(string id)
        {
            List<PatientNote> patientNotes = GetAll();
            foreach (PatientNote note in patientNotes)
            {
                if (note.ID.Equals(id))
                {
                    patientNotes.Remove(note);
                    Serialize(patientNotes);
                    return;
                }
            }
        }

        public List<PatientNote> GetAll()
        {
            List<PatientNote> patientNotes;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                patientNotes = (List<PatientNote>)serializer.Deserialize(file, typeof(List<PatientNote>));
            }

            if (patientNotes == null)
                patientNotes = new List<PatientNote>();

            return patientNotes;
        }

        public PatientNote GetByID(string id)
        {
            List<PatientNote> patientNotes = GetAll();
            foreach (PatientNote note in patientNotes)
                if (note.ID.Equals(id))
                    return note;

            return null;
        }

        public void Save(PatientNote patientNote)
        {
            List<PatientNote> patientNotes = GetAll();
            patientNotes.Add(patientNote);
            Serialize(patientNotes);
        }

        public void Serialize(List<PatientNote> notes)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, notes);
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
            List<PatientNote> patientNotes = GetAll();
            foreach (PatientNote note in patientNotes)
            {
                if (note.ID.Equals(ID)) return true;
            }
            return false;
        }

        public List<PatientNote> GetByPatientID(String patientId)
        {
            List<PatientNote> notes = GetAll();
            List<PatientNote> patientsNotes = new List<PatientNote>();
            foreach (PatientNote note in notes)
            {
                if (note.patientID.Equals(patientId)) patientsNotes.Add(note);
            }
            return patientsNotes;
        }

        public List<PatientNote> GetByPatientID()
        {
            throw new NotImplementedException();
        }
    }
}
