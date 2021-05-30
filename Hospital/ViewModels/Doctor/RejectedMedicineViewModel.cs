using Hospital.Commands.DoctorCommands;
using Hospital.Model;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ViewModels.Doctor
{
    public class RejectedMedicineViewModel : ViewModel
    {
        public Action CloseAction { get; set; }
        private RelayCommand backCommand;
        public RelayCommand BackCommand
        {
            get { return backCommand; }
            set
            {
                backCommand = value;
            }
        }
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
            set
            {
                saveCommand = value;
            }
        }
        private MedicineRevision medicineRevision = new MedicineRevision();
        private MedicineRevisionService medicineRevisionService = new MedicineRevisionService();
        public MedicineRevision MedicineRevision
        {
            get
            {
                return medicineRevision;
            }
            set
            {
                if (value != medicineRevision)
                {
                    medicineRevision = value;
                    OnPropertyChanged("MedicineRevision");
                }
            }
        }
        public RejectedMedicineViewModel(MedicineRevision medicineRevision)
        {
            SaveCommand = new RelayCommand(Execute_SaveRejection, CanExecute_Command);
            BackCommand = new RelayCommand(Execute_Back, CanExecute_Command);
            this.medicineRevision = medicineRevision;
        }
        private void Execute_Back(object sender)
        {
            CloseAction();
        }
        private bool CanExecute_Command(object obj)
        {
            return true;
        }
        private void Execute_SaveRejection(object sender)
        {
            MedicineRevision.IsMedicineRevised = true;
            medicineRevisionService.EditMedicine(MedicineRevision);
            CloseAction();
        }
    }
}
