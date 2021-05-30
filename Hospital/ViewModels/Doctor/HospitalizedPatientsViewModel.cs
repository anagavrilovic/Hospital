using Hospital.Commands.DoctorCommands;
using Hospital.DTO.DoctorDTO;
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
using System.Windows.Data;

namespace Hospital.ViewModels.Doctor
{
    class HospitalizedPatientsViewModel : ViewModel
    {
        private HospitalTreatmentService hospitalTreatmentService = new HospitalTreatmentService();
        private HospitalTreatment hospitalTreatment = new HospitalTreatment();
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
            set
            {
                saveCommand = value;
            }
        }

        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get { return editCommand; }
            set
            {
                editCommand = value;
            }
        }
        private Visibility calendarPanel;
        public Visibility CalendarPanel
        {
            get { return calendarPanel; }
            set
            {
                calendarPanel = value;
                OnPropertyChanged("CalendarPanel");
            }
        }
        private Visibility panelVisibility;
        public Visibility PanelVisibility
        {
            get { return panelVisibility; }
            set
            {
                panelVisibility = value;
                OnPropertyChanged("PanelVisibility");
            }
        }
        private bool saveButtonEnable;
        public bool SaveButtonEnable
        {
            get { return saveButtonEnable; }
            set 
            {
                saveButtonEnable = value;
                OnPropertyChanged("SaveButtonEnable");
            }
        }
        private bool editButtonEnable;
        public bool EditButtonEnable
        {
            get { return editButtonEnable; }
            set
            {
                editButtonEnable = value;
                OnPropertyChanged("EditButtonEnable");
            }
        }
        public HospitalTreatment HospitalTreatment
        {
            get
            {
                return hospitalTreatment;
            }
            set
            {
                if (value != hospitalTreatment)
                {
                    hospitalTreatment = value;
                    OnPropertyChanged("HospitalTreatment");
                    DTO.HospitalTreatment = HospitalTreatment;
                    Execute_SelectionChanged();
                }
            }
        }
        private HospitalizedPatientsDTO dTO;
        public HospitalizedPatientsDTO DTO
        {
            get
            {
                return dTO;
            }
            set
            {
                if (value != dTO)
                {
                    dTO = value;
                    OnPropertyChanged("DTO");
                }
            }
        }
        public HospitalizedPatientsViewModel()
        {
            DTO = new HospitalizedPatientsDTO();
            this.SaveCommand = new RelayCommand(Execute_Save, CanExecute_Command);
            this.EditCommand = new RelayCommand(Execute_Edit, CanExecute_Command);
            panelVisibility = Visibility.Collapsed;
            saveButtonEnable = false;
            EditButtonEnable = true;
            CalendarPanel = Visibility.Collapsed;
            DTO.StartDate = DateTime.Today;
            DTO.HospitalTreatments = new ObservableCollection<HospitalTreatment>(hospitalTreatmentService.SetTreatments());
        }

        private void Execute_SelectionChanged()
        {
            if (CalendarPanel.Equals(Visibility.Collapsed))
            {
                PanelVisibility = Visibility.Visible;
                this.OnPropertyChanged("HospitalTreatment");
            }
        }

        private void SaveHospitalizedTreatment()
        {
            hospitalTreatmentService.Update(HospitalTreatment);
        }

        private void Execute_Save(object sender)
        {
            if (HospitalTreatment.EndOfTreatment != null)
            {
                CalendarPanel = Visibility.Collapsed;
                SaveButtonEnable = false;
                EditButtonEnable = true;
                OnPropertyChanged("HospitalTreatment");
                SaveHospitalizedTreatment();
            }
        }
        private void Execute_Edit(object sender)
        {
            CalendarPanel = Visibility.Visible;
            SaveButtonEnable = true;
            EditButtonEnable = false;
        }
        private bool CanExecute_Command(object obj)
        {
            return true;
        }
    }
}
