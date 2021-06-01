using Hospital.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hospital
{
    public class MedicalRecord
    {
        private string healthCardNumber;
        private string parentName;
        private bool isInsured;
        private string medicalRecordID;
        private Patient patient = new Patient();
        private List<Examination> examination = new List<Examination>();
        private Allergen allergen = new Allergen();
        private BloodType bloodType;

        public MedicalRecord() { }

        public BloodType BloodType
        {
            get { return bloodType; }
            set
            {
                bloodType = value;
            }
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

        public string HealthCardNumber
        {
            get => healthCardNumber;
            set
            {
                healthCardNumber = value;
            }
        }

        public string ParentName
        {
            get => parentName;
            set
            {
                parentName = value;
            }
        }

        public bool IsInsured
        {
            get => isInsured;
            set
            {
                isInsured = value;
            }
        }

        public string MedicalRecordID
        {
            get => medicalRecordID;
            set
            {
                medicalRecordID = value;
            }
        }

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

        public void AddExamination(Examination newExamination)
        {
            if (newExamination == null)
                return;
            if (this.examination == null)
                this.examination = new List<Examination>();
            if (!this.examination.Contains(newExamination))
                this.examination.Add(newExamination);
        }

        public void RemoveExamination(Examination oldExamination)
        {
            if (oldExamination == null)
                return;
            if (this.examination != null)
                if (this.examination.Contains(oldExamination))
                    this.examination.Remove(oldExamination);
        }

        public void RemoveAllExamination()
        {
            if (examination != null)
                examination.Clear();
        }

        override
        public string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder("");
            stringBuilder.Append(this.Patient.FirstName).Append(" ").Append(this.Patient.LastName).Append(" | ").Append(this.Patient.PersonalID);

            return stringBuilder.ToString();
        }

        public bool HasSameIDAs(MedicalRecord medicalRecord)
        {
            return this.medicalRecordID.Equals(medicalRecord.medicalRecordID);
        }

        public void DeepCopy(MedicalRecord original)
        {
            this.Patient.FirstName = original.Patient.FirstName;
            this.Patient.LastName = original.Patient.LastName;
            this.Patient.Address.Street = original.Patient.Address.Street;
            this.Patient.Address.StreetNumber = original.Patient.Address.StreetNumber;
            this.Patient.CardID = original.Patient.CardID;
            this.Patient.Address.City.CityName = original.Patient.Address.City.CityName;
            this.Patient.Address.City.Country.CountryName = original.Patient.Address.City.Country.CountryName;
            this.Patient.DateOfBirth = original.Patient.DateOfBirth;
            this.Patient.Email = original.Patient.Email;
            this.Patient.Gender = original.Patient.Gender;
            this.Patient.IsGuest = original.Patient.IsGuest;
            this.Patient.MaritalStatus = original.Patient.MaritalStatus;
            this.Patient.Password = original.Patient.Password;
            this.Patient.PersonalID = original.Patient.PersonalID;
            this.Patient.PhoneNumber = original.Patient.PhoneNumber;
            this.Patient.Address.City.PostalCode = original.Patient.Address.City.PostalCode;
            this.Patient.Username = original.Patient.Username;
            this.Allergen = original.Allergen;
            this.Examination = original.Examination;
            this.HealthCardNumber = original.HealthCardNumber;
            this.IsInsured = original.IsInsured;
            this.MedicalRecordID = original.MedicalRecordID;
            this.ParentName = original.ParentName;
            this.BloodType = original.BloodType;
        }
    }
}