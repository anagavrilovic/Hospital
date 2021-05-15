using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View.Doktor
{
    /// <summary>
    /// Interaction logic for ValidnostLeka.xaml
    /// </summary>
    public partial class ValidnostLeka : Page,INotifyPropertyChanged
    {
        private Model.Doctor doctor=new Model.Doctor();
        private ObservableCollection<MedicineRevision> medicineRevisions = new ObservableCollection<MedicineRevision>();
        private MedicineRevisionStorage medicineRevisionStorage = new MedicineRevisionStorage();
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<string> ingredients=new ObservableCollection<string>();
        public ObservableCollection<string> Ingredients
        {
            get
            {
                return ingredients;
            }
            set
            {
                if (value != ingredients)
                {
                    ingredients = value;
                    OnPropertyChanged("MedicineRevisions");
                }
            }
        }
        public ObservableCollection<MedicineRevision> MedicineRevisions
        {
            get
            {
                return medicineRevisions;
            }
            set
            {
                if (value != medicineRevisions)
                {
                    medicineRevisions = value;
                    OnPropertyChanged("MedicineRevisions");
                }
            }
        }
        public ValidnostLeka(Model.Doctor doctor)
        {
            InitializeComponent();
            this.DataContext = this;
            this.doctor = doctor;
            initProperties();
        }

        private void initProperties()
        {
            foreach(MedicineRevision mRevision in medicineRevisionStorage.GetAll())
            {
                if (mRevision.DoctorID.Equals(doctor.PersonalID) && !mRevision.IsMedicineRevised)
                {
                    medicineRevisions.Add(mRevision);   
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

        private void ConfirmMedicine_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxRevisions.SelectedIndex != -1)
            {
                ConfirmBox confirmBox = new ConfirmBox("Da li ste sigurni da odobravate lek?");
                if ((bool)confirmBox.ShowDialog())
                {
                    medicineRevisionStorage.Delete(((MedicineRevision)ListBoxRevisions.SelectedItem).Medicine.ID);
                    MedicineStorage medicineStorage = new MedicineStorage();
                    medicineStorage.Save(((MedicineRevision)ListBoxRevisions.SelectedItem).Medicine);
                    MedicineRevisions.Remove(((MedicineRevision)ListBoxRevisions.SelectedItem));
                }
            }
        }

        private void RejectMedicine_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxRevisions.SelectedIndex != -1)
            {
                OdbijeniLekoviKomentar dialog = new OdbijeniLekoviKomentar(((MedicineRevision)ListBoxRevisions.SelectedItem), this);
                dialog.Show();
            }
        }
    }
}
