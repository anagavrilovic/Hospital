using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Hospital.ViewModels.Secretary
{
    public class DetaljiKartonViewModel : ViewModel
    {
        #region Properties

        public MedicalRecord PatientsRecord { get; set; }
        public NavigationService NavigationService { get; set; }

        #endregion

        #region Konstruktor

        public DetaljiKartonViewModel(NavigationService navigationService, MedicalRecord selectedRecord)
        {
            this.NavigationService = navigationService;
            this.PatientsRecord = selectedRecord;
        }

        #endregion
    }
}
