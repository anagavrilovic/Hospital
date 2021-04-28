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
    public partial class Lekovi : Page,INotifyPropertyChanged
    {
        private MedicineStorage mStorage = new MedicineStorage();
        private Medicine lek=new Medicine();
        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }
        public Medicine Lek
        {
            get { return lek; }
            set
            {
                lek = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Medicine> lekoviZaPrikaz;
        public ObservableCollection<Medicine> LekoviZaPrikaz
        {
            get { return lekoviZaPrikaz; }
            set
            {
                lekoviZaPrikaz = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Medicine> zamenskiLekovi=new ObservableCollection<Medicine>();
        public ObservableCollection<Medicine> ZamenskiLekovi
        {
            get { return zamenskiLekovi; }
            set
            {
                zamenskiLekovi = value;
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

        private ICollectionView lekoviCollection;

        public ICollectionView LekoviCollection
        {
            get { return lekoviCollection; }
            set { lekoviCollection = value; }
        }

        private ICollectionView ingredientCollection;

        public ICollectionView IngredientCollection
        {
            get { return ingredientCollection; }
            set { ingredientCollection = value; }
        }

        public Lekovi()
        {
            InitializeComponent();
            this.DataContext = this;
            SetProperites();
            AddFilterAndSorter();
        }

        private void SetProperites()
        {
            sastojci.ItemsSource = Lek.Ingredient;
            lekoviZaPrikaz = mStorage.GetAll();
            listBox.ItemsSource = lekoviZaPrikaz;
            SetReplacementMedicine();
            text = "Lek: ";
            ReadIngredients();
            listIngredients.ItemsSource = Ingredients;
        }

        private void ReadIngredients()
        {
            string[] lines2 = File.ReadAllLines("..\\..\\Files\\ingredients.txt");
            foreach (string line in lines2)
            {
                Ingredient ingredient = new Ingredient();
                ingredient.Name = line;
                Ingredients.Add(ingredient);
            }
        }

        private void AddFilterAndSorter()
        {
            LekoviCollection = CollectionViewSource.GetDefaultView(LekoviZaPrikaz);
            IngredientCollection = CollectionViewSource.GetDefaultView(Ingredients);
            IngredientCollection.Filter = filterIngredients;
            LekoviCollection.Filter = filterMedics;
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
            if (Lek != null)
            {
                ZamenskiLekovi.Clear();
                foreach (string medicID in Lek.ReplacementMedicineIDs)
                {
                    ZamenskiLekovi.Add(mStorage.GetOne(medicID));
                }
                listaZamena.ItemsSource = ZamenskiLekovi;
            }
        }

        private void LekoviFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listBox.ItemsSource).Refresh();
        }


        public ICollectionView GetPretraga()
        {
            return CollectionViewSource.GetDefaultView(LekoviZaPrikaz);
        }

        private void SelctedMedicChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Medicine)listBox.SelectedItem != null)
            {
                if (sacuvaj.IsEnabled.Equals(false))
                {
                    Lek = (Medicine)listBox.SelectedItem;
                    SetReplacementMedicine();
                    sastojci.ItemsSource = Lek.Ingredient;
                    sastojci.Items.Refresh();
                }
            }
        }

        private void EditMedic(object sender, RoutedEventArgs e)
        {
            listaZamena.IsEnabled = true;
            doza.IsEnabled = true;
            dodajZamenu.IsEnabled = true;
            izbaciZamenu.IsEnabled = true;
            sacuvaj.IsEnabled = true;
            izbaciSastojak.IsEnabled = true;
            dodajSastojak.IsEnabled = true;
            sastojci.IsEnabled = true;
            izmeni.IsEnabled = false;
        }

        private void AddIngridient(object sender, RoutedEventArgs e)
        {
            if (listIngredients.SelectedItem !=null)
            {
                Ingredient i =(Ingredient)listIngredients.SelectedItem;
                Lek.AddIngredient(i); 
                sastojci.ItemsSource = Lek.Ingredient;
                sastojci.Items.Refresh();
            }
        }

        private void RemoveIngridient(object sender, RoutedEventArgs e)
        {
            if (sastojci.SelectedItem != null)
            {
                Lek.RemoveIngredient((Ingredient)sastojci.SelectedItem);
                sastojci.ItemsSource = Lek.Ingredient;
                sastojci.Items.Refresh();
            }
        }

        private void AddReplacement(object sender, RoutedEventArgs e)
        {
            if (!Lek.ID.Equals(((Medicine)listBox.SelectedItem).ID))
            {
                Lek.AddMedicineID(((Medicine)listBox.SelectedItem).ID);
                ZamenskiLekovi.Add((Medicine)listBox.SelectedItem);
                listaZamena.ItemsSource = ZamenskiLekovi;
                listaZamena.Items.Refresh();
            }
        }

        private void RemoveReplacement(object sender, RoutedEventArgs e)
        {
            Lek.RemoveMedicineID(((Medicine)listBox.SelectedItem).ID);
            ZamenskiLekovi.Remove((Medicine)listBox.SelectedItem);
            listaZamena.ItemsSource = ZamenskiLekovi;
            listaZamena.Items.Refresh();
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {

                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            foreach(Medicine medicine in ZamenskiLekovi)
            {
                Lek.AddMedicineID(medicine.ID);
            }
            mStorage.EditMedicine(Lek);
            SavedChangesVisibilities();
        }

        private void SavedChangesVisibilities()
        {
            listaZamena.IsEnabled = false;
            doza.IsEnabled = false;
            dodajZamenu.IsEnabled = false;
            izbaciZamenu.IsEnabled = false;
            sacuvaj.IsEnabled = false;
            izbaciSastojak.IsEnabled = false;
            dodajSastojak.IsEnabled = false;
            sastojci.IsEnabled = false;
            izmeni.IsEnabled = true;
        }

        private void SearchIngredientFilter(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listIngredients.ItemsSource).Refresh();
        }
    }
}
