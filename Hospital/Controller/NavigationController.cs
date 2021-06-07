using Hospital.View.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Hospital.Controller
{
    public abstract class NavigationController
    {
        public abstract void NavigateToDoctorHomePage(DoctorMainPage doctorMainPage);

        public abstract void NavigateToDoctorNotifications(string id,NavigationController navigationController);

        public abstract void NavigateToDoctorValidation(Model.Doctor doctor);

        public abstract void NavigateToDoctorAppointments(string id, NavigationController navigationController);

        public abstract void NavigateToDoctorEditMedics();

        public abstract void NavigateToDoctorHospitalizedPatients();

        public abstract void NavigateToDoctorFeedback(string id, NavigationController navigationController);

        public abstract void NavigateToDoctorReport();
    }
}
