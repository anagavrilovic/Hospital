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
        public abstract void NavigateToDoctorHomePage();

        public abstract void NavigateToDoctorNotifications(string id,NavigationService nav);

        public abstract void NavigateToDoctorValidation();

        public abstract void NavigateToDoctorAppointments();

        public abstract void NavigateToDoctorEditMedics();

        public abstract void NavigateToDoctorHospitalizedPatients();

       
    }
}
