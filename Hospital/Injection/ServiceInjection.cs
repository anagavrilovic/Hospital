using Hospital.Services.Interfaces;

namespace Hospital.Injection
{
    public class ServiceInjection : IServiceInjection
    {
        IHospitalTreatmentService hospitalTreatmentService;

        public ServiceInjection(IHospitalTreatmentService hospitalTreatmentService)
        {
            this.hospitalTreatmentService = hospitalTreatmentService;
        }

        public IHospitalTreatmentService GetHospitalTreatmentService()
        {
            return hospitalTreatmentService;
        }
    }
}
