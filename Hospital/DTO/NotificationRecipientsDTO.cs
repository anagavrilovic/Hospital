using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DTO
{
    public class NotificationRecipientsDTO : INotifyPropertyChanged
    {
        private bool isEverySecretaryRecipient;

        public bool IsEverySecretaryRecipient
        {
            get { return isEverySecretaryRecipient; }
            set 
            { 
                isEverySecretaryRecipient = value;
                OnPropertyChanged("IsEverySecretaryRecipient");
            }
        }

        private bool isEveryDoctorRecipient;

        public bool IsEveryDoctorRecipient
        {
            get { return isEveryDoctorRecipient; }
            set 
            { 
                isEveryDoctorRecipient = value;
                OnPropertyChanged("IsEveryDoctorRecipient");
            }
        }

        private bool isEveryManagerRecipient;

        public bool IsEveryManagerRecipient
        {
            get { return isEveryManagerRecipient; }
            set 
            { 
                isEveryManagerRecipient = value;
                OnPropertyChanged("IsEveryManagerRecipient");
            }
        }

        private bool isEveryPatientRecipient;

        public bool IsEveryPatientRecipient
        {
            get { return isEveryPatientRecipient; }
            set 
            { 
                isEveryPatientRecipient = value;
                OnPropertyChanged("IsEveryPatientRecipient");
            }
        }

        public ObservableCollection<MedicalRecord> RecipientsRecords { get; set; }

        public NotificationRecipientsDTO()
        {
            RecipientsRecords = new ObservableCollection<MedicalRecord>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
