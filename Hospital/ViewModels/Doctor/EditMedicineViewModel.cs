using Hospital.Commands.DoctorCommands;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Hospital.ViewModels.Doctor
{
    class EditMedicineViewModel : ViewModel
    {
        private MedicineService medicineService = new MedicineService();
        private Medicine medicine = new Medicine();
        public Medicine Medicine
        {
            get { return medicine; }
            set
            {
                medicine = value;
                OnPropertyChanged("Medicine");
            }
        }
        private ObservableCollection<Medicine> medicineForDisplay;
        public ObservableCollection<Medicine> MedicineForDisplay
        {
            get { return medicineForDisplay; }
            set
            {
                medicineForDisplay = value;
                OnPropertyChanged("MedicineForDisplay");
            }
        }
        private ObservableCollection<Medicine> substituteDrugs = new ObservableCollection<Medicine>();
        public ObservableCollection<Medicine> SubstituteDrugs
        {
            get { return substituteDrugs; }
            set
            {
                substituteDrugs = value;
                OnPropertyChanged("SubstituteDrugs");
            }
        }
        private Ingredient selectedIngredient;
        public Ingredient SelectedIngredient
        {
            get { return selectedIngredient; }
            set
            {
                selectedIngredient = value;
                OnPropertyChanged("SelectedIngredient");
            }
        }

        private ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>();
        public ObservableCollection<Ingredient> Ingredients
        {
            get { return ingredients; }
            set
            {
                ingredients = value;
                OnPropertyChanged("Ingredients");
            }
        }

        private ICollectionView medicineCollection;

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
        public string dosageInMg;
        public string DosageInMg
        {
            get { return dosageInMg; }
            set
            {
                dosageInMg = value;
                OnPropertyChanged("DosageInMg");
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
        private Medicine selectedMedicine;
        public Medicine SelectedMedicine
        {
            get { return selectedMedicine; }
            set
            {
                selectedMedicine = value;
                OnPropertyChanged("SelectedMedicine");
                SelctedMedicChanged();
            }
        }
        private Medicine selectedSubstituteDrugs;
        public Medicine SelectedSubstituteDrugs
        {
            get { return selectedSubstituteDrugs; }
            set
            {
                selectedSubstituteDrugs = value;
                OnPropertyChanged("SelectedSubstituteDrugs");
            }
        }
        private Ingredient selectedMedicineIngredient;
        public Ingredient SelectedMedicineIngredient
        {
            get { return selectedMedicineIngredient; }
            set
            {
                selectedMedicineIngredient = value;
                OnPropertyChanged("SelectedMedicineIngredient");
            }
        }
        private ObservableCollection<Ingredient> medicineIngredients;
        public ObservableCollection<Ingredient> MedicineIngredients
        {
            get { return medicineIngredients; }
            set
            {
                medicineIngredients = value;
                OnPropertyChanged("MedicineIngredients");
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

        public EditMedicineViewModel()
        {
            NewMethod();
            SetButtonIsEnableProperty();
            Ingredients = new ObservableCollection<Ingredient>(medicineService.ReadIngredients());
            SetProperites();
            AddFilterAndSorter();
            SetReplacementMedicine();
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
            MedicineIngredients = new ObservableCollection<Ingredient>();
            MedicineForDisplay = new ObservableCollection<Medicine>(medicineService.GetAll());
            SelectedMedicine = MedicineForDisplay.First();
            Medicine = SelectedMedicine;
        }

        private void AddFilterAndSorter()
        {
            MedicineCollection = CollectionViewSource.GetDefaultView(MedicineForDisplay);
            IngredientCollection = CollectionViewSource.GetDefaultView(Ingredients);
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

        private void SetReplacementMedicine()
        {
            if (SelectedMedicine != null)
            {
                SubstituteDrugs = new ObservableCollection<Medicine>(medicineService.SetReplacementMedicine(Medicine));
            }
        }

        private void Execute_LekoviFilterTextChanged()
        {
            CollectionViewSource.GetDefaultView(MedicineForDisplay).Refresh();
            OnPropertyChanged("MedicineForDisplay");
        }


        public ICollectionView GetPretraga()
        {
            return CollectionViewSource.GetDefaultView(MedicineForDisplay);
        }

        private void SelctedMedicChanged()
        {
            if (SelectedMedicine != null && IsEnableRemoveIngredient.Equals(false))
            {
                DosageInMg = SelectedMedicine.DosageInMg.ToString();    
                Medicine = SelectedMedicine;
                SetReplacementMedicine();
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
            if (SelectedIngredient != null && !medicineService.ContainsIngredient(Medicine, SelectedIngredient))
            {
                Medicine.AddIngredient(SelectedIngredient);
                MedicineIngredients.Add(SelectedIngredient);
            }
        }

        private void Execute_RemoveIngridient(object sender)
        {
            if (SelectedMedicineIngredient != null)
            {
                Medicine.RemoveIngredient(SelectedMedicineIngredient);
                MedicineIngredients.Remove(SelectedMedicineIngredient);
            }
        }

        private void Execute_AddReplacement(object sender)
        {
            if (!Medicine.ID.Equals(SelectedMedicine.ID) && !medicineService.AlreadyInSubstituteDrugs(SelectedMedicine, SubstituteDrugs.ToList()))
            {
                Medicine.AddMedicineID((SelectedMedicine).ID);
                SubstituteDrugs.Add(SelectedMedicine);
            }
        }

        private void Execute_RemoveReplacement(object sender)
        {
            if (SelectedSubstituteDrugs != null)
            {
                Medicine.RemoveMedicineID(SelectedSubstituteDrugs.ID);
                SubstituteDrugs.Remove(SelectedSubstituteDrugs);
            }
        }
        private void Execute_SaveChanges(object sender)
        {
            Medicine.DosageInMg = Convert.ToDouble(DosageInMg);
            medicineService.SaveMedicineSubstitutes(SubstituteDrugs.ToList(), Medicine);
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
            CollectionViewSource.GetDefaultView(Ingredients).Refresh();
        }
        private bool CanExecute_Command(object obj)
        {
            return true;
        }
    }
}
