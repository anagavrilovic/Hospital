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

        public override void NavigateToDoctorAppointments(string id, NavigationController navigationController)
        {
            navService.Navigate(new DoctorAppointments(id, navigationController));
        }

        public override void NavigateToDoctorEditMedics()
        {
            navService.Navigate(new EditMedicinePage());
        }

        public override void NavigateToDoctorFeedback(string id, NavigationController navigationController)
        {
            navService.Navigate(new DoctorFeedbackPage(id,navigationController));
        }

        public override void NavigateToDoctorHomePage(DoctorMainPage doctorMainPage)
        {
            navService.Navigate(doctorMainPage);
        }

        public override void NavigateToDoctorHospitalizedPatients()
        {
            navService.Navigate(new HospitalizedPatients());
        }

        public override void NavigateToDoctorNotifications(string id, NavigationController navigationController)
        {
            navService.Navigate(new NotificationsPage(id, navigationController));
        }

        public override void NavigateToDoctorValidation(Model.Doctor doctor)
        {
            navService.Navigate(new MedicineValidity(doctor));
        }
    }
}
