// File:    Examination.cs
// Author:  ShowRunner
// Created: Tuesday, March 23, 2021 10:16:39 PM
// Purpose: Definition of Class Examination

using System;

namespace Hospital
{
    public class Examination
    {
        public String diagnosis;
        public String anamnesis;

        public MedicalRecord medicalRecord;

        public Examination()
        {
            medicalRecord = new MedicalRecord();
        }

        /// <summary>
        /// Property for MedicalRecord
        /// </summary>
        /// <pdGenerated>Default opposite class property</pdGenerated>
        public MedicalRecord MedicalRecord
        {
            get
            {
                return medicalRecord;
            }
            set
            {
                if (this.medicalRecord == null || !this.medicalRecord.Equals(value))
                {
                    if (this.medicalRecord != null)
                    {
                        MedicalRecord oldMedicalRecord = this.medicalRecord;
                        this.medicalRecord = null;
                        oldMedicalRecord.RemoveExamination(this);
                    }
                    if (value != null)
                    {
                        this.medicalRecord = value;
                        this.medicalRecord.AddExamination(this);
                    }
                }
            }
        }

    }
}