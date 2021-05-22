using Hospital.Model;
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
using System.Windows.Shapes;

namespace Hospital.View.Doctor
{
    /// <summary>
    /// Interaction logic for LekListBox.xaml
    /// </summary>
    public partial class MedicineTherapyWindow : Window, INotifyPropertyChanged
    {

        private MedicalRecord medicalRecord;
        MedicineStorage medicineStorage = new MedicineStorage();
        public event PropertyChangedEventHandler PropertyChanged;
        private TherapyPage parentWindow;

        ObservableCollection<Medicine> medics = new ObservableCollection<Medicine>();
        ObservableCollection<Medicine> Medics
        {
            get { return medics; }
            set
            {
                medics = value;
                OnPropertyChanged();
            }

        }

        private string medicineDescription;
        public string MedicineDescription
        {
            get { return medicineDescription; }
            set
            {
                medicineDescription = value;
                OnPropertyChanged();
            }

        }
        private ICollectionView medicsCollection;

        public ICollectionView MedicsCollection
        {
            get { return medicsCollection; }
            set { medicsCollection = value; }
        }

        private string daysForConsumption;
        public string DaysForConsumption
        {
            get { return daysForConsumption; }
            set
            {
                daysForConsumption = value;
                OnPropertyChanged();
            }
        }
        private string dailyIntake;
        public string DailyIntake
        {
            get { return dailyIntake; }
            set
            {
                dailyIntake = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
        public MedicineTherapyWindow(TherapyPage parentWindow,MedicalRecord medicalRecord)
        {
            InitializeComponent();
            this.DataContext = this;
            FillComboBoxes();
            FillProperties(medicalRecord);
            FilterAndSorterParameters();
            this.parentWindow = parentWindow;
        }

        private void FilterAndSorterParameters()
        {
            MedicsCollection = CollectionViewSource.GetDefaultView(Medics);
            MedicsCollection.Filter = filterMedics;
            ICollectionView view = GetSearch();
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Ascending));
        }

        private void FillProperties(MedicalRecord medicalRecord)
        {
            Medics = medicineStorage.GetAll();
            this.medicalRecord = medicalRecord;
            this.listBox.ItemsSource = Medics;
        }

        private void FillComboBoxes()
        {
            for (int i = 1; i < 10; i++)
            {
                dailyIntakeComboBox.Items.Add(i.ToString());
            }
            for (int i = 1; i < 60; i++)
            {
                daysConsumptionComboBox.Items.Add(i.ToString());
            }
            dailyIntakeComboBox.SelectedIndex = 2;
            daysConsumptionComboBox.SelectedIndex = 6;
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

        private void filterMedicine(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listBox.ItemsSource).Refresh();
        }

        public ICollectionView GetSearch()
        {
            return CollectionViewSource.GetDefaultView(Medics);
        }

        private void AddMedicineToTherapy(object sender, MouseButtonEventArgs e)
        {
            Medicine medicToBeAdded = ((Medicine)listBox.SelectedItem);
            MedicineTherapy medicineTherapy = FillMedicine(medicToBeAdded);
            if (!AlreadyAddedToTherapy(medicToBeAdded) && !AllergicToMedic(medicToBeAdded) && !AlergicToIngredients(medicToBeAdded))
            {
                    parentWindow.MedicineView.Add(medicToBeAdded);
                    parentWindow.Medics.Add(medicineTherapy);
                    this.Close();
            }
        }

        private bool AlergicToIngredients(Medicine medicToBeAdded)
        {
            foreach(string ingredient in medicalRecord.Allergen.IngredientNames)
            {
                foreach(Ingredient medicToBeAddedIngredients in medicToBeAdded.Ingredient)
                {
                    if (ingredient.Equals(medicToBeAddedIngredients.Name))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool AllergicToMedic(Medicine medicToBeAdded)
        {
            foreach (string medicineName in medicalRecord.Allergen.MedicineNames)
            {
                if (medicineName.Equals(medicToBeAdded.Name))
                {
                    ErrorBox errorBox=new ErrorBox("Pacijent je alergican na dati lek");
                    return true;
                }
            }
            return false;
        }


        private bool AlreadyAddedToTherapy(Medicine medicine)
        {
            foreach (MedicineTherapy medicineAddedToTherapy in parentWindow.Medics)
            {
                if (medicineAddedToTherapy.MedicineID.Equals(medicine.ID))
                {
                    ErrorBox errorBox = new ErrorBox("Vec ste dodali ovaj lek");
                    return true;
                }
            }

            return false;
        }

        private MedicineTherapy FillMedicine(Medicine medicToBeAdded)
        {
            MedicineTherapy medicineTherapy = new MedicineTherapy();
            medicineTherapy.MedicineID = medicToBeAdded.ID;
            medicineTherapy.DurationInDays = int.Parse(DaysForConsumption);
            medicineTherapy.TimesPerDay = int.Parse(DailyIntake);
            medicineTherapy.Description = MedicineDescription;
            return medicineTherapy;

        }

        private void BackToTherapy(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
