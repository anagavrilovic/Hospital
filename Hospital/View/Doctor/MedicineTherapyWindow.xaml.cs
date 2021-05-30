using Hospital.Controller.DoctorControllers;
using Hospital.DTO.DoctorDTO;
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
        private MedicineTherapyController controller;
        private MedicalRecord medicalRecord;
        private MedicineTherapyDTO dTO;
        public MedicineTherapyDTO DTO
        {
            get { return dTO; }
            set
            {
                dTO = value;
                OnPropertyChanged("DTO");
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;
        private TherapyPage parentWindow;
        private ICollectionView medicsCollection;

        public ICollectionView MedicsCollection
        {
            get { return medicsCollection; }
            set { medicsCollection = value; }
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
            MedicsCollection = CollectionViewSource.GetDefaultView(DTO.Medics);
            MedicsCollection.Filter = filterMedics;
            ICollectionView view = GetSearch();
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Ascending));
        }

        private void FillProperties(MedicalRecord medicalRecord)
        {
            DTO = new MedicineTherapyDTO();
            controller = new MedicineTherapyController(DTO);
            DTO.Medics = new ObservableCollection<Medicine>(controller.GetAllMedics());
            this.medicalRecord = medicalRecord;
            this.listBox.ItemsSource = DTO.Medics;
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
            return CollectionViewSource.GetDefaultView(DTO.Medics);
        }

        private void AddMedicineToTherapy(object sender, MouseButtonEventArgs e)
        {
            Medicine medicToBeAdded = ((Medicine)listBox.SelectedItem);
            MedicineTherapy medicineTherapy = FillMedicine(medicToBeAdded);
            if (!AlreadyAddedToTherapy(medicToBeAdded) && !controller.AllergicToMedic(medicToBeAdded,medicalRecord) && !controller.AlergicToIngredients(medicToBeAdded,medicalRecord))
            {
                    parentWindow.DTO.MedicineView.Add(medicToBeAdded);
                    parentWindow.DTO.Medics.Add(medicineTherapy);
                    this.Close();
            }
        }

        private bool AlreadyAddedToTherapy(Medicine medicine)
        {
            foreach (MedicineTherapy medicineAddedToTherapy in parentWindow.DTO.Medics)
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
            medicineTherapy.DurationInDays = int.Parse(DTO.DaysForConsumption);
            medicineTherapy.TimesPerDay = int.Parse(DTO.DailyIntake);
            medicineTherapy.Description = DTO.MedicineDescription;
            return medicineTherapy;

        }

        private void BackToTherapy(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
