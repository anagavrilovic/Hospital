using Hospital.Commands.Patient;
using Hospital.Model;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Hospital.ViewModels.Patient
{
    public class PatientSettingsPageViewModel
    {
        private NavigationService navService;
        private PatientSettingsService patientSettingsService = new PatientSettingsService();
        public ObservableCollection<String> DoctorsNamesSurnames { get; set; }
        public String SelectedDoctor {get;set;}
        private DoctorService doctorService=new DoctorService();

        private RelayCommand saveSettingsCommand;
        public RelayCommand SaveSettingsCommand
        {
            get { return saveSettingsCommand; }
            set
            {
                saveSettingsCommand = value;
            }
        }
        public void Executed_SaveSettingsCommand(object obj)
        {
            PatientSettings patientSettings = new PatientSettings(SelectedDoctor, MainWindow.IDnumber);
            patientSettingsService.Update(patientSettings);
            this.navService.Navigate(new Uri("View/Patient/PatientMenu.xaml", UriKind.Relative));
        }

        public bool CanExecute_SaveSettingsCommand(object obj)
        {
            return true;
        }

        public PatientSettingsPageViewModel(NavigationService service)
        {
            this.navService = service;
            DoctorsNamesSurnames = new ObservableCollection<string>();
           foreach(Model.Doctor doctor in doctorService.GetDoctorsBySpecialty(DoctorSpecialty.general))
            {
                DoctorsNamesSurnames.Add(doctor.ToString());
            }
            DoctorsNamesSurnames.Add("Nije mi bitno");
            SelectedDoctor = patientSettingsService.GetByID(MainWindow.IDnumber).ChosenDoctor;
            SaveSettingsCommand = new RelayCommand(Executed_SaveSettingsCommand, CanExecute_SaveSettingsCommand);
        }
    }
}
