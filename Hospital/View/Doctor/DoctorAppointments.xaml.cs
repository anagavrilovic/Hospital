using Hospital.Controller;
using Hospital.Model;
using Hospital.Services;
using Hospital.View.Doctor;
using Hospital.ViewModels.Doctor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Hospital.View.Doctor
{
    /// <summary>
    /// Interaction logic for KalendarTermini.xaml
    /// </summary>
    public partial class DoctorAppointments : Page
    {
       

        public DoctorAppointments(string IDnumber,NavigationController navigationController)
        {
            InitializeComponent();
            this.DataContext = new DoctorAppointmentsViewModel(IDnumber, navigationController);
        }
    }
}
