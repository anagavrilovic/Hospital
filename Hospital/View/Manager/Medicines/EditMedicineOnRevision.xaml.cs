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
    public partial class EditMedicineOnRevision : Page, INotifyPropertyChanged
    {
        private MedicineRevision _medicineOnRevision;
        public MedicineRevision MedicineOnRevision 
        { 
            get { return _medicineOnRevision; }
            set { _medicineOnRevision = value; OnPropertyChanged("MedicineOnRevision"); }
        }
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
            DoctorService doctorService = new DoctorService();
            ObservableCollection<string>  DoctorsNameSurname = new ObservableCollection<string>(doctorService.GetDoctorsIdNameSurname());
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
                return true;
            
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
            Ingredients.Add(ingredientToDelete.Name);
            allIngredientsList.Items.Refresh();
        }

        private void BtnSearchMouseDown(object sender, RoutedEventArgs e)
        {
            IngredientsCollection.Refresh();
        }

        private void SendAgainOnRevision(object sender, RoutedEventArgs e)
        {
            SaveUpdatedProperties();

            MedicineRevisionService service = new MedicineRevisionService();
            service.EditMedicine(MedicineOnRevision);

            NavigationService.Navigate(new MedicineRevisionWindow());
        }

        private void SaveUpdatedProperties()
        {
            nameTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            dosageTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            priceTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            quantityTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            doctorsCB.GetBindingExpression(ComboBox.SelectedItemProperty).UpdateSource();
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
                MedicineOnRevision.Medicine.AddIngredient(ingredient);
                ingredientsList.ItemsSource = MedicineOnRevision.Medicine.Ingredient;
                ingredientsList.Items.Refresh();
            }
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MedicineRevisionWindow());
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MedicineRevisionWindow());
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
