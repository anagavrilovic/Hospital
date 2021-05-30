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
    public class HospitalizedPatientsDTO : ViewModel
    {
        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged("StartDate");
            }
        }
        private ObservableCollection<HospitalTreatment> hospitalTreatments = new ObservableCollection<HospitalTreatment>();
        public ObservableCollection<HospitalTreatment> HospitalTreatments
        {
            get { return hospitalTreatments; }
            set
            {
                hospitalTreatments = value;
                OnPropertyChanged("HospitalTreatments");
            }
        }
        private HospitalTreatment hospitalTreatment;
        public HospitalTreatment HospitalTreatment
        {
            get
            {
                return hospitalTreatment;
            }
            set
            {
                if (value != hospitalTreatment)
                {
                    hospitalTreatment = value;
                    OnPropertyChanged("HospitalTreatment");
                }
            }
        }

    }
}
