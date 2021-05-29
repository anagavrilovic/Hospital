using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Controller.DoctorControllers
{
    public class DoctorMainWindowController
    {
        private DoctorService doctorService;
        private HospitalTreatmentService hospitalTreatmentService;

        public DoctorMainWindowController()
        {
            doctorService = new DoctorService();
            hospitalTreatmentService = new HospitalTreatmentService();
        }

        CheckHospitalTreatmentDates()
        {
            hospitalTreatmentService.checkHospitalTreatmentDates();
        }
    }
}
