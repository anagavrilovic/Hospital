using Hospital.Commands.DoctorCommands;
using Hospital.Controller;
using Hospital.Controller.DoctorControllers;
using Hospital.DTO.DoctorDTO;
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
        private MedicineValidityController controller;
        private MedicineValidityDTO dTO;
        private Model.Doctor doctor;
        NavigationController navigationController;
        public MedicineValidityDTO DTO
        {
            get
            {
                return dTO;
            }
            set
            {
                    dTO = value;
                    OnPropertyChanged("DTO");
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
        public MedicineValidityViewModel(Model.Doctor doctor, NavigationController navigationController)
        {
            this.doctor = doctor;
            this.navigationController = navigationController;
            DTO = new MedicineValidityDTO();
            controller = new MedicineValidityController(DTO);
            DTO.MedicineRevisions = new ObservableCollection<MedicineRevision>();
            this.ConfirmMedicine = new RelayCommand(Execute_ConfirmMedicine, CanExecute_Command);
            this.RejectMedicine = new RelayCommand(Execute_RejectMedicine, CanExecute_Command);
            DTO.MedicineRevisions = new ObservableCollection<MedicineRevision>(controller.SetRevisionList(doctor));
            SetIcons();
        }
        private void SetIcons()
        {
            var app = (App)Application.Current;
            if (!app.DarkTheme)
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
            if (DTO.SelectedRevision != null)
            {
                ConfirmBox confirmBox = new ConfirmBox("Da li ste sigurni da odobravate lek?");
                if ((bool)confirmBox.ShowDialog())
                {
                    controller.DeleteMedicineRevision();
                    controller.SaveMedicine();
                    DTO.MedicineRevisions.Remove((DTO.SelectedRevision));
                }
            }
        }

        private void Execute_RejectMedicine(object sender)
        {
            if (DTO.SelectedRevision != null)
            {
                RejectedMedicineComment dialog = new RejectedMedicineComment(DTO.SelectedRevision,navigationController,doctor);
                dialog.Show();
            }
        }
    }
}
