using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hospital
{
    public class MedicalRecord : INotifyPropertyChanged
    {
        private String healthCardNumber;
        private String parentName;
        private Boolean isInsured;
        private String medicalRecordID;
        private Patient patient = new Patient();

        private List<Examination> examination;
        private Allergen allergen = new Allergen();

        public MedicalRecord()
        {
            Patient = new Patient();
            examination = new List<Examination>();
        }

        public Allergen Allergen
        {
            get => allergen;
            set
            {
                allergen = value;
            }
        }

        public Patient Patient
        {
            get => patient;
            set
            {
                patient = value;
            }
        }

        public String HealthCardNumber
        {
            get => healthCardNumber;
            set
            {
                healthCardNumber = value;
                OnPropertyChanged("HealthCardNumber");
            }
        }

        public String ParentName
        {
            get => parentName;
            set
            {
                parentName = value;
                OnPropertyChanged("ParentName");
            }
        }

        public Boolean IsInsured
        {
            get => isInsured;
            set
            {
                isInsured = value;
                OnPropertyChanged("isInsured");
            }
        }

        public String MedicalRecordID
        {
            get => medicalRecordID;
            set
            {
                medicalRecordID = value;
                OnPropertyChanged("MedicalRecordID");
            }
        }

        /// <summary>
        /// Property for collection of Examination
        /// </summary>
        /// <pdGenerated>Default opposite class collection property</pdGenerated>
        public List<Examination> Examination
        {
            get
            {
                if (examination == null)
                    examination = new List<Examination>();
                return examination;
            }
            set
            {
                RemoveAllExamination();
                if (value != null)
                {
                    foreach (Examination oExamination in value)
                        AddExamination(oExamination);
                }
            }
        }

        /// <summary>
        /// Add a new Examination in the collection
        /// </summary>
        /// <pdGenerated>Default Add</pdGenerated>
        public void AddExamination(Examination newExamination)
        {
            if (newExamination == null)
                return;
            if (this.examination == null)
                this.examination = new List<Examination>();
            if (!this.examination.Contains(newExamination))
                this.examination.Add(newExamination);
        }

        /// <summary>
        /// Remove an existing Examination from the collection
        /// </summary>
        /// <pdGenerated>Default Remove</pdGenerated>
        public void RemoveExamination(Examination oldExamination)
        {
            if (oldExamination == null)
                return;
            if (this.examination != null)
                if (this.examination.Contains(oldExamination))
                    this.examination.Remove(oldExamination);
        }

        /// <summary>
        /// Remove all instances of Examination from the collection
        /// </summary>
        /// <pdGenerated>Default removeAll</pdGenerated>
        public void RemoveAllExamination()
        {
            if (examination != null)
                examination.Clear();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        override
        public string ToString()
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(this.Patient.FirstName);
            sb.Append(" ");
            sb.Append(this.Patient.LastName);
            sb.Append(" ");
            sb.Append(this.Patient.PersonalID);

            return sb.ToString();
        }
    }
}