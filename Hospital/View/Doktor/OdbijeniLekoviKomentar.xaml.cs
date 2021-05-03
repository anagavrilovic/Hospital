using Hospital.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Hospital.View.Doktor
{
    /// <summary>
    /// Interaction logic for OdbijeniLekoviKomentar.xaml
    /// </summary>
    public partial class OdbijeniLekoviKomentar : Window,INotifyPropertyChanged
    {
        private MedicineRevision medicineRevision = new MedicineRevision();
        private MedicineRevisionStorage medicineRevisionStorage = new MedicineRevisionStorage();
        private ValidnostLeka validateMedicine;
        public event PropertyChangedEventHandler PropertyChanged;
        public MedicineRevision MedicineRevision
        {
            get
            {
                return medicineRevision;
            }
            set
            {
                if (value != medicineRevision)
                {
                    medicineRevision = value;
                    OnPropertyChanged("MedicineRevision");
                }
            }
        }
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public OdbijeniLekoviKomentar(MedicineRevision medicineRevision, ValidnostLeka validateMedicine)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 3 / 4);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 1 / 2);
            this.medicineRevision = medicineRevision;
            this.validateMedicine = validateMedicine;
        }

        private void Nazad(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveRejection_Click(object sender, RoutedEventArgs e)
        {
            MedicineRevision.IsMedicineRevised = true;
            validateMedicine.MedicineRevisions.Remove(MedicineRevision);
            medicineRevisionStorage.EditMedicine(MedicineRevision);
            this.Close();
        }
    }
}
