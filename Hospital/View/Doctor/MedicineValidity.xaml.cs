using Hospital.Model;
using Hospital.Services;
using Hospital.Services.DoctorServices;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Hospital.View.Doctor
{
    /// <summary>
    /// Interaction logic for ValidnostLeka.xaml
    /// </summary>
    public partial class MedicineValidity : Page,INotifyPropertyChanged
    {
        private MedicineRevisionServicePage service=new MedicineRevisionServicePage();
        private MedicineService medicineService = new MedicineService();
        private MedicineRevisionService medicineRevisionService = new MedicineRevisionService();
        private Model.Doctor doctor=new Model.Doctor();
        private ObservableCollection<MedicineRevision> medicineRevisions = new ObservableCollection<MedicineRevision>();
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
        public MedicineValidity(Model.Doctor doctor)
        {
            InitializeComponent();
            this.DataContext = this;
            this.doctor = doctor;
            medicineRevisions = service.SetRevisionList(doctor);
            SetIcons();
        }
        private void SetIcons()
        {
            var app = (App)Application.Current;
            if (app.DarkTheme)
            {
                okButton.Source = new BitmapImage(new Uri("pack://application:,,,/Icon/DoctorIcons/ok-16 (1).png", UriKind.Absolute));
                cancelButton.Source = new BitmapImage(new Uri("pack://application:,,,/Icon/DoctorIcons/x-mark-3-16 (1).png", UriKind.Absolute));
            }
            else
            {
                okButton.Source = new BitmapImage(new Uri("pack://application:,,,/Icon/Secretary/ok.png", UriKind.Absolute));
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
                    medicineRevisionService.Delete(((MedicineRevision)ListBoxRevisions.SelectedItem).Medicine.ID);
                    medicineService.SaveMedicine(((MedicineRevision)ListBoxRevisions.SelectedItem).Medicine);
                    MedicineRevisions.Remove(((MedicineRevision)ListBoxRevisions.SelectedItem));
                }
            }
        }

        private void RejectMedicine_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxRevisions.SelectedIndex != -1)
            {
                RejectedMedicineComment dialog = new RejectedMedicineComment(((MedicineRevision)ListBoxRevisions.SelectedItem), this);
                dialog.Show();
            }
        }
    }
}
