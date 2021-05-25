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
    public class PatientSettingsService
    {
        IPatientSettingsRepository patientSettingsRepository;

        public PatientSettingsService()
        {
            patientSettingsRepository = new PatientSettingsFileRepository();
        }

        

        public List<PatientSettings> GetAll()
        {
            return patientSettingsRepository.GetAll();
        }

        public PatientSettings GetByID(string id)
        {
            return patientSettingsRepository.GetByID(id);
        }

        public void Save(PatientSettings parameter)
        {
            patientSettingsRepository.Save(parameter);
        }

        public void Update(PatientSettings patientSettings)
        {
            patientSettingsRepository.Update(patientSettings);
        }

        public void FirstSave(String id)
        {
            patientSettingsRepository.FirstSave(id);
        }

        
    }
}
