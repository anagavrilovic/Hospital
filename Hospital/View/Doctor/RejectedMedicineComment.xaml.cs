using Hospital.Controller;
using Hospital.Model;
using Hospital.ViewModels.Doctor;
using System;
using System.Collections.Generic;
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

namespace Hospital.View.Doctor
{
    /// <summary>
    /// Interaction logic for OdbijeniLekoviKomentar.xaml
    /// </summary>
    public partial class RejectedMedicineComment : Window
    {

        public RejectedMedicineComment(MedicineRevision medicineRevision, NavigationController navigationController,Model.Doctor doctor)
        {
            InitializeComponent();
            RejectedMedicineViewModel viewModel = new RejectedMedicineViewModel(medicineRevision,navigationController,doctor);
            this.DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(this.Close);
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 3 / 4);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 1 / 2);

        }
    }
}
