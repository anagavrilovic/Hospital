using Hospital.ViewModels.Manager;
using System.Windows.Controls;

namespace Hospital.View.Manager
{
    public partial class ManagerProfil : Page
    {
        public ManagerProfil(ManagerProfileViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
