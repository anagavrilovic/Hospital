using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services.DoctorServices
{
    class AppointmentWindowService
    {
        HospitalTreatmentService hospitalTreatmentService;
        public bool AlreadyHospitalized(string idPatient)
        {
            hospitalTreatmentService = new HospitalTreatmentService();
            foreach (HospitalTreatment hospitalTreatment in hospitalTreatmentService.GetAll())
            {
                if (hospitalTreatment.PatientId.Equals(idPatient))
                    return true;
            }
            return false;
        }
    }
}
