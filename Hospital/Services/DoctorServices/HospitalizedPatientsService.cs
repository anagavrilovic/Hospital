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
        HospitalTreatmentService hospitalTreatmentService;
        MedicalRecordService medicalRecordService;
        RoomService roomService;
        public ObservableCollection<HospitalTreatment> SetTreatments()
        {
            roomService = new RoomService();
            medicalRecordService=new MedicalRecordService();
            hospitalTreatmentService = new HospitalTreatmentService();
            ObservableCollection<HospitalTreatment> hospitalTreatments = new ObservableCollection<HospitalTreatment>();
            foreach (HospitalTreatment hospitalTreatmentFromStorage in hospitalTreatmentService.GetAll())
            {
                hospitalTreatmentFromStorage.PatientsRecord = medicalRecordService.GetByPatientId(hospitalTreatmentFromStorage.PatientId);
                hospitalTreatmentFromStorage.Room = roomService.GetById(hospitalTreatmentFromStorage.RoomId);
                hospitalTreatments.Add(hospitalTreatmentFromStorage);
            }
            return hospitalTreatments;
        }
    }
}
