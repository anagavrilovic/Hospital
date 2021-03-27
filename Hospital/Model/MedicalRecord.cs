
using System;

namespace Hospital
{

    public class MedicalRecord
    {
        public String healthCardNumber;
        public String parentName;
        public Boolean isInsured;
        public int medicalRecordID;

        public System.Collections.Generic.List<Examination> examination;

        public System.Collections.Generic.List<Examination> Examination
        {
            get
            {
                if (examination == null)
                    examination = new System.Collections.Generic.List<Examination>();
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
                this.examination = new System.Collections.Generic.List<Examination>();
            if (!this.examination.Contains(newExamination))
            {
                this.examination.Add(newExamination);
                newExamination.MedicalRecord = this;
            }
        }


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
        public Patient patient;

    }
}