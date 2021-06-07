using Hospital.ViewModels.Secretary;
using System.Windows.Controls;

namespace Hospital.View
{
    public partial class IzmenaKartona : Page
    {
        public IzmenaKartona(IzmenaKartonaViewModel izmenaKartonaViewModel)
        {
            InitializeComponent();
            this.DataContext = izmenaKartonaViewModel;
        }
    }
}
