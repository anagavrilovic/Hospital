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

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for Terapija.xaml
    /// </summary>
    public partial class Terapija : Page, INotifyPropertyChanged
    {
        private ObservableCollection<Medicine> medics = new ObservableCollection<Medicine>();
        public int dani;
        private MedicalRecordStorage medicineStorage = new MedicalRecordStorage();
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Medicine> Medics
        {
            get { return medics; }
            set
            {
                medics = value;
                OnPropertyChanged();
            }
        }
        private string therapyDescription;
        public string TherapyDescription
        {
            get { return therapyDescription; }
            set
            {
                therapyDescription = value;
                OnPropertyChanged();
            }
        }
        private string therapyTitle;
        public string TherapyTitle
        {
            get { return therapyTitle; }
            set
            {
                therapyTitle = value;
                OnPropertyChanged();
            }
        }

        public Terapija()
        {
            InitializeComponent();
            this.DataContext = this;
            medicineBox.ItemsSource = Medics;
        }

        private void AddMedicine(object sender, RoutedEventArgs e)
        {
            MedicalRecord medicalRecord = medicineStorage.GetByPatientID(((Doctor_Examination)Window.GetWindow(this)).appointment.IDpatient);
            LekListBox AddMedicineWindow = new LekListBox(this,medicalRecord);
            AddMedicineWindow.Owner = Window.GetWindow(this);
            AddMedicineWindow.ShowDialog();
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {

                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        private void SaveTherapyInExamination(object sender, RoutedEventArgs e)
        {
            Therapy therapy = new Therapy();
            FillTherapy(therapy);
            ((Doctor_Examination)Window.GetWindow(this)).Pregled.therapy = therapy;
            SetTabColors();
        }

        private void SetTabColors()
        {
            ((Doctor_Examination)Window.GetWindow(this)).tab.SelectedIndex = 4;
            ((Doctor_Examination)Window.GetWindow(this)).Terapija.IsEnabled = false;
            ((Doctor_Examination)Window.GetWindow(this)).TerapijaLabela.Foreground = Brushes.Black;
            ((Doctor_Examination)Window.GetWindow(this)).DiagnozaLabela.Foreground = Brushes.White;
            ((Doctor_Examination)Window.GetWindow(this)).Dijagnoza.IsEnabled = true;
        }

        private void FillTherapy(Therapy therapy)
        {
            foreach (Medicine m in Medics)
            {
                therapy.AddMedicine(m);
            }
            therapy.description = TherapyDescription;
            therapy.name = TherapyTitle;
        }

        private void RemoveMedicineFromList(object sender, RoutedEventArgs e)
        {
            if (medicineBox.SelectedItem != null)
            {
               Medics.Remove((Medicine)medicineBox.SelectedItem);
            }
        }
    }
}
