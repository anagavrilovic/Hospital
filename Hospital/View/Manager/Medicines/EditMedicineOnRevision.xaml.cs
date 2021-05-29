using Hospital.Model;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hospital.View
{
    public partial class EditMedicineOnRevision : Page
    {
        public MedicineRevision MedicineOnRevision { get; set; }
        public List<string> Ingredients { get; set; }
        private string _searchCriterion;
        public ICollectionView IngredientsCollection {  get; set; }

        public EditMedicineOnRevision(MedicineRevision medicineRevision)
        {
            InitializeComponent();
            this.DataContext = this;
            MedicineOnRevision = medicineRevision;

            AddIngredientsInListBox();
            AddDoctorsInComboBox();
            SetDoctorOnComboBox();
        }

        private void AddDoctorsInComboBox()
        {
            ObservableCollection<string>  DoctorsNameSurname = new ObservableCollection<string>();
            DoctorService doctorService = new DoctorService();
            ObservableCollection<Model.Doctor> doctors = new ObservableCollection<Model.Doctor>(doctorService.GetAll());
            foreach (Hospital.Model.Doctor doctor in doctors)
                DoctorsNameSurname.Add(doctor.ToString());

            doctorsCB.ItemsSource = DoctorsNameSurname;
        }

        private void SetDoctorOnComboBox()
        {
            DoctorService doctorService = new DoctorService();
            doctorsCB.SelectedItem = doctorService.GetDoctorById(MedicineOnRevision.DoctorID).ToString();
        }

        private void AddIngredientsInListBox()
        {
            MedicineService medicineService = new MedicineService();
            Ingredients = medicineService.GetAllIngredients();
        }

        private void SearchIngredients(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox != null)
            {
                this._searchCriterion = textbox.Text;
                if (!string.IsNullOrEmpty(_searchCriterion))
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(allIngredientsList.ItemsSource);
                    view.Filter = new Predicate<object>(Filter);
                    IngredientsCollection.Refresh();
                }
                else
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(Ingredients);
                    IngredientsCollection.Refresh();
                }
            }
        }

        private bool Filter(object item)
        {
            if (((string)item).Contains(_searchCriterion))
            {
                return true;
            }
            return false;
        }

        private void BtnPlusIngredients(object sender, RoutedEventArgs e)
        {
            Ingredient newIngredient = new Ingredient();
            string selectedIngredient = (string)allIngredientsList.SelectedItem;
            if (selectedIngredient == null)
                return;

            newIngredient.Name = selectedIngredient;

            foreach (Ingredient ing in MedicineOnRevision.Medicine.Ingredient)
                if (ing.Name.Equals(newIngredient.Name))
                    return;

            MedicineOnRevision.Medicine.AddIngredient(newIngredient);
            ingredientsList.Items.Refresh();
        }

        private void BtnMinusIngredients(object sender, RoutedEventArgs e)
        {
            Ingredient ingredientToDelete = (Ingredient)ingredientsList.SelectedItem; 
            if (ingredientToDelete == null)
                return;

            MedicineOnRevision.Medicine.RemoveIngredient(ingredientToDelete);
            ingredientsList.ItemsSource = MedicineOnRevision.Medicine.Ingredient;
            ingredientsList.Items.Refresh();
        }

        private void BtnSearchMouseDown(object sender, RoutedEventArgs e)
        {
            IngredientsCollection.Refresh();
        }


        private void SendAgainOnRevision(object sender, RoutedEventArgs e)
        {
            priceTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            quantityTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            doctorsCB.GetBindingExpression(ComboBox.SelectedItemProperty).UpdateSource();

            MedicineRevisionService service = new MedicineRevisionService();
            service.EditMedicine(MedicineOnRevision);

            NavigationService.Navigate(new MedicineRevisionWindow());
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MedicineRevisionWindow());
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MedicineRevisionWindow());
        }
    }
}
