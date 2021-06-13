using Hospital.Factory;
using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class PatientSettingsService
    {
        IPatientSettingsRepository patientSettingsRepository;

        public PatientSettingsService(IPatientSettingsRepositoryFactory factory)
        {
            patientSettingsRepository = factory.CreatePatientSettingsRepository();
        }

        

        public List<PatientSettings> GetAll()
        {
            return patientSettingsRepository.GetAll();
        }

        public PatientSettings GetByID(string id)
        {
            return patientSettingsRepository.GetByID(id);
        }

        public void Save(PatientSettings parameter)
        {
            patientSettingsRepository.Save(parameter);
        }

        public void Update(PatientSettings patientSettings)
        {
            patientSettingsRepository.Update(patientSettings);
        }

        public void FirstSave(String id)
        {
            patientSettingsRepository.FirstSave(id);
        }

        public Boolean IsAntiTrollTriggered()
        {
            PatientSettings patientSettings = patientSettingsRepository.GetByID(MainWindow.IDnumber);

            if ((patientSettings.LatestScheduledAppointmentsTime != null) && (patientSettings.LatestScheduledAppointmentsTime.Count == 3))
            {

                if (AreAppointmentsWithinTenDays(patientSettings.LatestScheduledAppointmentsTime)) return true;
            }
            return false;
        }

        private Boolean AreAppointmentsWithinTenDays(List<DateTime> appointmentsDates)
        {
            foreach (DateTime dts in appointmentsDates)
            {
                if ((DateTime.Now - dts).TotalDays > 10)
                {
                    return false;
                }
            }
            return true;
        }

        public void AddScheduling(DateTime dt)
        {
            PatientSettings patientSettings = patientSettingsRepository.GetByID(MainWindow.IDnumber);
            if (patientSettings.LatestScheduledAppointmentsTime == null)
            {
                patientSettings.LatestScheduledAppointmentsTime = new List<DateTime>();
            }
            patientSettings.LatestScheduledAppointmentsTime.Add(dt);
            if (patientSettings.LatestScheduledAppointmentsTime.Count > 3)
            {
                patientSettings.LatestScheduledAppointmentsTime.RemoveAt(0);
            }
            Update(patientSettings);
        }

    }
}
