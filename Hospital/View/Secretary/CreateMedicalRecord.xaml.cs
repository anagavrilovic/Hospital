using Hospital.ViewModels.Secretary;
using System.Windows.Controls;

namespace Hospital.View
{
    public partial class CreateMedicalRecord : Page
    {
        public CreateMedicalRecord(CreateMedicalRecordViewModel kreiranjeKartonaViewModel)
        {
            InitializeComponent();
            this.DataContext = kreiranjeKartonaViewModel;
        }
    }
}
