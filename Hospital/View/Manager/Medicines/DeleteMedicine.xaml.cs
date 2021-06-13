using Hospital.ViewModels.Manager;
using System.Windows.Controls;

namespace Hospital.View
{
    public partial class DeleteMedicine : Page
    {
        public Medicine MedicineForDeleting { get; set; }

        public DeleteMedicine(DeleteMedicineViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
