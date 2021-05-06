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
        public TimeSpan Vremena { get; set; }

        public List<String> Days { get; set; }
        public List<DateTime> Dates { get; set; }
        public List<String> Doctors { get; set; }

        private PatientSettingsStorage patientSettingsStorage = new PatientSettingsStorage();
        private PatientSettings patientSettings;
        private AppointmentStorage appointmentStorage = new AppointmentStorage();
        private DoctorStorage doctorStorage = new DoctorStorage();
        /*  public String Ponedeljak { get; set; }
          public String Utorak { get; set; }
          public String Sreda { get; set; }
          public String Cetvrtak { get; set; }
          public String Petak { get; set; }
          public String Subota { get; set; }
          public String Nedelja { get; set; }
          DateTime PonedeljakVreme { get; set; }
          DateTime UtorakVreme { get; set; }
          DateTime SredaVreme { get; set; }
          DateTime CetvrtakVreme { get; set; }
          DateTime PetakVreme { get; set; }
          DateTime SubotaVreme { get; set; }
          DateTime NedeljaVreme { get; set; }*/
        public PatientCalendarDTO(int i, int j, DateTime mondayTime)
        {
            patientSettings = patientSettingsStorage.getByID(MainWindow.IDnumber);
            Days = new List<string>(new string[7]);
            Dates = new List<DateTime>(new DateTime[7]);
            Doctors = new List<string>(new string[7]);
            var start = i.ToString() + ":" + j.ToString() + ":" + "00";   //"17:05:11";
            var startTime = DateTime.Parse(start);
            //Vremena = startTime;
            Vremena = startTime.TimeOfDay;
            mondayTime = mondayTime.Date + Vremena;
            for (int br = 0; br < 7; br++)
            {
                
                   
                        if (mondayTime < DateTime.Now)
                        {
                            Days[br] = "";
                            Dates[br] = mondayTime;
                            mondayTime = mondayTime.AddDays(1);
                            continue;
                        }
                        if (IsDoctorIrrelevant())
                        {
                            if (IsAppointmentFreeForAnyDoctor(mondayTime) == null)
                            {
                                Days[br] = "";
                            }
                            else
                            {
                                Days[br] = "dr "+ IsAppointmentFreeForAnyDoctor(mondayTime);
                                Doctors[br] = doctorStorage.GetIDByNameSurname(IsAppointmentFreeForAnyDoctor(mondayTime));
                    }
                        }
                        else
                        {
                            if(appointmentStorage.ExistByTime(mondayTime, doctorStorage.GetIDByNameSurname(patientSettings.ChosenDoctor)))
                            {
                                Days[br] = "";
                                
                            }
                            else
                            {
                                Days[br] = "dr "+ patientSettings.ChosenDoctor;
                                Doctors[br] = doctorStorage.GetIDByNameSurname(patientSettings.ChosenDoctor);
                            }
                        }
                        
                        Dates[br] = mondayTime;
                        mondayTime = mondayTime.AddDays(1);
                        
                   }
            }
        private Boolean IsDoctorIrrelevant()
        {
            if (patientSettings.ChosenDoctor.Equals("Nije mi bitno")) return true;
            return false;

        }

        private String IsAppointmentFreeForAnyDoctor(DateTime varVreme)
        {
            ObservableCollection<Hospital.Model.Doctor> doctors = doctorStorage.GetAll();
            Boolean first = true;
            String nameSurname=null;
            String IDdoctor=null;

            foreach (Hospital.Model.Doctor d in doctors)
            {
                if (d.Specialty != DoctorSpecialty.general) continue;
                if (appointmentStorage.ExistByTime(varVreme, d.PersonalID)) continue;
                if (first)
                {
                    IDdoctor = d.PersonalID;
                    nameSurname = d.FirstName + " " + d.LastName;
                    first = false;
                }
                else
                {
                    if (appointmentStorage.GetNumberOfAppointmentsForDoctor(IDdoctor) > appointmentStorage.GetNumberOfAppointmentsForDoctor(d.PersonalID))
                    {
                        IDdoctor = d.PersonalID;
                        nameSurname = d.FirstName + " " + d.LastName;
                        

                    }
                }
            }
            return nameSurname;
        }
    }
}
