using Hospital.Commands.DoctorCommands;
using Hospital.Factory;
using Hospital.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace Hospital.ViewModels.Doctor
{
    public class AddPatientViewModel : ViewModel
    {
        public Action CloseAction { get; set; }
        ObservableCollection<Hospital.Patient> patients = new ObservableCollection<Hospital.Patient>();
        private MedicalRecordService medicalRecordService;
        NewAppointmentViewModel parentViewModel;
        public ObservableCollection<Hospital.Patient> Patients
        {
            get { return patients; }
            set
            {
                patients = value;
                OnPropertyChanged("Patients");
            }

        }
        private string patientTextFilter;
        public string PatientTextFilter
        {
            get { return patientTextFilter; }
            set
            {
                patientTextFilter = value;
                OnPropertyChanged("PatientTextFilter");
            }

        }
        private Hospital.Patient selectedPatient;
        public Hospital.Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                selectedPatient = value;
                OnPropertyChanged("SelectedPatient");
            }

        }
        private RelayCommand doubleClickCommand;
        public RelayCommand DoubleClickCommand
        {
            get { return doubleClickCommand; }
            set
            {
                doubleClickCommand = value;
            }
        }
        private RelayCommand filterCommand;
        public RelayCommand FilterCommand
        {
            get { return filterCommand; }
            set
            {
                filterCommand = value;
            }
        }
        public AddPatientViewModel(NewAppointmentViewModel parentViewModel)
        {
            filterCommand = new RelayCommand(Execute_FilterPatients, CanExecute_Command);
            DoubleClickCommand = new RelayCommand(Execute_MouseDoubleClick, CanExecute_Command);
            this.parentViewModel = parentViewModel;
            medicalRecordService = new MedicalRecordService();
            foreach (MedicalRecord record in medicalRecordService.GetAllRecords())
            {
                Patients.Add(record.Patient);
            }
            SetSorterAndFilter();
        }
        private bool CanExecute_Command(object obj)
        {
            return true;
        }
        private void SetSorterAndFilter()
        {
            ICollectionView view = GetPretraga();
            view.SortDescriptions.Add(new SortDescription("FirstName", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("LastName", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("PersonalID", ListSortDirection.Ascending));
        }

        private void Execute_MouseDoubleClick(object sender)
        {
            parentViewModel.DTO.Appointment.PatientsRecord = medicalRecordService.GetByPatientId((SelectedPatient).PersonalID);
            parentViewModel.PatientLabel = parentViewModel.DTO.Appointment.PatientsRecord.Patient.FirstName + " " + parentViewModel.DTO.Appointment.PatientsRecord.Patient.LastName;
            CloseAction();
        }
        public ICollectionView GetPretraga()
        {
            return CollectionViewSource.GetDefaultView(patients);
        }

        private void Execute_FilterPatients(object sender)
        {
            ICollectionView view = GetPretraga();
            view.Filter = delegate (object item)
            {
                Hospital.Patient p = item as Hospital.Patient;
                string txt = p.FirstName + " " + p.LastName + " " + p.PersonalID;
                return txt.Contains(PatientTextFilter);
            };
        }
    }
}
