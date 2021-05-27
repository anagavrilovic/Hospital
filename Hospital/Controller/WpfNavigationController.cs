using Hospital.View.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Hospital.Controller
{
    class WpfNavigationController : NavigationController
    {
        private NavigationService navService;

        public WpfNavigationController(NavigationService service)
        {
            navService = service;
        }

        public override void NavigateToDoctorAppointments()
        {
        }

        public override void NavigateToDoctorEditMedics()
        {
            throw new NotImplementedException();
        }

        public override void NavigateToDoctorHomePage()
        {
            throw new NotImplementedException();
        }

        public override void NavigateToDoctorHospitalizedPatients()
        {
            throw new NotImplementedException();
        }

        public override void NavigateToDoctorNotifications(string id,NavigationService nav)
        {
            navService.Navigate(new NotificationsPage(id,nav));
        }

        public override void NavigateToDoctorValidation()
        {
            throw new NotImplementedException();
        }
    }
}
