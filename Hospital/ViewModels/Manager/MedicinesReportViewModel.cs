using Hospital.Commands.Manager;
using Hospital.Factory;
using Hospital.ReportsPatterns;
using Hospital.View;
using Hospital.View.Manager;
using System.Collections.Generic;
using System.Windows.Navigation;

namespace Hospital.ViewModels.Manager
{
    public class MedicinesReportViewModel : ViewModel
    {
        #region Properties
        public List<Medicine> Medicines { get; set; }
        public Services.MedicineService MedicineService { get; set; }
        public NavigationService NavigationService { get; set; }

        #endregion

        #region Constructor
        public MedicinesReportViewModel(NavigationService navigate)
        {
            MedicineService = new Services.MedicineService(new MedicineFileFactory(), new MedicalRecordFileFactory());
            Medicines = MedicineService.GetAll();
            GenerateReportCommand = new RelayCommand(Execute_GenerateReportCommand, CanExecuteCommands);
            BackCommand = new RelayCommand(Execute_BackCommand, CanExecuteCommands);
            NavigationService = navigate;          
        }
        #endregion

        #region Commands
        public RelayCommand GenerateReportCommand { get; set; }
        public RelayCommand BackCommand { get; set; }


        public void Execute_GenerateReportCommand(object obj)
        {
            ReportGenerator reportGenerator = new ReportsPatterns.MedicinesReport();
            reportGenerator.GenerateReport();

            MessageWindow message = new MessageWindow("Izveštaj možete pregledati u folderu Records");
            message.Show();
        }

        public void Execute_BackCommand(object obj)
        {
            NavigationService.Navigate(new ManagerMainPage());
        }

        public bool CanExecuteCommands(object obj)
        {
            return true;
        }

        #endregion
    }
}
