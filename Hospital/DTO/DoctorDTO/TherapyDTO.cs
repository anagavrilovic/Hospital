using Hospital.Model;
using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DTO.DoctorDTO
{
    public class TherapyDTO : ViewModel
    {
        private ObservableCollection<MedicineTherapy> medics = new ObservableCollection<MedicineTherapy>();
        public ObservableCollection<MedicineTherapy> Medics
        {
            get { return medics; }
            set
            {
                medics = value;
                OnPropertyChanged("Medics");
            }
        }
        private ObservableCollection<Medicine> medicineView = new ObservableCollection<Medicine>();
        public ObservableCollection<Medicine> MedicineView
        {
            get { return medicineView; }
            set
            {
                medicineView = value;
                OnPropertyChanged("MedicineView");
            }
        }
    }
}
