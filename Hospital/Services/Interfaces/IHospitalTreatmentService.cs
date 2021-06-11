using Hospital.Model;
using System.Collections.Generic;

namespace Hospital.Services.Interfaces
{
    public interface IHospitalTreatmentService
    {
        bool AlreadyHospitalized(string idPatient);
        void checkHospitalTreatmentDates();
        void Delete(string id);
        List<HospitalTreatment> GetAll();
        List<HospitalTreatment> SetTreatments();
        void Update(HospitalTreatment hospitalTreatment);
    }
}