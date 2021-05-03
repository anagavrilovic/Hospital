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
    public partial class EditMedicine : Window
    {
        public MedicineRevision MedicineOnRevision { get; set; }

        public ObservableCollection<MedicineRevision> AllMedicinesOnRevision { get; set; }
        private MedicineRevisionStorage medicineRevisionStorage;

        public List<string> Ingredients { get; set; }

        private string searchstr;

        private ICollectionView ingredientsCollection;

        public ICollectionView IngredientsCollection
        {
            get { return ingredientsCollection; }
            set { ingredientsCollection = value; }
        }

        public EditMedicine(MedicineRevision medicineRevision)
        {
            InitializeComponent();
            this.DataContext = this;
            this.medicineRevisionStorage = new MedicineRevisionStorage();
            MedicineOnRevision = medicineRevision;
            AllMedicinesOnRevision = medicineRevisionStorage.GetAll();
            addIngredientsInListBox();
            addDoctorsInComboBox();
            setDoctorOnComboBox();
        }

        private void addDoctorsInComboBox()
        {
            ObservableCollection<string>  DoctorsNameSurname = new ObservableCollection<string>();
            DoctorStorage doctorStorage = new DoctorStorage();
            foreach (Hospital.Model.Doctor doctor in doctorStorage.GetAll())
            {
                DoctorsNameSurname.Add(doctor.ToString());
            }

            doctorsCB.ItemsSource = DoctorsNameSurname;
        }

        private void setDoctorOnComboBox()
        {
            DoctorStorage doctorStorage = new DoctorStorage();
            doctorsCB.SelectedItem = doctorStorage.GetOne(MedicineOnRevision.DoctorID).ToString();
            Console.WriteLine(doctorStorage.GetOne(MedicineOnRevision.DoctorID).ToString());
        }

        private void addIngredientsInListBox()
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
                    view.Filter = new Predicate<object>(filter);
                    IngredientsCollection.Refresh();
                }
                else
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(Ingredients);
                    IngredientsCollection.Refresh();
                }
            }
        }

        private bool filter(object item)
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
            ingredientsCollection.Refresh();
        }


        private void SendAgainOnRevision(object sender, RoutedEventArgs e)
        {
            priceTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            quantityTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            doctorsCB.GetBindingExpression(ComboBox.SelectedItemProperty).UpdateSource();

            medicineRevisionStorage.DoSerialization(AllMedicinesOnRevision);

            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
