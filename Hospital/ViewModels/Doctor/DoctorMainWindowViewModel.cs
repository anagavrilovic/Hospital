using Hospital.Commands.DoctorCommands;
using Hospital.Controller;
using Hospital.Controller.DoctorControllers;
using Hospital.Services;
using Hospital.View.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Hospital.ViewModels.Doctor
{
    public class DoctorMainWindowViewModel : ViewModel
    {
        public Action CloseAction { get; set; }
        private DoctorMainWindowController controller;
        public Frame frameMainPage;
        private DoctorMainPage mainPage;
        private string doctorId;
        private NavigationController navigationController;
        private Model.Doctor doctor = new Model.Doctor();
        private RelayCommand logoCommand;

        public RelayCommand LogoCommand
        {
            get { return logoCommand; }
            set
            {
                logoCommand = value;
            }
        }
        private RelayCommand signOutCommand;

        public RelayCommand SignOutCommand
        {
            get { return signOutCommand; }
            set
            {
                signOutCommand = value;
            }
        }
        private RelayCommand announcmentsCommand;

        public RelayCommand AnnouncmentsCommand
        {
            get { return announcmentsCommand; }
            set
            {
                announcmentsCommand = value;
            }
        }
        private RelayCommand revisionCommand;

        public RelayCommand RevisionCommand
        {
            get { return revisionCommand; }
            set
            {
                revisionCommand = value;
            }
        }
        public DoctorMainWindowViewModel(string doctorId,NavigationController navController)
        {
            controller = new DoctorMainWindowController();
            RevisionCommand = new RelayCommand(Execute_Revision, canExecuteMethod);
            AnnouncmentsCommand = new RelayCommand(Execute_Announcment, canExecuteMethod);
            SignOutCommand = new RelayCommand(Execute_SignOut, canExecuteMethod);
            LogoCommand = new RelayCommand(Execute_Logo, canExecuteMethod);
            InitProperties(doctorId, navController);
            controller.CheckHospitalTreatmentDates();
        }

        private void InitProperties(string doctorId, NavigationController navController)
        {
            this.navigationController = navController;
            this.doctorId = doctorId;
            doctor = controller.GetDoctorById(doctorId);
            frameMainPage = new Frame();
            mainPage = new DoctorMainPage(doctorId,navigationController);
            navigationController.NavigateToDoctorHomePage(mainPage);
        }

        private bool canExecuteMethod(object parameter)
        {
            return true;
        }

        private void Execute_Logo(object sender)
        {
            navigationController.NavigateToDoctorHomePage(mainPage);
        }

        private void Execute_SignOut(object sender)
        {
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            CloseAction();
        }

        private void Execute_Announcment(object sender)
        {
            navigationController.NavigateToDoctorNotifications(doctorId,navigationController);
        }

        private void Execute_Revision(object sender)
        {
            navigationController.NavigateToDoctorValidation(doctor);
        }


    }
}
