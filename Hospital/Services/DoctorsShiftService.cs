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
    public class DoctorsShiftService
    {
        IDoctorRepository doctorRepository;

        public DoctorsShiftService()
        {
            doctorRepository = new DoctorFileRepository();
        }

        public void UpdateAllShifts()
        {
            List<Doctor> allDoctors = doctorRepository.GetAll();
            foreach (Doctor doctor in allDoctors)
            {
                UpdateDoctorsShift(doctor);
                doctorRepository.Update(doctor);
            }
        }

        private void UpdateDoctorsShift(Doctor doctor)
        {
            DateTime todaysDate = DateTime.Today.Date;

            if (todaysDate > doctor.Shifts.LastUpdated.Date)
            {
                ScheduledShift scheduledShift = GetScheduledShiftForToday(doctor);
                if (scheduledShift == null)
                    RollShift(doctor);
                else
                {
                    ChangeToScheduledShift(doctor, scheduledShift);
                    doctor.Shifts.ScheduledShifts.Remove(scheduledShift);
                }    
            }
        }

        private void ChangeToScheduledShift(Doctor doctor, ScheduledShift scheduledShift)
        {
            if (scheduledShift.RollShifts)
            {
                doctor.Shifts.ChangingShift = scheduledShift.Shift;
                doctor.Shifts.Shift = scheduledShift.Shift;
                doctor.Shifts.LastUpdated = DateTime.Today;
            }
            else
            {
                doctor.Shifts.ChangingShift = (Shift)(((int)doctor.Shifts.ChangingShift + 1) % 4);
                doctor.Shifts.Shift = scheduledShift.Shift;
                doctor.Shifts.LastUpdated = DateTime.Today;
            }
        }

        private void RollShift(Doctor doctor)
        {
            int daysPassedFromChangingShift = (DateTime.Today - doctor.Shifts.LastUpdated).Days;
            doctor.Shifts.ChangingShift = (Shift)(((int)doctor.Shifts.ChangingShift + daysPassedFromChangingShift) % 4);
            doctor.Shifts.Shift = doctor.Shifts.ChangingShift;
            doctor.Shifts.LastUpdated = DateTime.Today;
        }

        private ScheduledShift GetScheduledShiftForToday(Doctor doctor)
        {
            DateTime todaysDate = DateTime.Today.Date;

            foreach (ScheduledShift scheduledShift in doctor.Shifts.ScheduledShifts)
                if (scheduledShift.Date == todaysDate)
                    return scheduledShift;
            return null;
        }
    }
}
