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
    public class HospitalTreatmentService
    {
        IHospitalTreatmentRepository hospitalTreatmentRepository;
        IRoomRepository roomRepository;
        IMedicalRecordRepository medicalRecordRepository;
        public HospitalTreatmentService()
        {
            medicalRecordRepository = new MedicalRecordFileRepository();
            hospitalTreatmentRepository = new HospitalTreatmentFileRepository();
            roomRepository = new RoomFileRepository();
        }
        public void Delete(string id)
        {
           hospitalTreatmentRepository.Delete(id);
        }
        public List<HospitalTreatment> GetAll()
        {
            return hospitalTreatmentRepository.GetAll();
        }
        public void Update(HospitalTreatment hospitalTreatment)
        {
            hospitalTreatmentRepository.EditHospitalTreatment(hospitalTreatment);
        }

        public bool AlreadyHospitalized(string idPatient)
        {
            foreach (HospitalTreatment hospitalTreatment in GetAll())
            {
                if (hospitalTreatment.PatientId.Equals(idPatient))
                    return true;
            }
            return false;
        }

        public void checkHospitalTreatmentDates()
        {
            foreach (HospitalTreatment hospitalTreatment in GetAll())
            {
                if (hospitalTreatment.EndOfTreatment.Date < DateTime.Today)
                    Delete(hospitalTreatment.PatientId);
            }
        }

        public List<HospitalTreatment> SetTreatments()
        {
            roomRepository = new RoomFileRepository();
            medicalRecordRepository = new MedicalRecordFileRepository();
            List<HospitalTreatment> hospitalTreatments = new List<HospitalTreatment>();
            foreach (HospitalTreatment hospitalTreatmentFromStorage in GetAll())
            {
                hospitalTreatmentFromStorage.PatientsRecord = medicalRecordRepository.GetByPatientID(hospitalTreatmentFromStorage.PatientId);
                hospitalTreatmentFromStorage.Room = roomRepository.GetByID(hospitalTreatmentFromStorage.RoomId);
                hospitalTreatments.Add(hospitalTreatmentFromStorage);
            }
            return hospitalTreatments;
        }

        internal void Save(HospitalTreatment hospitalTreatment)
        {
            hospitalTreatmentRepository.Save(hospitalTreatment);
        }
    }
}
