using Hospital.Services.Interfaces;

namespace Hospital.Injection
{
    interface IServiceInjection
    {
        IHospitalTreatmentService GetHospitalTreatmentService();
    }
}
