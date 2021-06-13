using Hospital.Commands.Patient;
using Hospital.Factory;
using Hospital.Model;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Navigation;

namespace Hospital.ViewModels.Patient
{
    public class PatientSettingsPageViewModel
    {
        private NavigationService navService;
        private PatientSettingsService patientSettingsService = new PatientSettingsService(new PatientSettingsFileFactory());
        public ObservableCollection<String> DoctorsNamesSurnames { get; set; }
        public String SelectedDoctor {get;set;}
        private DoctorService doctorService=new DoctorService();

        public Boolean ShowWizard { get; set; }
        public Boolean ShowTooltips { get; set; }

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
            patientSettings.ShowWizard = ShowWizard;
            patientSettings.ShowTooltips = ShowTooltips;
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
            ShowWizard = patientSettingsService.GetByID(MainWindow.IDnumber).ShowWizard;
            ShowTooltips = patientSettingsService.GetByID(MainWindow.IDnumber).ShowTooltips;
            SaveSettingsCommand = new RelayCommand(Executed_SaveSettingsCommand, CanExecute_SaveSettingsCommand);
        }

    }
}
