using Hospital.Commands.DoctorCommands;
using Hospital.Controller;
using Hospital.Controller.DoctorControllers;
using Hospital.DTO.DoctorDTO;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Hospital.ViewModels.Doctor
{
    class EditMedicineViewModel : ViewModel
    {
        private MedicineService medicineService = new MedicineService();
        private ICollectionView medicineCollection;
        private EditMedicineController controller;
        private EditMedicineDTO dTO;
        public EditMedicineDTO DTO
        {
            get { return dTO; }
            set
            {
                dTO = value;
                OnPropertyChanged("DTO");
            }
        }
        public ICollectionView MedicineCollection
        {
            get { return medicineCollection; }
            set { medicineCollection = value; }
        }

        private ICollectionView ingredientCollection;

        public ICollectionView IngredientCollection
        {
            get { return ingredientCollection; }
            set { ingredientCollection = value; }
        }
        private string searchIngredient;

        public string SearchIngredient
        {
            get { return searchIngredient; }
            set 
            { 
                searchIngredient = value;
                OnPropertyChanged("SearchIngredient");
                Execute_SearchIngredientFilter();
            }
        }
        private Medicine selectedMedicine;
        public Medicine SelectedMedicine
        {
            get { return selectedMedicine; }
            set
            {
                selectedMedicine = value;
                OnPropertyChanged("SelectedMedicine");
                DTO.SelectedMedicine = SelectedMedicine;
                Execute_SelctedMedicChanged();
            }
        }

        private string searchMedicine;

        public string SearchMedicine
        {
            get { return searchMedicine; }
            set
            {
                searchMedicine = value;
                OnPropertyChanged("SearchMedicine");
                Execute_LekoviFilterTextChanged();
            }
        }
        private bool isEnableAddSubstitute;
        public bool IsEnableAddSubstitute
        {
            get { return isEnableAddSubstitute; }
            set
            {
                isEnableAddSubstitute = value;
                OnPropertyChanged("IsEnableAddSubstitute");
            }
        }
        private bool isEnableRemoveSubstitute;
        public bool IsEnableRemoveSubstitute
        {
            get { return isEnableRemoveSubstitute; }
            set
            {
                isEnableRemoveSubstitute = value;
                OnPropertyChanged("IsEnableRemoveSubstitute");
            }
        }
        private bool isEnableAddIngredient;
        public bool IsEnableAddIngredient
        {
            get { return isEnableAddIngredient; }
            set
            {
                isEnableAddIngredient = value;
                OnPropertyChanged("IsEnableAddIngredient");
            }
        }
        private bool isEnableRemoveIngredient;
        public bool IsEnableRemoveIngredient
        {
            get { return isEnableRemoveIngredient; }
            set
            {
                isEnableRemoveIngredient = value;
                OnPropertyChanged("IsEnableRemoveIngredient");
            }
        }
        private bool isEnableEdit;
        public bool IsEnableEdit
        {
            get { return isEnableEdit; }
            set
            {
                isEnableEdit = value;
                OnPropertyChanged("IsEnableEdit");
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
        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get { return editCommand; }
            set
            {
                editCommand = value;
            }
        }
        private RelayCommand medicineCommandFilterChanged;
        public RelayCommand MedicineCommandFilterChanged
        {
            get { return medicineCommandFilterChanged; }
            set
            {
                medicineCommandFilterChanged = value;
            }
        }
        private RelayCommand commandAddIngredient;
        public RelayCommand CommandAddIngredient
        {
            get { return commandAddIngredient; }
            set
            {
                commandAddIngredient = value;
            }
        }
        private RelayCommand commandRemoveIngredient;
        public RelayCommand CommandRemoveIngredient
        {
            get { return commandRemoveIngredient; }
            set
            {
                commandRemoveIngredient = value;
            }
        }
        private RelayCommand commandAddReplacement;
        public RelayCommand CommandAddReplacement
        {
            get { return commandAddReplacement; }
            set
            {
                commandAddReplacement = value;
            }
        }
        private RelayCommand commandRemoveReplacement;
        public RelayCommand CommandRemoveReplacement
        {
            get { return commandRemoveReplacement; }
            set
            {
                commandRemoveReplacement = value;
            }
        }
        private RelayCommand commandSearchIngredientFilter;
        public RelayCommand CommandSearchIngredientFilter
        {
            get { return commandSearchIngredientFilter; }
            set
            {
                commandSearchIngredientFilter = value;
            }
        }

        private RelayCommand commandSelectionChanged;
        public RelayCommand CommandSelectionChanged
        {
            get { return commandSelectionChanged; }
            set
            {
                commandSelectionChanged = value;
            }
        }

        public EditMedicineViewModel()
        {
            DTO = new EditMedicineDTO();
            controller = new EditMedicineController(DTO);
            NewMethod();
            SetButtonIsEnableProperty();
            DTO.Ingredients = new ObservableCollection<Ingredient>(medicineService.ReadIngredients());
            SetProperites();
            AddFilterAndSorter();
        }

        private void NewMethod()
        {
            CommandRemoveReplacement = new RelayCommand(Execute_RemoveReplacement, CanExecute_Command);
            CommandAddReplacement = new RelayCommand(Execute_AddReplacement, CanExecute_Command);
            CommandRemoveIngredient = new RelayCommand(Execute_RemoveIngridient, CanExecute_Command);
            CommandAddIngredient = new RelayCommand(Execute_AddIngridient, CanExecute_Command);
            SaveCommand = new RelayCommand(Execute_SaveChanges, CanExecute_Command);
            EditCommand = new RelayCommand(Execute_EditMedic, CanExecute_Command);
        }

        private void SetButtonIsEnableProperty()
        {
            IsEnableEdit = true;
            IsEnableRemoveIngredient = false;
            IsEnableAddIngredient = false;
            IsEnableRemoveSubstitute = false;
            IsEnableAddSubstitute = false;
        }

        private void SetProperites()
        {
            DTO.MedicineIngredients = new ObservableCollection<Ingredient>();
            DTO.MedicineForDisplay = new ObservableCollection<Medicine>(medicineService.GetAll());
            SelectedMedicine = DTO.MedicineForDisplay.First();
            DTO.Medicine = SelectedMedicine;
            SetReplacementMedicine();
        }

        private void AddFilterAndSorter()
        {
            MedicineCollection = CollectionViewSource.GetDefaultView(DTO.MedicineForDisplay);
            IngredientCollection = CollectionViewSource.GetDefaultView(DTO.Ingredients);
            IngredientCollection.Filter = filterIngredients;
            MedicineCollection.Filter = filterMedics;
            ICollectionView view = GetPretraga();
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Ascending));
        }

        private bool filterIngredients(object obj)
        {
            if (string.IsNullOrEmpty(SearchIngredient))
            {
                return true;
            }
            else
            {
                return (obj.ToString().IndexOf(SearchIngredient, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private bool filterMedics(object obj)
        {
            if (string.IsNullOrEmpty(SearchMedicine))
            {
                return true;
            }
            else
            {
                return (obj.ToString().IndexOf(SearchMedicine, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private void Execute_LekoviFilterTextChanged()
        {
            CollectionViewSource.GetDefaultView(DTO.MedicineForDisplay).Refresh();
        }


        public ICollectionView GetPretraga()
        {
            return CollectionViewSource.GetDefaultView(DTO.MedicineForDisplay);
        }

        private void Execute_SelctedMedicChanged()
        {
            if (SelectedMedicine != null && IsEnableRemoveIngredient.Equals(false))
            {
                DTO.DosageInMg = SelectedMedicine.DosageInMg.ToString();
                DTO.Medicine = SelectedMedicine;
                SetReplacementMedicine();
            }
        }

        private void SetReplacementMedicine()
        {
            if (SelectedMedicine != null)
            {
                DTO.SubstituteDrugs = new ObservableCollection<Medicine>(controller.SetReplacementMedicine());
                DTO.MedicineIngredients = new ObservableCollection<Ingredient>(DTO.Medicine.Ingredient);
            }
        }

        private void Execute_EditMedic(object sender)
        {
            IsEnableAddSubstitute = true;
            IsEnableRemoveSubstitute = true;
            IsEnableRemoveIngredient = true;
            IsEnableAddIngredient = true;
            IsEnableEdit = false;
        }

        private void Execute_AddIngridient(object sender)
        {
            if (DTO.SelectedIngredient != null && ! controller.MedicineContainsIngredient())
            {
                DTO.Medicine.AddIngredient(DTO.SelectedIngredient);
                DTO.MedicineIngredients.Add(DTO.SelectedIngredient);
            }
        }

        private void Execute_RemoveIngridient(object sender)
        {
            if (DTO.SelectedMedicineIngredient != null)
            {
                MessageBox.Show(DTO.SelectedMedicineIngredient.Name);
                DTO.Medicine.RemoveIngredient(DTO.SelectedMedicineIngredient);
                DTO.MedicineIngredients.Remove(DTO.SelectedMedicineIngredient);
            }
        }

        private void Execute_AddReplacement(object sender)
        {
            if (!DTO.Medicine.ID.Equals(SelectedMedicine.ID) && !controller.MedicineAlreadyInSubstituteDrugs())
            {
                DTO.Medicine.AddMedicineID((SelectedMedicine).ID);
                DTO.SubstituteDrugs.Add(SelectedMedicine);
            }
        }

        private void Execute_RemoveReplacement(object sender)
        {
            if (DTO.SelectedSubstituteDrugs != null)
            {
                DTO.Medicine.RemoveMedicineID(DTO.SelectedSubstituteDrugs.ID);
                DTO.SubstituteDrugs.Remove(DTO.SelectedSubstituteDrugs);
            }
        }
        private void Execute_SaveChanges(object sender)
        {
            DTO.Medicine.DosageInMg = Convert.ToDouble(DTO.DosageInMg);
            controller.SaveMedicineChanges();
            SavedChangesVisibilities();
        }

        private void SavedChangesVisibilities()
        {
            IsEnableAddSubstitute = false;
            IsEnableRemoveSubstitute = false;
            IsEnableRemoveIngredient = false;
            IsEnableAddIngredient = false;
            IsEnableEdit = true;
        }

        private void Execute_SearchIngredientFilter()
        {
            CollectionViewSource.GetDefaultView(DTO.Ingredients).Refresh();
        }
        private bool CanExecute_Command(object obj)
        {
            return true;
        }
    }
}
