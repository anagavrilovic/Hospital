using Hospital.Commands.Manager;
using Hospital.Services;
using Hospital.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Navigation;

namespace Hospital.ViewModels.Manager
{
    public class MedicinesWindowViewModel : ViewModel
    {

        #region Properties

        private ObservableCollection<Medicine> _medicines;
        public ObservableCollection<Medicine> Medicines
        {
            get => _medicines;
            set
            {
                _medicines = value;
                OnPropertyChanged("Medicines");
            }
        }

        public Medicine SelectedMedicine { get; set; }
        public MedicineService MedicineService { get; set; }
        public NavigationService NavigationService { get; set; }

        public string SearchCriterion { get; set; }
        public ICollectionView MedicineCollection { get; set; }
        #endregion

        #region Constructor
        public MedicinesWindowViewModel(NavigationService navigate)
        {
            MedicineService = new MedicineService();
            SelectedMedicine = new Medicine();
            Medicines = new ObservableCollection<Medicine>(MedicineService.GetAll());
            MedicineCollection = CollectionViewSource.GetDefaultView(Medicines);
            AddMedicineCommand = new RelayCommand(ExecuteAddMedicineCommand, CanExecuteCommands);
            EditMedicineCommand = new RelayCommand(ExecuteEditMedicineCommand, CanExecuteCommandsCondition);
            DeleteMedicineCommand = new RelayCommand(ExecuteDeleteMedicineCommand, CanExecuteCommandsCondition);
            SearchMedicineCommand = new RelayCommand(ExecuteSearchMedicineCommand, CanExecuteCommands);
            BackCommand = new RelayCommand(ExecuteBackCommand, CanExecuteCommands);
            MedicineRevisionViewCommand = new RelayCommand(ExecuteMedicineRevisionViewCommand, CanExecuteCommands);
            RefreshViewCommand = new RelayCommand(ExecuteBackCommand, CanExecuteCommands);
            NavigationService = navigate;
        }
        #endregion

        #region CommandsDeclaration
        public RelayCommand AddMedicineCommand   { get; set; }
        public RelayCommand EditMedicineCommand  { get; set; }
        public RelayCommand DeleteMedicineCommand { get; set; }
        public RelayCommand SearchMedicineCommand { get; set; }
        public RelayCommand RefreshViewCommand    { get; set; }
        public RelayCommand BackCommand           { get; set; }
        public RelayCommand MedicineRevisionViewCommand { get; set; }
        #endregion

        #region Actions

        public void ExecuteAddMedicineCommand(object obj)
        {
            AddMedicine addMedicine = new AddMedicine();
            NavigationService.Navigate(addMedicine);
        }

        public void ExecuteEditMedicineCommand(object obj)
        {
            SelectedMedicine = MedicineService.GetById(SelectedMedicine.ID);
            if (SelectedMedicine == null)
                return;

            EditMedicine editMedicine = new EditMedicine(SelectedMedicine);
            NavigationService.Navigate(editMedicine);
        }

        public void ExecuteDeleteMedicineCommand(object obj)
        {
            SelectedMedicine = MedicineService.GetById(SelectedMedicine.ID);
            if (SelectedMedicine == null)
                return;

            NavigationService.Navigate(new DeleteMedicine(SelectedMedicine));
        }

        public void ExecuteMedicineRevisionViewCommand(object obj)
        {
            MedicineRevisionWindow medicineRevisionWindow = new MedicineRevisionWindow(new MedicineRevisionViewModel(NavigationService));
            NavigationService.Navigate(medicineRevisionWindow);
        }

        public void ExecuteSearchMedicineCommand(object obj)
        {
             if (!string.IsNullOrEmpty(SearchCriterion))
             {
                  ICollectionView view = CollectionViewSource.GetDefaultView(Medicines);
                  view.Filter = new Predicate<object>(Filter);
             }
             else
             {
                  ICollectionView view = CollectionViewSource.GetDefaultView(Medicines);
             }

            this.MedicineCollection.Refresh();
        }

        private bool Filter(object item)
        {
            if (((Medicine)item).Name.Contains(SearchCriterion) || ((Medicine)item).ID.Contains(SearchCriterion) || ((Medicine)item).Quantity.ToString().Contains(SearchCriterion) || ((Medicine)item).Price.ToString().Contains(SearchCriterion))
                return true;

            return false;
        }

        public void ExecuteRefreshViewCommand(object obj)
        {
            MedicineCollection.Refresh();
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
            if (SelectedMedicine == null)
                return false;

            return true;
        }

        #endregion
    }
}
