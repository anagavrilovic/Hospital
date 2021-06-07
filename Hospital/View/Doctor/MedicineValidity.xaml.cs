using Hospital.Controller;
using Hospital.Model;
using Hospital.Services;
using Hospital.ViewModels.Doctor;
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
    public partial class MedicineValidity : Page
    {
        public MedicineValidity(Model.Doctor doctor, NavigationController navigationController)
        {
            InitializeComponent();
            this.DataContext = new MedicineValidityViewModel(doctor,navigationController);
        }
    }
}
