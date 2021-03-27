using System;

namespace Hospital
{

    public class Examination
    {
        public String diagnosis;
        public String anamnesis;

        public Therapy therapy;
        public MedicalRecord medicalRecord;

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
        public Appointment appointment;

    }
}