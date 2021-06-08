using Hospital.ViewModels.Manager;
using System.Windows.Controls;

namespace Hospital.View
{
    public partial class MedicineRevisionWindow : Page
    {

        public MedicineRevisionWindow(MedicineRevisionViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
