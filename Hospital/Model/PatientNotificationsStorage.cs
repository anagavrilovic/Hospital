using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    class PatientNotificationsStorage
    {
        private String fileName;

        public PatientNotificationsStorage(String file = "patientNotificationsStorage.json")
        {
            this.fileName = file;

        }

        public ObservableCollection<PatientTherapyMedicineNotification> GetAll()
        {
            ObservableCollection<PatientTherapyMedicineNotification> patientNotifications;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                patientNotifications = (ObservableCollection<PatientTherapyMedicineNotification>)serializer.Deserialize(file, typeof(ObservableCollection<PatientTherapyMedicineNotification>));
            }

            return patientNotifications;
        }

        public void DoSerialization(ObservableCollection<PatientTherapyMedicineNotification> patientNotifications)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, patientNotifications);
            }
        }

        public void SaveAll()
        {
            ObservableCollection<PatientTherapyMedicineNotification> patientNotifications = GetAll();
            if (patientNotifications == null)
            {
                patientNotifications = new ObservableCollection<PatientTherapyMedicineNotification>();
            }
            MedicalRecordStorage medicalRecordStorage = new MedicalRecordStorage();
            MedicalRecord medicalRecord = medicalRecordStorage.GetByPatientID(MainWindow.IDnumber);
            foreach (Examination e in medicalRecord.Examination)
            {
                int x = 0;
                foreach (MedicineTherapy med in e.therapy.Medicine)
                {
                    x++;
                    MedicineStorage medicineStorage = new MedicineStorage();
                    Medicine medicine = medicineStorage.GetOne(med.MedicineID);
                    PatientTherapyMedicineNotification patientTherapyMedicineNotification = new PatientTherapyMedicineNotification();
                    patientTherapyMedicineNotification.Name = e.therapy.name + ": " + medicine.Name;
                    for (int i = 0; i < med.TimesPerDay; i++)
                    {
                        TimeSpan ts = new TimeSpan(i * 3 + 10, 0, 0);
                        DateTime dateTime = DateTime.Now.Date + ts;
                        patientTherapyMedicineNotification.updateTimes(dateTime);
                    }
                    patientTherapyMedicineNotification.Read = false;
                    DateTime date1 = e.appointment.DateTime;
                    DateTime date2 = date1.AddDays(med.DurationInDays);
                    patientTherapyMedicineNotification.FromDate = date1;
                    patientTherapyMedicineNotification.ToDate = date2;
                    patientTherapyMedicineNotification.updateDuration();
                    if ((date1.Date <= DateTime.Now) && (date2.Date >= DateTime.Now))
                    {
                        patientTherapyMedicineNotification.Active = true;
                    }
                    else
                    {
                        patientTherapyMedicineNotification.Active = false;
                    }
                    patientTherapyMedicineNotification.ID = x.ToString();
                    patientNotifications.Add(patientTherapyMedicineNotification);
                }
            }
            DoSerialization(patientNotifications);
        }


        public String GetNewID()
        {
            ObservableCollection<PatientTherapyMedicineNotification> apps;
            using (StreamReader sr = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                apps = GetAll();
                apps = JsonConvert.DeserializeObject<ObservableCollection<PatientTherapyMedicineNotification>>(sr.ReadToEnd());
            }

            int retVal = 1;
            if (apps==null || apps.Count == 0)
            {
                return retVal.ToString();
            }
            foreach (PatientTherapyMedicineNotification app in apps)
            {
                int x = Int32.Parse(app.ID);
                if (x >= retVal)
                {
                    retVal++;
                }
            }
            return retVal.ToString();
        }

        public void Save(PatientTherapyMedicineNotification parameter1)
        {
            ObservableCollection<PatientTherapyMedicineNotification> appointment = GetAll();
            appointment.Add(parameter1);
            DoSerialization(appointment);
        }

        public Boolean Delete(string id)
        {
            ObservableCollection<PatientTherapyMedicineNotification> appointments = GetAll();
            foreach (PatientTherapyMedicineNotification r in appointments)
            {
                if (r.ID.Equals(id))
                {
                    appointments.Remove(r);
                    DoSerialization(appointments);
                    return true;
                }
            }
            return false;
        }


    }
}
