using Hospital.Controller;
using Hospital.Model;
using Hospital.Services;
using Hospital.ViewModels.Doctor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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

namespace Hospital.View.Doctor
{
    /// <summary>
    /// Interaction logic for DoktorGlavniProzor.xaml
    /// </summary>
    public partial class DoctorMainWindow : Window
    {
        public DoctorMainWindow(string doctorId)
        {
            InitializeComponent();
            DoctorMainWindowViewModel viewModel = new DoctorMainWindowViewModel(doctorId, new WpfNavigationController(this.Main.NavigationService));
            this.DataContext = viewModel;
            this.Height = (SystemParameters.PrimaryScreenHeight * 3 / 4);
            this.Width = (SystemParameters.PrimaryScreenWidth * 3 / 4);
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(this.Close);
        }

    }
}
