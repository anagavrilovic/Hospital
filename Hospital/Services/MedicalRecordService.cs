using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class MedicalRecordService
    {
        private IMedicalRecordRepository medicalRecordRepository;

        private RegistratedUserService registratedUserService = new RegistratedUserService();

        public MedicalRecordService()
        {
            medicalRecordRepository = new MedicalRecordFileRepository();
        }

        public ObservableCollection<MedicalRecord> GetAllRecords()
        {
            return medicalRecordRepository.GetAll();
        }

        public void RegisterNewRecord(MedicalRecord newRecord)
        {
            bool accountCreated = registratedUserService.CreateAccount(newRecord);

            if (accountCreated)
                medicalRecordRepository.Save(newRecord);
        }

        public void DeletePatientsRecordFromSystem(MedicalRecord medicalRecord)
        {
            medicalRecordRepository.Delete(medicalRecord.MedicalRecordID);
            registratedUserService.DeleteAccount(medicalRecord.Patient.Username);
        }

        public void UpdateAllRecords(ObservableCollection<MedicalRecord> medicalRecords)
        {
            medicalRecordRepository.Serialize(medicalRecords);
        }

        public MedicalRecord GetRecordByID(string id)
        {
            return medicalRecordRepository.GetByID(id);
        }

        public void UpdateMedicalRecord(MedicalRecord updatedRecord)
        {
            medicalRecordRepository.UpdatePatientsInformation(updatedRecord);
        }

        public string GenerateID()
        {
            List<int> existingIDs = medicalRecordRepository.GetExistingIDs();
            int newID = 1;
            while (true)
            {
                if (!existingIDs.Contains(newID))
                    break;

                newID += 1;
            }

            return newID.ToString();
        }

        internal MedicalRecord GetRecordByUsername(string username)
        {
            return medicalRecordRepository.GetByUsername(username);
        }
    }
}
