using Hospital.ViewModels.Manager;
using System.Windows.Controls;

namespace Hospital.View
{
    public partial class AddInventory : Page
    {

        public AddInventory(AddInventoryViewModel addInventoryViewModel)
        {
            InitializeComponent();
            this.DataContext = addInventoryViewModel;
        }

    }
}
