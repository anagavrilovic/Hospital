using Hospital.Commands.DoctorCommands;
using Hospital.Services;
using Hospital.View.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital.ViewModels.Doctor
{
    public class DoctorReportViewModel :ViewModel
    {
        private MedicineService medicineService = new MedicineService();
        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged("StartDate");
            }
        }
        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged("EndDate");
            }
        }
        private RelayCommand reportCommand;
        public RelayCommand ReportCommand
        {
            get { return reportCommand; }
            set
            {
                reportCommand = value;
            }
        }

        public DoctorReportViewModel()
        {
            ReportCommand = new RelayCommand(Execute_Report, CanExecuteMethod);
            EndDate = DateTime.Now;
            StartDate = DateTime.Now;
            medicineService = new MedicineService();
        }
        private void Execute_Report(object obj)
        {
            Validate();
            List<Medicine> medicines = medicineService.GetConsumedMedicineInPeriod(StartDate, EndDate);
            MessageBox.Show(medicines.Count.ToString());
        }

        private void Validate()
        {
            if (EndDate < StartDate)
            {
                ErrorBox error = new ErrorBox("Neispravan izbor datuma");
                return;
            }
        }

        private bool CanExecuteMethod(object parameter)
        {
            return true;
        }

    }
}
