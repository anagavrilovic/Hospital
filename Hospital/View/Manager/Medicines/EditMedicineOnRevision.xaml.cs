using Hospital.Model;
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

        public ObservableCollection<MedicineRevision> AllMedicinesOnRevision { get; set; }
        private MedicineRevisionStorage _medicineRevisionStorage;

        public List<string> Ingredients { get; set; }

        private string searchstr;

        public ICollectionView IngredientsCollection {  get; set; }

        public EditMedicineOnRevision(MedicineRevision medicineRevision)
        {
            InitializeComponent();
            this.DataContext = this;
            this._medicineRevisionStorage = new MedicineRevisionStorage();
            MedicineOnRevision = medicineRevision;
            AllMedicinesOnRevision = _medicineRevisionStorage.GetAll();

            AddIngredientsInListBox();
            AddDoctorsInComboBox();
            SetDoctorOnComboBox();
        }

        private void AddDoctorsInComboBox()
        {
            ObservableCollection<string>  DoctorsNameSurname = new ObservableCollection<string>();
            DoctorStorage doctorStorage = new DoctorStorage();
            foreach (Hospital.Model.Doctor doctor in doctorStorage.GetAll())
            {
                DoctorsNameSurname.Add(doctor.ToString());
            }

            doctorsCB.ItemsSource = DoctorsNameSurname;
        }

        private void SetDoctorOnComboBox()
        {
            DoctorStorage doctorStorage = new DoctorStorage();
            doctorsCB.SelectedItem = doctorStorage.GetOne(MedicineOnRevision.DoctorID).ToString();
            Console.WriteLine(doctorStorage.GetOne(MedicineOnRevision.DoctorID).ToString());
        }

        private void AddIngredientsInListBox()
        {
            Ingredients = new List<string>();
            string[] ingredients = File.ReadAllLines("..\\..\\Files\\ingredients.txt");
            foreach (string line in ingredients)
                Ingredients.Add(line);
        }

        private void SearchIngredients(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox != null)
            {
                this.searchstr = textbox.Text;
                if (!string.IsNullOrEmpty(searchstr))
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
            if (((string)item).Contains(searchstr))
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
            {
                if (ing.Name.Equals(newIngredient.Name))
                    return;
            }

            MedicineOnRevision.Medicine.AddIngredient(newIngredient);
            ingredientsList.Items.Refresh();
        }

        private void BtnMinusIngredients(object sender, RoutedEventArgs e)
        {
            Ingredient ingredientToDelete = (Ingredient)ingredientsList.SelectedItem; ;
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

            _medicineRevisionStorage.EditMedicine(MedicineOnRevision);

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
