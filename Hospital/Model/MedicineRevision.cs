using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class MedicineRevision : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private Medicine _medicine;
        public Medicine Medicine
        {
            get => _medicine;
            set
            {
                _medicine = value;
                OnPropertyChanged("Medicine");
            }
        }

        private string _doctorID;
        public string DoctorID
        {
            get => _doctorID;
            set
            {
                _doctorID = value;
                OnPropertyChanged("DoctorID");
            }
        }

        private Doctor _revisionDoctor;
        [JsonIgnore]
        public Doctor RevisionDoctor
        {
            get => _revisionDoctor;
            set
            {
                _revisionDoctor = value;
                OnPropertyChanged("RevisionDoctor");
            }
        }

        private string _revisionExplanation;
        public string RevisionExplanation
        {
            get => _revisionExplanation;
            set
            {
                _revisionExplanation = value;
                OnPropertyChanged("RevisionExplanation");
            }
        }

        private bool _isMedicineRevised;
        public bool IsMedicineRevised
        {
            get => _isMedicineRevised;
            set
            {
                _isMedicineRevised = value;
                OnPropertyChanged("IsMedicineRevised");
            }
        }

        
        private string revisionStatus;
        [JsonIgnore]
        public string RevisionStatus
        {
            get => revisionStatus;
            set
            {
                revisionStatus = value;
                OnPropertyChanged("RevisionStatus");
            }
        }
    }
}
