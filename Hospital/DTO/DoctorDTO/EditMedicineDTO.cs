using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DTO.DoctorDTO
{
    public class EditMedicineDTO : ViewModel
    {
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
        private Medicine medicine;
        public Medicine Medicine
        {
            get { return medicine; }
            set
            {
                medicine = value;
                OnPropertyChanged("Medicine");
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
            }
        }

    }
}
