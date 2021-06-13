using Hospital.Factory;
using Hospital.Model;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for PatientTherapysMedications.xaml
    /// </summary>
    public partial class PatientTherapysMedications : Page
    {
        public ObservableCollection<MedicineTherapy> MedicineTherapies
        {
            get;
            set;
        }
       
        private MedicineService medicineService=new MedicineService(new MedicineFileFactory(), new MedicalRecordFileFactory());
        
        public PatientTherapysMedications(List<MedicineTherapy> medicineTherapies)
        {
            InitializeComponent();
            this.DataContext = this;
            this.MedicineTherapies = new ObservableCollection<MedicineTherapy>(medicineTherapies);
            foreach(MedicineTherapy medTer in medicineTherapies)
            {
                medTer.Medicine=medicineService.GetById(medTer.MedicineID);
            }
            dataGridApp.SelectedIndex = 0;

            dataGridApp.Focus();
        }

        private void myTestKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                MedicineTherapy selectedItem = (MedicineTherapy)dataGridApp.SelectedItem;
                PatientMedication patientMedication = new PatientMedication(selectedItem);
                this.NavigationService.Navigate(patientMedication);

            }

            if (e.Key == Key.Escape)
            {
                this.NavigationService.GoBack();
            }
        }
    }
}
