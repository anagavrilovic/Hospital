using Hospital.Model;
using Hospital.Services;
using Hospital.ViewModels.Doctor;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Hospital.View.Doctor
{
    public partial class HospitalizedPatients : Page
    {

        public HospitalizedPatients()
        {
            InitializeComponent();
            this.DataContext = new HospitalizedPatientsViewModel();
        }
    }
}
