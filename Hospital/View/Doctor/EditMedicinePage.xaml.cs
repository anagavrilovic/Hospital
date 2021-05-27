using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View.Doctor
{
    /// <summary>
    /// Interaction logic for Lekovi.xaml
    /// </summary>
    public partial class EditMedicinePage : Page, INotifyPropertyChanged
    {
        private MedicineService medicineService = new MedicineService();
        private Medicine medicine = new Medicine();
        public Medicine Medicine
        {
            get { return medicine; }
            set
            {
                medicine = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Medicine> medicineForDisplay;
        public ObservableCollection<Medicine> MedicineForDisplay
        {
            get { return medicineForDisplay; }
            set
            {
                medicineForDisplay = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Medicine> substituteDrugs = new ObservableCollection<Medicine>();
        public ObservableCollection<Medicine> SubstituteDrugs
        {
            get { return substituteDrugs; }
            set
            {
                substituteDrugs = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>();
        public ObservableCollection<Ingredient> Ingredients
        {
            get { return ingredients; }
            set
            {
                ingredients = value;
                OnPropertyChanged();
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

        public EditMedicinePage()
        {
            InitializeComponent();
            this.DataContext = this;
            Ingredients =new ObservableCollection<Ingredient>(medicineService.ReadIngredients());
            SetProperites();
            AddFilterAndSorter();
            SetReplacementMedicine();
        }

        private void SetProperites()
        {
            medicineIngredientsListBox.ItemsSource = Medicine.Ingredient;
            medicineForDisplay = new ObservableCollection<Medicine>(medicineService.GetAll());
            allDrugsListBox.ItemsSource = medicineForDisplay;
            allIngredientsListBox.ItemsSource = Ingredients;
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
            if (string.IsNullOrEmpty(pretraziSastojak.Text))
            {
                return true;
            }
            else
            {
                return (obj.ToString().IndexOf(pretraziSastojak.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private bool filterMedics(object obj)
        {
            if (string.IsNullOrEmpty(pretrazi.Text))
            {
                return true;
            }
            else
            {
                return (obj.ToString().IndexOf(pretrazi.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private void SetReplacementMedicine()
        {
            if (Medicine != null)
            {
                SubstituteDrugs =new ObservableCollection<Medicine>(medicineService.SetReplacementMedicine(Medicine));
                replacementDrugsListBox.ItemsSource = SubstituteDrugs;
                replacementDrugsListBox.Items.Refresh();
            }
        }

        private void LekoviFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(allDrugsListBox.ItemsSource).Refresh();
        }


        public ICollectionView GetPretraga()
        {
            return CollectionViewSource.GetDefaultView(MedicineForDisplay);
        }

        private void SelctedMedicChanged(object sender, SelectionChangedEventArgs e)
        {
            Medicine selectedMedic = (Medicine)allDrugsListBox.SelectedItem;
            if (selectedMedic != null && sacuvaj.IsEnabled.Equals(false))
            {
                Medicine = selectedMedic;
                SetReplacementMedicine();
                medicineIngredientsListBox.ItemsSource = Medicine.Ingredient;
                medicineIngredientsListBox.Items.Refresh();
            }
        }

       

        private void EditMedic_Click(object sender, RoutedEventArgs e)
        {
            doza.IsEnabled = true;
            dodajZamenu.IsEnabled = true;
            izbaciZamenu.IsEnabled = true;
            sacuvaj.IsEnabled = true;
            izbaciSastojak.IsEnabled = true;
            dodajSastojak.IsEnabled = true;
            izmeni.IsEnabled = false;
        }

        private void AddIngridient_Click(object sender, RoutedEventArgs e)
        {
            Ingredient seletedIngredient = (Ingredient)allIngredientsListBox.SelectedItem;
            if (seletedIngredient != null && !medicineService.ContainsIngredient(Medicine, seletedIngredient))
            {
                Medicine.AddIngredient(seletedIngredient);
                medicineIngredientsListBox.ItemsSource = Medicine.Ingredient;
                medicineIngredientsListBox.Items.Refresh();
            }
        }

        private void RemoveIngridient_Click(object sender, RoutedEventArgs e)
        {
            if (medicineIngredientsListBox.SelectedItem != null)
            {
                Medicine.RemoveIngredient((Ingredient)medicineIngredientsListBox.SelectedItem);
                medicineIngredientsListBox.ItemsSource = Medicine.Ingredient;
                medicineIngredientsListBox.Items.Refresh();
            }
        }

        private void AddReplacement_Click(object sender, RoutedEventArgs e)
        {
            Medicine selectedMedic = ((Medicine)allDrugsListBox.SelectedItem);
            if (!Medicine.ID.Equals(selectedMedic.ID) && !medicineService.AlreadyInSubstituteDrugs(selectedMedic, SubstituteDrugs.ToList()))
            {
                Medicine.AddMedicineID(((Medicine)allDrugsListBox.SelectedItem).ID);
                SubstituteDrugs.Add((Medicine)allDrugsListBox.SelectedItem);
                replacementDrugsListBox.ItemsSource = SubstituteDrugs;
                replacementDrugsListBox.Items.Refresh();
            }
        }

        private void RemoveReplacement_Click(object sender, RoutedEventArgs e)
        {
            Medicine selectedReplacementMedic = ((Medicine)replacementDrugsListBox.SelectedItem);
            if (selectedReplacementMedic != null)
            {
                Medicine.RemoveMedicineID(selectedReplacementMedic.ID);
                SubstituteDrugs.Remove(selectedReplacementMedic);
                replacementDrugsListBox.ItemsSource = SubstituteDrugs;
                replacementDrugsListBox.Items.Refresh();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {

                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            medicineService.SaveMedicineSubstitutes(SubstituteDrugs.ToList(), Medicine);
            SavedChangesVisibilities();
        }

        private void SavedChangesVisibilities()
        {
            doza.IsEnabled = false;
            dodajZamenu.IsEnabled = false;
            izbaciZamenu.IsEnabled = false;
            sacuvaj.IsEnabled = false;
            izbaciSastojak.IsEnabled = false;
            dodajSastojak.IsEnabled = false;
            izmeni.IsEnabled = true;
        }

        private void SearchIngredientFilter(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(allIngredientsListBox.ItemsSource).Refresh();
        }
    }
}
