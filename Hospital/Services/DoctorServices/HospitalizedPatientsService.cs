using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services.DoctorServices
{
    public class HospitalizedPatientsService
    {
        IHospitalTreatmentRepository treatmentRepository;
        IMedicalRecordRepository medicalRecordRepository;
        IRoomRepository roomRepository;

        public ObservableCollection<HospitalTreatment> SetTreatments()
        {
            treatmentRepository = new HospitalTreatmentFileRepository();
            medicalRecordRepository = new MedicalRecordFileRepository();
            ObservableCollection<HospitalTreatment> hospitalTreatments = new ObservableCollection<HospitalTreatment>();
            roomRepository = new RoomFileRepository();
            foreach (HospitalTreatment hospitalTreatmentFromStorage in treatmentRepository.GetAll())
            {
                hospitalTreatmentFromStorage.PatientsRecord = medicalRecordRepository.GetByPatientID(hospitalTreatmentFromStorage.PatientId);
                hospitalTreatmentFromStorage.Room = roomRepository.GetByID(hospitalTreatmentFromStorage.RoomId);
                hospitalTreatments.Add(hospitalTreatmentFromStorage);
            }
            return hospitalTreatments;
        }
        public void EditHospitalTreatment(HospitalTreatment hospitalTreatment)
        {
            treatmentRepository = new HospitalTreatmentFileRepository();
            treatmentRepository.EditHospitalTreatment(hospitalTreatment);
        }
    }
}
