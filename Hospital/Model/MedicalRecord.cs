using System;
using System.Collections.Generic;
using System.ComponentModel;

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
        public List<Allergen> allergen;

        public MedicalRecord()
        {
            Patient = new Patient();
            examination = new List<Examination>();
            allergen = new List<Allergen>();
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

        /// <summary>
        /// Property for collection of Allergen
        /// </summary>
        /// <pdGenerated>Default opposite class collection property</pdGenerated>
        public System.Collections.Generic.List<Allergen> Allergen
        {
            get
            {
                if (allergen == null)
                    allergen = new List<Allergen>();
                return allergen;
            }
            set
            {
                RemoveAllAllergen();
                if (value != null)
                {
                    foreach (Allergen oAllergen in value)
                        AddAllergen(oAllergen);
                }
            }
        }

        /// <summary>
        /// Add a new Allergen in the collection
        /// </summary>
        /// <pdGenerated>Default Add</pdGenerated>
        public void AddAllergen(Allergen newAllergen)
        {
            if (newAllergen == null)
                return;
            if (this.allergen == null)
                this.allergen = new List<Allergen>();
            if (!this.allergen.Contains(newAllergen))
                this.allergen.Add(newAllergen);
        }

        /// <summary>
        /// Remove an existing Allergen from the collection
        /// </summary>
        /// <pdGenerated>Default Remove</pdGenerated>
        public void RemoveAllergen(Allergen oldAllergen)
        {
            if (oldAllergen == null)
                return;
            if (this.allergen != null)
                if (this.allergen.Contains(oldAllergen))
                    this.allergen.Remove(oldAllergen);
        }

        /// <summary>
        /// Remove all instances of Allergen from the collection
        /// </summary>
        /// <pdGenerated>Default removeAll</pdGenerated>
        public void RemoveAllAllergen()
        {
            if (allergen != null)
                allergen.Clear();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}