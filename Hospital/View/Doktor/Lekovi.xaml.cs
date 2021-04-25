using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private string sastojak;
        public string Sastojak
        {
            get { return sastojak; }
            set
            {
                sastojak = value;
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

        private ICollectionView lekoviCollection;

        public ICollectionView LekoviCollection
        {
            get { return lekoviCollection; }
            set { lekoviCollection = value; }
        }

        public Lekovi()
        {
            InitializeComponent();
            this.DataContext = this;
            SetProperites();
        }

        private void SetProperites()
        {
            sastojci.ItemsSource = Lek.Ingredient;
            lekoviZaPrikaz = mStorage.GetAll();
            listBox.ItemsSource = lekoviZaPrikaz;
            SetReplacementMedicine();
            text = "Lek: ";
            AddFilterAndSorter();
        }

        private void AddFilterAndSorter()
        {
            LekoviCollection = CollectionViewSource.GetDefaultView(LekoviZaPrikaz);
            LekoviCollection.Filter = filterMedics;
            ICollectionView view = GetPretraga();
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Ascending));
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
                if (sacuvaj.Visibility.Equals(Visibility.Hidden))
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
            dodajZamenu.Visibility = Visibility.Visible;
            izbaciZamenu.Visibility = Visibility.Visible;
            sacuvaj.Visibility = Visibility.Visible;
            izmeni.Visibility = Visibility.Hidden;
            listaZamena.IsEnabled = true;
            doza.IsEnabled = true;
            izbaciSastojak.Visibility = Visibility.Visible;
            dodajSastojak.Visibility = Visibility.Visible;
            dodajZamenu.IsEnabled = true;
            izbaciZamenu.IsEnabled = true;
            sacuvaj.IsEnabled = true;
            izbaciSastojak.IsEnabled = true;
            dodajSastojak.IsEnabled = true;
            sastojci.IsEnabled = true;
            txtSastojak.Visibility = Visibility.Visible;
            sastojakLabela.Visibility = Visibility.Hidden;
        }

        private void AddIngridient(object sender, RoutedEventArgs e)
        {
            if (!Sastojak.Equals(""))
            {
                Ingredient i = new Ingredient();
                i.Name = Sastojak;
                Lek.AddIngredient(i); 
                Sastojak = "";
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
            izmeni.Visibility = Visibility.Visible;
            sacuvaj.Visibility = Visibility.Hidden;
            dodajZamenu.Visibility = Visibility.Hidden;
            izbaciZamenu.Visibility = Visibility.Hidden;
            listaZamena.IsEnabled = false;
            doza.IsEnabled = false;
            izbaciSastojak.Visibility = Visibility.Hidden;
            dodajSastojak.Visibility = Visibility.Hidden;
            dodajZamenu.IsEnabled = false;
            izbaciZamenu.IsEnabled = false;
            sacuvaj.IsEnabled = false;
            izbaciSastojak.IsEnabled = false;
            dodajSastojak.IsEnabled = false;
            sastojci.IsEnabled = false;
            sastojakLabela.Visibility = Visibility.Hidden;
            txtSastojak.Visibility = Visibility.Hidden;
        }
    }
}
