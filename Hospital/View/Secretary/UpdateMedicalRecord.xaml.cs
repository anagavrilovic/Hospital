using Hospital.ViewModels.Secretary;
using System.Windows.Controls;

namespace Hospital.View
{
    public partial class UpdateMedicalRecord : Page
    {
        public UpdateMedicalRecord(UpdateMedicalRecordViewModel izmenaKartonaViewModel)
        {
            InitializeComponent();
            this.DataContext = izmenaKartonaViewModel;
        }
    }
}
