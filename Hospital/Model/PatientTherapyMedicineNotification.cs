﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class PatientTherapyMedicineNotification:IPatientNotification
    {
        public string Text { get; set; }
        public string Name { get; set; }
        public int DurationInDays { get; set; }

        public string ID { get; set; }

        public string IDpatient { get; set; }

        public DateTime LastRead { get; set; }

        public bool Active { get; set; }

        public bool Read { get; set; }

        private String times { get; set; }
        public String Times
        {
            get
            {
                return times;
            }
            set
            {
                times = value;
            }
        }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        private String duration;

        public String Duration
        {
            get
            {
                return duration;
            }
            set
            {
                duration = value;
            }
        }

        public void updateTimes(DateTime dt)
        {
            times = Times + dt.ToString("hh:mm tt") + " ";
        }

        public void updateDuration()
        {
            String s = FromDate.ToShortDateString();
            duration = FromDate.ToShortDateString() + " - " + ToDate.ToShortDateString();
        }

        [JsonIgnore]
        public MedicalRecord PatientsRecord { get; set; }

        public PatientTherapyMedicineNotification(string iD, string iDpatient, bool read, DateTime fromDate, DateTime toDate,string text)
        {
            ID = iD;
            IDpatient = iDpatient;
            Read = read;
            FromDate = fromDate;
            ToDate = toDate;
            Text = text;
        }
    }
}
