using Hospital.Model;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Hospital.View
{
    public partial class AddMedicine : Page, INotifyPropertyChanged
    {
        private MedicineRevision _medicineRevision;
        public MedicineRevision MedicineRevision
        {
            get { return _medicineRevision; }
            set { _medicineRevision = value; OnPropertyChanged("MedicineRevision"); }
        }
        public List<string> Ingredients { get; set; }
        private string _searchCriterion;
        public ICollectionView IngredientsCollection { get; set; }

        public AddMedicine()
        {
            InitializeComponent();

            MedicineRevision = new MedicineRevision();
            MedicineRevision.Medicine = new Medicine();
            InitializeComboBoxItems();
            InitializeIngredientsListBox();
            IngredientsCollection = CollectionViewSource.GetDefaultView(Ingredients);

            this.DataContext = this;
        }

        private void InitializeComboBoxItems()
        {
            DoctorService doctorService = new DoctorService();
            doctorsCB.ItemsSource  = new ObservableCollection<string>(doctorService.GetDoctorsIdNameSurname());
        }

        private void InitializeIngredientsListBox()
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
            Ingredients.Add(ingredientToDelete.Name);
            allIngredientsList.Items.Refresh();
        }

        private void BtnSearchMouseDown(object sender, RoutedEventArgs e)
        {
            IngredientsCollection.Refresh();
        }

        private void SendOnRevision(object sender, RoutedEventArgs e)
        {
            InitializeMedicine();
            MedicineService medicineService = new MedicineService();

            if (!medicineService.IsMedicineIDUnique(MedicineRevision.Medicine.ID))
            {
                Hospital.View.Manager.MessageWindow message = new Hospital.View.Manager.MessageWindow("Vec postoji lek sa unetom oznakom!");
                message.Show();
                return;
            }
            MedicineRevisionService medicineRevisionService = new MedicineRevisionService();
            medicineRevisionService.Save(MedicineRevision);

            NavigationService.Navigate(new MedicinesWindow());
        }


        private void InitializeMedicine()
        {
            string doctorSelected = doctorsCB.Text.Trim().Split("-".ToCharArray())[0];
            DoctorService doctorService = new DoctorService();
            MedicineRevision.DoctorID = doctorSelected;
            MedicineRevision.RevisionDoctor = doctorService.GetDoctorById(doctorSelected);
            MedicineRevision.IsMedicineRevised = false;
        }

        private Point startPoint;

        private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void ListBox_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                ListBox listBox = sender as ListBox;
                ListBoxItem listBoxItem =
                    FindAncestor<ListBoxItem>((DependencyObject)e.OriginalSource);

                if (listBoxItem == null) 
                    return;

                Ingredient ingredient = new Ingredient();         
                string ingredientName = (string)listBox.ItemContainerGenerator.ItemFromContainer(listBoxItem);
                ingredient.Name = ingredientName;

                DataObject dragData = new DataObject("myFormat", ingredient);
                DragDrop.DoDragDrop(listBoxItem, dragData, DragDropEffects.Move);
            }
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                    return (T)current;
                
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void ListBox_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || e.Source == sender)
                e.Effects = DragDropEffects.None;           
        }

        private void ListBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Ingredient ingredient = e.Data.GetData("myFormat") as Ingredient;
                Ingredients.Remove(ingredient.Name);
                allIngredientsList.Items.Refresh();
                MedicineRevision.Medicine.AddIngredient(ingredient);
                ingredientsList.ItemsSource = MedicineRevision.Medicine.Ingredient;
                ingredientsList.Items.Refresh();
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
