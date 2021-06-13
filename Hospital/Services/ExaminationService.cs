using Hospital.Factory;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    class ExaminationService
    {
        private IMedicalRecordRepository medicalRecordRepository;

        public ExaminationService(IMedicalRecordRepositoryFactory factory)
        {
            medicalRecordRepository = factory.CreateMedicalRecordRepository();
        }
        public Examination GetExaminationByID(String id)
        {
            MedicalRecord medicalRecord = medicalRecordRepository.GetByPatientID(MainWindow.IDnumber);
            foreach (Examination examination in medicalRecord.Examination)
            {
                if (examination.appointment.IDAppointment.Equals(id)) return examination;
            }
            return null;
        }

        public void PatientDelete(String id)
        {
            Examination examination = GetExaminationByID(id);
            MedicalRecord medicalRecord = DeleteExaminationFromMedicalRecord(id);
            examination.patientVisible = false;
            medicalRecord.Examination.Add(examination);
            medicalRecordRepository.Delete(medicalRecord.MedicalRecordID);
            medicalRecordRepository.Save(medicalRecord);
            
        }

        private MedicalRecord DeleteExaminationFromMedicalRecord(String id)
        {
            MedicalRecord medicalRecord = medicalRecordRepository.GetByPatientID(MainWindow.IDnumber);
            foreach (Examination examination in medicalRecord.Examination)
            {
                if (examination.appointment.IDAppointment.Equals(id))
                {
                    medicalRecord.Examination.Remove(examination);
                    break;
                }
            }
            return medicalRecord;
        }
    }
}
