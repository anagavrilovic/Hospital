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
    /// Interaction logic for ModifikacijaAlergena.xaml
    /// </summary>
    public partial class ModifikacijaAlergena : Window
    {
        private ObservableCollection<string> lekovi = new ObservableCollection<string>();
        private ObservableCollection<string> sastojci = new ObservableCollection<string>();
        private string ostalo;

        private MedicalRecord mr;
        private ObservableCollection<MedicalRecord> pacijenti;

        private List<string> stariLekovi = new List<string>();

        public List<string> StariLekovi
        {
            get { return stariLekovi; }
            set { stariLekovi = value; }
        }

        private List<string> stariSastojci = new List<string>();

        public List<string> StariSastojci
        {
            get { return stariSastojci; }
            set { stariSastojci = value; }
        }

        public ObservableCollection<MedicalRecord> Pacijenti
        {
            get { return pacijenti; }
            set { pacijenti = value; }
        }


        public MedicalRecord Mr
        {
            get { return mr; }
            set { mr = value; }
        }


        public string Ostalo
        {
            get { return ostalo; }
            set { ostalo = value; }
        }

        public ObservableCollection<string> Sastojci
        {
            get { return sastojci; }
            set { sastojci = value; }
        }

        public ObservableCollection<string> Lekovi
        {
            get { return lekovi; }
            set { lekovi = value; }
        }

        private ICollectionView lekoviCollection;

        public ICollectionView LekoviCollection
        {
            get { return lekoviCollection; }
            set { lekoviCollection = value; }
        }

        private ICollectionView sastojciCollection;

        public ICollectionView SastojciCollection
        {
            get { return sastojciCollection; }
            set { sastojciCollection = value; }
        }

        public ModifikacijaAlergena(MedicalRecord mr, ObservableCollection<MedicalRecord> p)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Mr = mr;
            this.Pacijenti = p;
            this.ReadDrugsAndIngredients();

            foreach (string lek in Mr.Allergen.MedicineNames)
                StariLekovi.Add(lek);

            foreach (string sastojak in Mr.Allergen.IngredientNames)
                StariSastojci.Add(sastojak);

        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            OstaloTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            MedicalRecordStorage mrs = new MedicalRecordStorage();
            mrs.DoSerialization(Pacijenti);
            this.Close();
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            foreach(string lek in Mr.Allergen.MedicineNames.ToList<string>())
                if (!StariLekovi.Contains(lek))
                    Mr.Allergen.MedicineNames.Remove(lek);

            foreach (string sastojak in Mr.Allergen.IngredientNames.ToList<string>())
                if (!StariSastojci.Contains(sastojak))
                    Mr.Allergen.IngredientNames.Remove(sastojak);

            this.Close();
        }

        private void ReadDrugsAndIngredients()
        {
            string[] lines = File.ReadAllLines("..\\..\\Files\\drugs.txt");
            foreach (string line in lines)
                Lekovi.Add(line);

            LekoviLista.ItemsSource = Lekovi;
            LekoviCollection = CollectionViewSource.GetDefaultView(LekoviLista.ItemsSource);
            LekoviCollection.Filter = CustomFilterLekovi;

            string[] lines2 = File.ReadAllLines("..\\..\\Files\\ingredients.txt");
            foreach (string line in lines2)
                Sastojci.Add(line);

            SastojciLista.ItemsSource = Sastojci;
            SastojciCollection = CollectionViewSource.GetDefaultView(SastojciLista.ItemsSource);
            SastojciCollection.Filter = CustomFilterSastojci;
        }

        private bool CustomFilterLekovi(object obj)
        {
            if (string.IsNullOrEmpty(LekoviFilter.Text))
            {
                return true;
            }
            else
            {
                return (obj.ToString().IndexOf(LekoviFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private bool CustomFilterSastojci(object obj)
        {
            if (string.IsNullOrEmpty(SastojciFilter.Text))
            {
                return true;
            }
            else
            {
                return (obj.ToString().IndexOf(SastojciFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private void LekoviFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(LekoviLista.ItemsSource).Refresh();
        }

        private void SastojciFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(SastojciLista.ItemsSource).Refresh();
        }

        private void BtnPlusLekoviClick(object sender, RoutedEventArgs e)
        {
            string selektovaniLek = (string)LekoviLista.SelectedItem;
            if (!Mr.Allergen.MedicineNames.Contains(selektovaniLek))
                Mr.Allergen.MedicineNames.Add(selektovaniLek);
        }

        private void BtnPlusSastojciClick(object sender, RoutedEventArgs e)
        {
            string selektovaniSastojak = (string)SastojciLista.SelectedItem;
            if (!Mr.Allergen.IngredientNames.Contains(selektovaniSastojak))
                Mr.Allergen.IngredientNames.Add(selektovaniSastojak);
        }

        private void LekoviUkloni(object sender, RoutedEventArgs e)
        {
            Mr.Allergen.MedicineNames.Remove((string)DodatiLekovi.SelectedItem);
        }

        private void SastojciUkloni(object sender, RoutedEventArgs e)
        {
            Mr.Allergen.IngredientNames.Remove((string)DodatiSastojci.SelectedItem);
        }
    }
}
