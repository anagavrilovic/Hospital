using Hospital.Commands.Manager;
using Hospital.Model;
using Hospital.Services;
using Hospital.View;
using System.Collections.ObjectModel;
using System.Windows.Navigation;

namespace Hospital.ViewModels.Manager
{
    public class MedicineRevisionViewModel : ViewModel
    {
        #region Properties

        MedicineRevisionService MedicineRevisionService { get; set; }
        NavigationService NavigationService { get; set; }

        public MedicineRevision SelectedMedicineRevision { get; set; }

        private ObservableCollection<MedicineRevision> medicinesOnRevision;
        public ObservableCollection<MedicineRevision> MedicinesOnRevision
        {
            get => medicinesOnRevision;
            set
            {
                medicinesOnRevision = value;
                OnPropertyChanged("MedicinesOnRevision");
            }
        }
        #endregion

        #region Constructor

        public MedicineRevisionViewModel(NavigationService navigate)
        {
            NavigationService = navigate;
            SelectedMedicineRevision = new MedicineRevision();
            MedicineRevisionService = new MedicineRevisionService();
            MedicinesOnRevision = new ObservableCollection<MedicineRevision>(MedicineRevisionService.GetAll());
            BackCommand = new RelayCommand(ExecuteBackCommand, CanExecuteCommands);
            EditMedicineCommand = new RelayCommand(ExecuteEditMedicineCommand, CanExecuteCommandsCondition);

            SetRevisionStatusTextBlock();
            SetRevisionDoctorTextBlock();
        }
        #endregion

        #region Commands
        public RelayCommand BackCommand { get; set; }
        public RelayCommand EditMedicineCommand { get; set; }
        #endregion

        #region Actions

        public void ExecuteEditMedicineCommand(object obj)
        {
            //SelectedMedicineRevision = MedicineService.GetById(SelectedMedicineRevision.ID);
            if (SelectedMedicineRevision == null)
                return;

            EditMedicineOnRevision editMedicine = new EditMedicineOnRevision(SelectedMedicineRevision);
            NavigationService.Navigate(editMedicine);
        }

        public void ExecuteBackCommand(object obj)
        {
            NavigationService.Navigate(new ManagerMainPage());
        }

        public bool CanExecuteCommands(object obj)
        {
            return true;
        }

        public bool CanExecuteCommandsCondition(object obj)
        {
            if (SelectedMedicineRevision == null)
                return false;

            return true;
        }

        private void SetRevisionDoctorTextBlock()
        {
            DoctorService doctorService = new DoctorService();

            foreach (MedicineRevision mr in MedicinesOnRevision)
            {
                Hospital.Model.Doctor dr = doctorService.GetDoctorById(mr.DoctorID);
                mr.RevisionDoctor = dr;
                mr.RevisionDoctor.FirstName = dr.ToString();
            }
        }

        private void SetRevisionStatusTextBlock()
        {
            foreach (MedicineRevision mr in MedicinesOnRevision)
            {
                if (mr.IsMedicineRevised)
                    mr.RevisionStatus = "Vracen sa revizije";
                else
                    mr.RevisionStatus = "Na reviziji";
            }
        }
        #endregion
    }
}
