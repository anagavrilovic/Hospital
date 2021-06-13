using Hospital.ViewModels.Secretary;
using System.Windows.Controls;

namespace Hospital.View
{
    public partial class KreiranjeKartona : Page
    {
        public KreiranjeKartona(KreiranjeKartonaViewModel kreiranjeKartonaViewModel)
        {
            InitializeComponent();
            this.DataContext = kreiranjeKartonaViewModel;
        }
    }
}
