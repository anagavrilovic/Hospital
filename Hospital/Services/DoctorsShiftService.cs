using Hospital.DTO;
using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class DoctorsShiftService
    {
        private IDoctorRepository doctorRepository;

        private AppointmentService appointmentService = new AppointmentService();

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



        public List<DoctorsShiftsDTO> GetAllDoctorsShiftsForNextFiveDays(DateTime selectedDate)
        {
            List<DoctorsShiftsDTO> doctorsShiftsDTOs = new List<DoctorsShiftsDTO>();
            List<Doctor> allDoctors = doctorRepository.GetAll();

            foreach (Doctor doctor in allDoctors)
            {
                DoctorsShiftsDTO dto = GetDoctorsShiftsForNextFiveDays(doctor, selectedDate);
                doctorsShiftsDTOs.Add(dto);
            }

            return doctorsShiftsDTOs;
        }

        private DoctorsShiftsDTO GetDoctorsShiftsForNextFiveDays(Doctor doctor, DateTime selectedDate)
        {
            DoctorsShiftsDTO dto = new DoctorsShiftsDTO { Doctor = doctor };

            PropertyInfo[] properties = typeof(DoctorsShiftsDTO).GetProperties();
            for (int i = 1; i < properties.Length; i++)
            {
                properties[i].SetValue(dto, GetDoctorsShiftByDate(doctor, selectedDate));
                selectedDate = selectedDate.AddDays(1);
            }

            return dto;
        }


        public string GetDoctorsShiftByDate(Doctor doctor, DateTime selectedDate)
        {
            DateTime todaysDate = DateTime.Today.Date;
            ScheduledShift scheduledShift = GetScheduledShiftForDate(doctor, selectedDate);
            if (scheduledShift != null)
                return ConvertShiftToString(scheduledShift.Shift);
            else
                return CalculateShift(doctor, selectedDate);
        }

        private string CalculateShift(Doctor doctor, DateTime selectedDate)
        {
            Shift s;
            ScheduledShift rollingShiftStarts = GetClosestDateWhenRollingIsSet(doctor, selectedDate);
            if (rollingShiftStarts == null)
                rollingShiftStarts = new ScheduledShift { Shift = doctor.Shifts.Shift, Date = DateTime.Today, RollShifts = true };

            int dayDifference = (selectedDate - rollingShiftStarts.Date).Days;
            if (dayDifference == 0 && ((int)rollingShiftStarts.Shift + dayDifference == 4))
                return ConvertShiftToString(doctor.Shifts.Shift);
            s = (Shift)(((int)rollingShiftStarts.Shift + dayDifference) % 4);

            return ConvertShiftToString(s);
        }

        private ScheduledShift GetClosestDateWhenRollingIsSet(Doctor doctor, DateTime selectedDate)
        {
            doctor.Shifts.ScheduledShifts = doctor.Shifts.ScheduledShifts.OrderBy(s => s.Date).ToList();
            for (int i = doctor.Shifts.ScheduledShifts.Count() - 1; i >= 0; i--)
                if (doctor.Shifts.ScheduledShifts[i].RollShifts && selectedDate > doctor.Shifts.ScheduledShifts[i].Date)
                    return doctor.Shifts.ScheduledShifts[i];
            return null;
        }

        private string ConvertShiftToString(Shift shift)
        {
            switch (shift)
            {
                case Shift.first: return "I";
                case Shift.second: return "II";
                case Shift.third: return "III";
                case Shift.pause: return "P";
                case Shift.free: return "O";
                default: return "/";
            }
        }

        private ScheduledShift GetScheduledShiftForDate(Doctor doctor, DateTime selectedDate)
        {
            foreach (ScheduledShift scheduledShift in doctor.Shifts.ScheduledShifts)
                if (scheduledShift.Date.Date == selectedDate.Date)
                    return scheduledShift;
            return null;
        }


        public void ChangeShift(Doctor doctor, ScheduledShift shiftForScheduling)
        {
            if (shiftForScheduling.Date.Date == DateTime.Today.Date)
            {
                doctor.Shifts.Shift = shiftForScheduling.Shift;
                if (shiftForScheduling.RollShifts)
                    doctor.Shifts.ChangingShift = shiftForScheduling.Shift;
            }
            else
                ScheduleShift(doctor, shiftForScheduling);

            doctorRepository.Update(doctor);

            appointmentService.CancelAppointmentsBecauseOfShiftChange(doctor, shiftForScheduling);
        }

        private void ScheduleShift(Doctor doctor, ScheduledShift shiftForScheduling)
        {
            foreach (ScheduledShift shift in doctor.Shifts.ScheduledShifts)
            {
                if (shift.Date.Date == shiftForScheduling.Date.Date)
                {
                    shift.Shift = shiftForScheduling.Shift;
                    shift.RollShifts = shiftForScheduling.RollShifts;
                    return;
                }
            }

            doctor.Shifts.ScheduledShifts.Add(shiftForScheduling);
        }


        public bool SetFreeDays(Doctor selectedDoctor, DateTime startDate, DateTime endDate)
        {
            if (GetFreeDays(selectedDoctor) < CountWorkDays(startDate, endDate))
                return false;

            selectedDoctor.Shifts.NumberOfFreeDays -= CountWorkDays(startDate, endDate);
            for (DateTime i = startDate; i <= endDate; i = i.AddDays(1))
            {
                ScheduledShift freeShift = new ScheduledShift { Shift = Shift.free, Date = i, RollShifts = false };
                selectedDoctor.Shifts.ScheduledShifts.Add(freeShift);
            }

            doctorRepository.Update(selectedDoctor);
            return true;
        }

        private int CountWorkDays(DateTime startDate, DateTime endDate)
        {
            int workDays = 0;
            for (DateTime i = startDate; i <= endDate; i = i.AddDays(1))
            {
                if (IsWorkDay(i))
                    workDays++;
            }
            return workDays;
        }

        private bool IsWorkDay(DateTime i)
        {
            List<DateTime> nonWorkDays = GetNonWorkDays();
            bool isNonWorkDay = false;
            foreach (DateTime date in nonWorkDays)
            {
                if (date.Day == i.Day && date.Month == i.Month)
                {
                    isNonWorkDay = true;
                    break;
                }
            }

            return i.DayOfWeek != DayOfWeek.Saturday && i.DayOfWeek != DayOfWeek.Sunday && !isNonWorkDay;
        }

        private List<DateTime> GetNonWorkDays()
        {
            List<DateTime> nonWorkDays = new List<DateTime>(); ;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\nonWorkDays.json"))
                nonWorkDays = (List<DateTime>)new JsonSerializer().Deserialize(file, typeof(List<DateTime>));

            return nonWorkDays;
        }


        public int GetFreeDays(Doctor selectedDoctor)
        {
            return selectedDoctor.Shifts.NumberOfFreeDays;
        }

        public bool IsDoctorWorkingAtSelectedTime(Doctor doctor, DateTime dateTimeForNewAppointment)
        {
            string doctorsShift = GetDoctorsShiftByDate(doctor, dateTimeForNewAppointment);

            TimeSpan morning = new TimeSpan(6, 0, 0);
            TimeSpan day = new TimeSpan(14, 0, 0);
            TimeSpan night = new TimeSpan(22, 0, 0);

            if (IsTimeBetween(dateTimeForNewAppointment, morning, day) && doctorsShift.Equals("I"))
                return true;
            else if (IsTimeBetween(dateTimeForNewAppointment, day, night) && doctorsShift.Equals("II"))
                return true;
            else if (IsTimeBetween(dateTimeForNewAppointment, night, morning) && doctorsShift.Equals("III"))
                return true;
            else
                return false;
        }

        private bool IsTimeBetween(DateTime datetime, TimeSpan start, TimeSpan end)
        {
            TimeSpan now = datetime.TimeOfDay;
            if (start < end)
                return start <= now && now <= end;
            return !(end < now && now < start);
        }
    }
}
