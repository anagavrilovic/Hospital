// File:    MedicalRecord.cs
// Author:  ShowRunner
// Created: Tuesday, March 23, 2021 10:16:14 PM
// Purpose: Definition of Class MedicalRecord

using System;
using System.Collections.Generic;

namespace Hospital
{
    public class MedicalRecord
    {
        private String healthCardNumber;
        private String parentName;
        private Boolean isInsured;
        private int medicalRecordID;
        private Patient patient = new Patient();

        private List<Examination> examination;

        public MedicalRecord()
        {
            Patient = new Patient();
            examination = new List<Examination>();
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
            }
        }

        public String ParentName
        {
            get => parentName;
            set
            {
                parentName = value;
            }
        }

        public Boolean IsInsured
        {
            get => isInsured;
            set
            {
                isInsured = value;
            }
        }

        public int MedicalRecordID
        {
            get => medicalRecordID;
            set
            {
                medicalRecordID = value;
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
            {
                this.examination.Add(newExamination);
                newExamination.MedicalRecord = this;
            }
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
                {
                    this.examination.Remove(oldExamination);
                    oldExamination.MedicalRecord = null;
                }
        }

        /// <summary>
        /// Remove all instances of Examination from the collection
        /// </summary>
        /// <pdGenerated>Default removeAll</pdGenerated>
        public void RemoveAllExamination()
        {
            if (examination != null)
            {
                System.Collections.ArrayList tmpExamination = new System.Collections.ArrayList();
                foreach (Examination oldExamination in examination)
                    tmpExamination.Add(oldExamination);
                examination.Clear();
                foreach (Examination oldExamination in tmpExamination)
                    oldExamination.MedicalRecord = null;
                tmpExamination.Clear();
            }
        }


    }
}