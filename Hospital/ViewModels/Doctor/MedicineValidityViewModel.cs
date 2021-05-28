using Hospital.Commands.DoctorCommands;
using Hospital.Model;
using Hospital.Services;
using Hospital.View.Doctor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Hospital.ViewModels.Doctor
{

   public class MedicineValidityViewModel : ViewModel
    {
        private MedicineService medicineService = new MedicineService();
        private MedicineRevisionService medicineRevisionService = new MedicineRevisionService();
        private ObservableCollection<MedicineRevision> medicineRevisions = new ObservableCollection<MedicineRevision>();
        private ObservableCollection<string> ingredients = new ObservableCollection<string>();
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
        private RelayCommand rejectMedicine;
        public RelayCommand RejectMedicine
        {
            get { return rejectMedicine; }
            set
            {
                rejectMedicine = value;
            }
        }
        private RelayCommand confirmMedicine;
        public RelayCommand ConfirmMedicine
        {
            get { return confirmMedicine; }
            set
            {
                confirmMedicine = value;
            }
        }
        private MedicineRevision selectedRevision = new MedicineRevision();
        public MedicineRevision SelectedRevision
        {
            get
            {
                return selectedRevision;
            }
            set
            {
                if (value != selectedRevision)
                {
                    selectedRevision = value;
                    OnPropertyChanged("SelectedRevision");
                }
            }
        }
        private BitmapImage okImage;
        public BitmapImage OkImage
        {
            get { return this.okImage; }
            set { this.okImage = value; this.OnPropertyChanged("OkImage"); }
        }
        private BitmapImage cancelImage;
        public BitmapImage CancelImage
        {
            get { return this.cancelImage; }
            set { this.cancelImage = value; this.OnPropertyChanged("OkImage"); }
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
        public MedicineValidityViewModel(Model.Doctor doctor)
        {
            this.ConfirmMedicine = new RelayCommand(Execute_ConfirmMedicine, CanExecute_Command);
            this.RejectMedicine = new RelayCommand(Execute_RejectMedicine, CanExecute_Command);
            medicineRevisions = new ObservableCollection<MedicineRevision>(medicineRevisionService.SetRevisionList(doctor));
            SetIcons();
        }
        private void SetIcons()
        {
            var app = (App)Application.Current;
            if (app.DarkTheme)
            {
                OkImage = new BitmapImage(new Uri("pack://application:,,,/Icon/DoctorIcons/ok_pink.png", UriKind.Absolute));
                CancelImage = new BitmapImage(new Uri("pack://application:,,,/Icon/DoctorIcons/cancel_pink.png", UriKind.Absolute));
            }
            else
            {
                OkImage = new BitmapImage(new Uri("pack://application:,,,/Icon/DoctorIcons/ok.png", UriKind.Absolute));
                CancelImage = new BitmapImage(new Uri("pack://application:,,,/Icon/DoctorIcons/cancel.png", UriKind.Absolute));
            }
        }
        private bool CanExecute_Command(object obj)
        {
            return true;
        }

        private void Execute_ConfirmMedicine(object sender)
        {
            if (selectedRevision != null)
            {
                ConfirmBox confirmBox = new ConfirmBox("Da li ste sigurni da odobravate lek?");
                if ((bool)confirmBox.ShowDialog())
                {
                    medicineRevisionService.Delete((selectedRevision).Medicine.ID);
                    medicineService.SaveMedicine((selectedRevision).Medicine);
                    MedicineRevisions.Remove((selectedRevision));
                }
            }
        }

        private void Execute_RejectMedicine(object sender)
        {
            if (selectedRevision != null)
            {
                RejectedMedicineComment dialog = new RejectedMedicineComment(selectedRevision);
                dialog.Show();
            }
        }
    }
}
