﻿using Hospital.Model;
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

        private MedicineRevision _medicineRevision;
        public MedicineRevision MedicineRevision
        {
            get => _medicineRevision;
            set
            {
                _medicineRevision = value;
                OnPropertyChanged("MedicineRevision");
            }
        }

        private ObservableCollection<string> _doctorsNameSurname;
        public ObservableCollection<string> DoctorsNameSurname
        {
            get => _doctorsNameSurname;
            set
            {
                _doctorsNameSurname = value;
                OnPropertyChanged("DoctorsNameSurname");
            }
        }

        private MedicineRevisionStorage _medicineRevisionStorage;
        public List<string> Ingredients { get; set; }
        private string _searchCriterion;
        public ICollectionView IngredientsCollection { get; set; }

        public AddMedicine()
        {
            InitializeComponent();

            _medicineRevisionStorage = new MedicineRevisionStorage();
            MedicineRevision = new MedicineRevision();
            MedicineRevision.Medicine = new Medicine();
            InitializeComboBoxItems();
            InitializeIngredientsListBox();
            IngredientsCollection = CollectionViewSource.GetDefaultView(Ingredients);

            this.DataContext = this;
        }

        private void InitializeComboBoxItems()
        {
            DoctorsNameSurname = new ObservableCollection<string>();
            DoctorStorage doctorStorage = new DoctorStorage();
            foreach (Hospital.Model.Doctor doctor in doctorStorage.GetAll())
            {
                DoctorsNameSurname.Add(doctor.ToString());
            }
        }

        private void InitializeIngredientsListBox()
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
            IngredientsCollection.Refresh();
        }

        private void SendOnRevision(object sender, RoutedEventArgs e)
        {
            InitializeMedicine();

            if (!IsMedicineIDUnique())
                return;

            _medicineRevisionStorage.Save(MedicineRevision);

            NavigationService.Navigate(new MedicinesWindow());
        }

        private bool IsMedicineIDUnique()
        {
            MedicineStorage medicineStorage = new MedicineStorage();
            ObservableCollection<Medicine> medicines = medicineStorage.GetAll();
            foreach (Medicine medicine in medicines)
            {
                if (medicine.ID.Equals(MedicineRevision.Medicine.ID))
                {
                    MessageBox.Show("Vec postoji lek sa unetom oznakom!");
                    return false;
                }
            }
            return true;
        }

        private void InitializeMedicine()
        {
            string doctorSelected = doctorsCB.Text.Trim().Substring(3);
            DoctorStorage doctorStorage = new DoctorStorage();
            MedicineRevision.DoctorID = doctorStorage.GetIDByNameSurname(doctorSelected);
            MedicineRevision.RevisionDoctor = doctorStorage.GetOne(MedicineRevision.DoctorID);
            MedicineRevision.IsMedicineRevised = false;
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
