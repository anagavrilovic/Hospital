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
    public class MedicineValidityDTO : ViewModel
    {
        private ObservableCollection<string> ingredients;
        public ObservableCollection<string> Ingredients
        {
            get
            {
                return ingredients;
            }
            set
            {
                if (value != ingredients)
                {
                    ingredients = value;
                    OnPropertyChanged("Ingredients");
                }
            }
        }
        private ObservableCollection<MedicineRevision> medicineRevisions;
        public ObservableCollection<MedicineRevision> MedicineRevisions
        {
            get
            {
                return medicineRevisions;
            }
            set
            {
                if (value != medicineRevisions)
                {
                    medicineRevisions = value;
                    OnPropertyChanged("MedicineRevisions");
                }
            }
        }
        private MedicineRevision selectedRevision = new MedicineRevision();
        public MedicineRevision SelectedRevision
        {
            get
            {
                return selectedRevision;
            }
            set
            {
                if (value != selectedRevision)
                {
                    selectedRevision = value;
                    OnPropertyChanged("SelectedRevision");
                }
            }
        }
    }
}
