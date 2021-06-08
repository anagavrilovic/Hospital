﻿using Hospital.Model;
using Hospital.Services;
using Hospital.ViewModels.Manager;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

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
            MedicineRevisionService medicineRevisionService = new MedicineRevisionService();
            MedicinesOnRevision = new ObservableCollection<MedicineRevision>(medicineRevisionService.GetAll());

            SetRevisionStatusTextBlock();
            SetRevisionDoctorTextBlock();
        }

        private void SetRevisionDoctorTextBlock()
        {
            DoctorService doctorService = new DoctorService();

            foreach(MedicineRevision mr in MedicinesOnRevision)
            {
                Hospital.Model.Doctor dr = doctorService.GetDoctorById(mr.DoctorID);
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
            NavigationService.Navigate(new MedicinesWindow(new MedicinesWindowViewModel(NavigationService)));
        }
    }
}
