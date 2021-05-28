using Hospital.Controller;
using Hospital.View.Doctor;
using Hospital.ViewModels.Doctor;
using System;
using System.Collections.Generic;
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

namespace Hospital.View.Doctor
{
    /// <summary>
    /// Interaction logic for DoktorGlavnaStranica.xaml
    /// </summary>
    public partial class DoctorMainPage : Page
    {
        public DoctorMainPage(string doctorId, NavigationController navigationController)
        {
            InitializeComponent();
            this.DataContext = new DoctorMainPageViewModel(doctorId, navigationController);
        }
    }
}
