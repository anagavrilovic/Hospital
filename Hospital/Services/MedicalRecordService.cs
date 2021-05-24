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
        private IAppointmentRepository appointmentRepository;

        private RegistratedUserService registratedUserService = new RegistratedUserService();

        public MedicalRecordService()
        {
            medicalRecordRepository = new MedicalRecordFileRepository();
        }

        public List<MedicalRecord> GetAllRecords()
        {
            return medicalRecordRepository.GetAll();
        }

        public void RegisterNewRecord(MedicalRecord newRecord)
        {
            if (newRecord.Patient.IsGuest)
                RegisterGuestRecord(newRecord);
            else
                RegisterRegularRecord(newRecord);
        }

        private void RegisterGuestRecord(MedicalRecord newGuestRecord)
        {
            medicalRecordRepository.Save(newGuestRecord);
        }

        private void RegisterRegularRecord(MedicalRecord newRecord)
        {
            bool accountCreated = registratedUserService.CreateAccount(newRecord);

            if (accountCreated)
                medicalRecordRepository.Save(newRecord);
        }

        public void DeletePatientsRecordFromSystem(MedicalRecord medicalRecord)
        {
            medicalRecordRepository.Delete(medicalRecord.MedicalRecordID);
            registratedUserService.DeleteAccount(medicalRecord.Patient.Username);
            appointmentRepository.DeletePatientsAppointments(medicalRecord.Patient.PersonalID);
        }

        public void UpdateAllRecords(List<MedicalRecord> medicalRecords)
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

        public MedicalRecord GetRecordByUsername(string username)
        {
            return medicalRecordRepository.GetByUsername(username);
        }

        public MedicalRecord GetByPatientId(string id)
        {
            return medicalRecordRepository.GetByPatientID(id);
        }
    }
}
