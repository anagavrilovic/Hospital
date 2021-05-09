using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
   public class PatientCalendarDTO
    {
        public TimeSpan Term { get; set; }

        public List<String> Doctors { get; set; }
        public List<DateTime> Dates { get; set; }
        public List<String> DoctorsID { get; set; }

        private PatientSettingsStorage patientSettingsStorage = new PatientSettingsStorage();
        private PatientSettings patientSettings;
        private AppointmentStorage appointmentStorage = new AppointmentStorage();
        private DoctorStorage doctorStorage = new DoctorStorage();
        private const int NUMBER_OF_DAYS_IN_WEEK = 7;
        private DateTime trackedDateTime;


        public PatientCalendarDTO(int hour, int minute, DateTime mondayTime)
        {
            patientSettings = patientSettingsStorage.getByID(MainWindow.IDnumber);
            Doctors = new List<string>(new string[7]);
            Dates = new List<DateTime>(new DateTime[7]);
            DoctorsID = new List<string>(new string[7]);
            var term = hour.ToString() + ":" + minute.ToString() + ":" + "00";   
            Term = TimeSpan.Parse(term);
            trackedDateTime = mondayTime.Date + Term;
            SetAppointmentsForTerm();
            }

        private void SetAppointmentsForTerm()
        {
            for (int day = 0; day < NUMBER_OF_DAYS_IN_WEEK; day++)
            {
                if (HasAppointmentPassed(day)) continue;
                if (IsDoctorIrrelevant())
                {
                    SetAppointmentIfDoctorIsIrrelevant(day);
                }
                else
                {
                    SetAppointmentIfDoctorIsRelevant(day);
                }
            }
        }

        public void SetAppointmentIfDoctorIsRelevant(int day)
        {  
            Doctors[day] = "";
            if (!appointmentStorage.ExistByTime(trackedDateTime, doctorStorage.GetIDByNameSurname(patientSettings.ChosenDoctor)))
                {
                    Doctors[day] = "dr " + patientSettings.ChosenDoctor;
                    DoctorsID[day] = doctorStorage.GetIDByNameSurname(patientSettings.ChosenDoctor);
                }
            Dates[day] = trackedDateTime;
            trackedDateTime = trackedDateTime.AddDays(1);
        }

        public void SetAppointmentIfDoctorIsIrrelevant(int day)
        {
           
            Doctors[day] = "";
            SetAvailableDoctorForAppointment(day);
            Dates[day] = trackedDateTime;
            trackedDateTime = trackedDateTime.AddDays(1);
        }

        private Boolean HasAppointmentPassed(int day)
        {
            if (trackedDateTime < DateTime.Now)
            {
                Doctors[day] = "";
                Dates[day] = trackedDateTime;
                trackedDateTime = trackedDateTime.AddDays(1);
                return true;
            }
            return false;
        }

        private Boolean IsDoctorIrrelevant()
        {
            return patientSettings.ChosenDoctor.Equals("Nije mi bitno");  
        }

        private void SetAvailableDoctorForAppointment(int day)
        {
            ObservableCollection<Hospital.Model.Doctor> doctors = doctorStorage.GetAll();
            Boolean first = true;

            foreach (Hospital.Model.Doctor doctor in doctors)
            {
                if ((doctor.Specialty != DoctorSpecialty.general) || (appointmentStorage.ExistByTime(trackedDateTime, doctor.PersonalID))) continue;
                if (first)
                {
                    SetDoctor(day, doctor);
                    first = false;
                }
                else
                {
                    if (appointmentStorage.GetNumberOfAppointmentsForDoctor(DoctorsID[day]) > appointmentStorage.GetNumberOfAppointmentsForDoctor(doctor.PersonalID))
                    {
                        SetDoctor(day, doctor);  
                    }
                }
            }
        }

        private void SetDoctor(int day,Doctor doctor)
        {
            Doctors[day] = "dr " + doctor.FirstName + " " + doctor.LastName;
            DoctorsID[day] = doctor.PersonalID;
        }
    }
}
