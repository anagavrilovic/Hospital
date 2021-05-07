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
    /// <summary>
    /// Interaction logic for AddMedicine.xaml
    /// </summary>
    public partial class AddMedicine : Page, INotifyPropertyChanged
    { 
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private MedicineRevision medicineRevision;
        public MedicineRevision MedicineRevision
        {
            get => medicineRevision;
            set
            {
                medicineRevision = value;
                OnPropertyChanged("MedicineRevision");
            }
        }

        private ObservableCollection<string> doctorsNameSurname;
        public ObservableCollection<string> DoctorsNameSurname
        {
            get
            {
                return doctorsNameSurname;
            }

            set
            {
                doctorsNameSurname = value;
                OnPropertyChanged("DoctorsNameSurname");
            }
        }

        public List<string> Ingredients { get; set; }

        private string searchstr;

        private ICollectionView ingredientsCollection;

        public ICollectionView IngredientsCollection
        {
            get { return ingredientsCollection; }
            set { ingredientsCollection = value; }
        }

        public AddMedicine()
        {
            InitializeComponent();
            MedicineRevision = new MedicineRevision();
            MedicineRevision.Medicine = new Medicine();
            addDoctorsInComboBox();
            addIngredientsInListBox();
            IngredientsCollection = CollectionViewSource.GetDefaultView(Ingredients);

            this.DataContext = this;
        }

        private void addDoctorsInComboBox()
        {
            DoctorsNameSurname = new ObservableCollection<string>();
            DoctorStorage doctorStorage = new DoctorStorage();
            foreach (Hospital.Model.Doctor doctor in doctorStorage.GetAll())
            {
                DoctorsNameSurname.Add(doctor.ToString());
            }
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
            string selectedIngredient = (string) allIngredientsList.SelectedItem;
            if (selectedIngredient == null)
                return;

            newIngredient.Name = selectedIngredient;
      
            foreach (Ingredient ing in MedicineRevision.Medicine.Ingredient)
            {
                if (ing.Name.Equals(newIngredient.Name))
                    return;     
            }

            MedicineRevision.Medicine.AddIngredient(newIngredient);
            ingredientsList.Items.Refresh();
        }

        private void BtnMinusIngredients(object sender, RoutedEventArgs e)
        {
            Ingredient ingredientToDelete = (Ingredient) ingredientsList.SelectedItem;;
            if (ingredientToDelete == null)
                return;

            MedicineRevision.Medicine.RemoveIngredient(ingredientToDelete);
            ingredientsList.ItemsSource = MedicineRevision.Medicine.Ingredient;
            ingredientsList.Items.Refresh();
        }

        private void BtnSearchMouseDown(object sender, RoutedEventArgs e)
        {
            ingredientsCollection.Refresh();
        }

        private void SendOnRevision(object sender, RoutedEventArgs e)
        {
            string doctorSelected = doctorsCB.Text.Trim().Substring(3);
            DoctorStorage doctorStorage = new DoctorStorage();
            MedicineRevision.DoctorID = doctorStorage.GetIDByNameSurname(doctorSelected);
            MedicineRevision.RevisionDoctor = doctorStorage.GetOne(MedicineRevision.DoctorID);
            MedicineRevision.IsMedicineRevised = false;

            CheckUniquenessOfMedicineID();

            MedicineRevisionStorage medicineRevisionStorage = new MedicineRevisionStorage();
            medicineRevisionStorage.Save(MedicineRevision);

            NavigationService.Navigate(new MedicinesWindow());
        }

        private void CheckUniquenessOfMedicineID()
        {
            MedicineStorage medicineStorage = new MedicineStorage();
            ObservableCollection<Medicine> medicines = medicineStorage.GetAll();
            foreach (Medicine medicine in medicines)
            {
                if (medicine.ID.Equals(MedicineRevision.Medicine.ID))
                {
                    MessageBox.Show("Vec postoji lek sa unetom oznakom!");
                    return;
                }
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MedicinesWindow());
        }

        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MedicinesWindow());
        }
    }
}
