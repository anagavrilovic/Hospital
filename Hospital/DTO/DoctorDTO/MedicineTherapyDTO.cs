using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DTO.DoctorDTO
{
    public class MedicineTherapyDTO : ViewModel
    {
        ObservableCollection<Medicine> medics = new ObservableCollection<Medicine>();
        public ObservableCollection<Medicine> Medics
        {
            get { return medics; }
            set
            {
                medics = value;
                OnPropertyChanged("Medics");
            }

        }

        private string medicineDescription;
        public string MedicineDescription
        {
            get { return medicineDescription; }
            set
            {
                medicineDescription = value;
                OnPropertyChanged("MedicineDescription");
            }

        }
        private string daysForConsumption;
        public string DaysForConsumption
        {
            get { return daysForConsumption; }
            set
            {
                daysForConsumption = value;
                OnPropertyChanged("DaysForConsumption");
            }
        }
        private string dailyIntake;
        public string DailyIntake
        {
            get { return dailyIntake; }
            set
            {
                dailyIntake = value;
                OnPropertyChanged("DailyIntake");
            }
        }
    }
}
