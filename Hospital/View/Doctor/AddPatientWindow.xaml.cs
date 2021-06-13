using Hospital.Services;
using Hospital.ViewModels.Doctor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for PacijentListBox.xaml
    /// </summary>
    public partial class AddPatientWindow : Window
    {
        public AddPatientWindow(NewAppointmentViewModel parentViewModel)
        {
            InitializeComponent();
            AddPatientViewModel viewModel = new AddPatientViewModel(parentViewModel);
            this.DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(this.Close);

        }
    }
}
