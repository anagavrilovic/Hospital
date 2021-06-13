using Hospital.Commands.DoctorCommands;
using Hospital.Factory;
using Hospital.Repositories;
using Hospital.Services;
using Hospital.View;
using System.Windows.Navigation;

namespace Hospital.ViewModels.Manager
{
    public class DeleteMedicineViewModel : ViewModel
    {
        #region Properties
        public Medicine MedicineForDeleting { get; set; }
        NavigationService NavigationService { get; set; }

        #endregion

        #region Constructor
        public DeleteMedicineViewModel(NavigationService navigate, Medicine medicine)
        {
            MedicineForDeleting = medicine;
            NavigationService = navigate;

            CancelCommand = new RelayCommand(ExecuteCancelCommand, CanExecuteCommands);
            DeleteMedicineCommand = new RelayCommand(ExecuteDeleteMedicineCommand, CanExecuteCommands);
        }
        #endregion

        #region Commands
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand DeleteMedicineCommand { get; set; }

        #endregion

        #region Actions

        public void ExecuteDeleteMedicineCommand(object obj)
        {
            MedicineService service = new MedicineService(new MedicineFileRepository(), new MedicalRecordFileRepository());
            service.DeleteMedicine(MedicineForDeleting);

            NavigationService.Navigate(new MedicinesWindow(new MedicinesWindowViewModel(NavigationService)));
        }

        public void ExecuteCancelCommand(object obj)
        {
            NavigationService.Navigate(new MedicinesWindow(new MedicinesWindowViewModel(NavigationService)));
        }

        public bool CanExecuteCommands(object obj)
        {
            return true;
        }
        #endregion
    }
}
