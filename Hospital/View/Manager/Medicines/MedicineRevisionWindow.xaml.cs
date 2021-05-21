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
using System.Windows.Shapes;

namespace Hospital.View
{
    public partial class MedicineRevisionWindow : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private MedicineRevisionStorage _medicineRevisionStorage;

        private ObservableCollection<MedicineRevision> medicinesOnRevision;
        public ObservableCollection<MedicineRevision> MedicinesOnRevision
        {
            get => medicinesOnRevision;
            set
            {
                medicinesOnRevision = value;
                OnPropertyChanged("MedicinesOnRevision");
            }
        }

        public MedicineRevisionWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this._medicineRevisionStorage = new MedicineRevisionStorage();
            MedicinesOnRevision = _medicineRevisionStorage.GetAll();

            SetRevisionStatusTextBlock();
            SetRevisionDoctorTextBlock();
        }

        private void SetRevisionDoctorTextBlock()
        {
            DoctorStorage doctorStorage = new DoctorStorage();

            foreach(MedicineRevision mr in MedicinesOnRevision)
            {
                Hospital.Model.Doctor dr = doctorStorage.GetDoctorByID(mr.DoctorID);
                mr.RevisionDoctor = dr;
                mr.RevisionDoctor.FirstName = dr.ToString();
            }
        }

        private void SetRevisionStatusTextBlock()
        {
            foreach(MedicineRevision mr in MedicinesOnRevision)
            {
                if (mr.IsMedicineRevised)
                    mr.RevisionStatus = "Vracen sa revizije";
                else
                    mr.RevisionStatus = "Na reviziji";
            }
        }

        private void EditMedicine(object sender, RoutedEventArgs e)
        {
            MedicineRevision selectedMedicineOnRevision = (MedicineRevision)listBoxMedicines.SelectedItem;
            if (selectedMedicineOnRevision == null)
                return;

            EditMedicineOnRevision editMedicine = new EditMedicineOnRevision(selectedMedicineOnRevision);
            NavigationService.Navigate(editMedicine);
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MedicinesWindow());
        }
    }
}
