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

        private Medicine medicine;
        public Medicine Medicine
        {
            get => medicine;
            set
            {
                medicine = value;
                OnPropertyChanged("Medicine");
            }
        }

        private string doctorID;
        public string DoctorID
        {
            get => doctorID;
            set
            {
                doctorID = value;
                OnPropertyChanged("DoctorID");
            }
        }

        private Doctor revisionDoctor;
        [JsonIgnore]
        public Doctor RevisionDoctor
        {
            get => revisionDoctor;
            set
            {
                revisionDoctor = value;
                OnPropertyChanged("RevisionDoctor");
            }
        }

        private string revisionExplanation;
        public string RevisionExplanation
        {
            get => revisionExplanation;
            set
            {
                revisionExplanation = value;
                OnPropertyChanged("RevisionExplanation");
            }
        }

        private bool isMedicineRevised;
        public bool IsMedicineRevised
        {
            get => isMedicineRevised;
            set
            {
                isMedicineRevised = value;
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
