using Hospital.Factory;
using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    class PatientNotesService
    {
        IPatientNotesRepository patientNotesRepository;

        public PatientNotesService()
        {
            patientNotesRepository = new PatientNotesFileRepository();
        }

        public void Delete(string id)
        {
            patientNotesRepository.Delete(id);
        }

        public List<PatientNote> GetAll()
        {
            return patientNotesRepository.GetAll();
        }

        public PatientNote GetByID(string id)
        {
            return patientNotesRepository.GetByID(id);
        }

        public void Save(PatientNote patientNote)
        {
            patientNotesRepository.Save(patientNote);
        }

        public String GetNewID()
        {
            return patientNotesRepository.GetNewID();
        }

        public List<PatientNote> GetByPatientID()
        {
            return patientNotesRepository.GetByPatientID();
        }
    }
}
