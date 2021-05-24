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
    public partial class ModifikacijaAlergena : Page
    {  
        public MedicalRecord PatientsRecord { get; set; }

        public ObservableCollection<string> AllMedicines { get; set; }
        public ObservableCollection<string> AllIngredients { get; set; }
        public ICollectionView MedicineCollection { get; set; }
        public ICollectionView IngredientsCollection { get; set; }

        public string SelectedMedicine { get; set; }
        public string SelectedIngredient { get; set; }

        private MedicalRecordService MedicalRecordService;
        private MedicineService MedicineService;


        public ModifikacijaAlergena(MedicalRecord selectedRecord)
        {
            InitializeComponent();
            this.DataContext = this;
            InitializeEmptyProperties();
            ReadAllMedicines();
            ReadAllIngredients();
            this.PatientsRecord = selectedRecord;
        }

        private void InitializeEmptyProperties()
        {
            MedicalRecordService = new MedicalRecordService();
            MedicineService = new MedicineService();
        }

        private void ReadAllMedicines()
        {
            AllMedicines = new ObservableCollection<string>(MedicineService.GetAllMedicines());

            MedicineCollection = CollectionViewSource.GetDefaultView(AllMedicines);
            MedicineCollection.Filter = CustomFilterLekovi;
        }

        private void ReadAllIngredients()
        {
            AllIngredients = new ObservableCollection<string>(MedicineService.GetAllIngredients());

            IngredientsCollection = CollectionViewSource.GetDefaultView(AllIngredients);
            IngredientsCollection.Filter = CustomFilterSastojci;
        }

        private void BtnPotvrdiClick(object sender, RoutedEventArgs e)
        {
            MedicalRecordService.UpdateMedicalRecord(PatientsRecord);
            NavigationService.Navigate(new PrikazPacijenata());
        }

        private void BtnOtkaziClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PrikazPacijenata());
        }

        private bool CustomFilterLekovi(object obj)
        {
            if (string.IsNullOrEmpty(LekoviFilter.Text))
                return true;
            else
                return (obj.ToString().IndexOf(LekoviFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private bool CustomFilterSastojci(object obj)
        {
            if (string.IsNullOrEmpty(SastojciFilter.Text))
                return true;
            else
                return (obj.ToString().IndexOf(SastojciFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
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
            if (SelectedMedicine != null)
                if(!PatientsRecord.Allergen.MedicineNames.Contains(SelectedMedicine))
                    PatientsRecord.Allergen.MedicineNames.Add(SelectedMedicine);
        }

        private void BtnPlusSastojciClick(object sender, RoutedEventArgs e)
        {
            if (SelectedIngredient != null)
                if(!PatientsRecord.Allergen.IngredientNames.Contains(SelectedIngredient))
                    PatientsRecord.Allergen.IngredientNames.Add(SelectedIngredient);
        }

        private void LekoviUkloni(object sender, RoutedEventArgs e)
        {
            PatientsRecord.Allergen.MedicineNames.Remove((string)DodatiLekovi.SelectedItem);
        }

        private void SastojciUkloni(object sender, RoutedEventArgs e)
        {
            PatientsRecord.Allergen.IngredientNames.Remove((string)DodatiSastojci.SelectedItem);
        }
    }
}
