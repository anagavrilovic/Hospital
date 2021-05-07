using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public class OptionForRescheduling
    {
        public ObservableCollection<MoveAppointment> Option { get; set; }
        public DateTime NewUrgentAppointmentTime { get; set; }

        public OptionForRescheduling()
        {
            Option = new ObservableCollection<MoveAppointment>();
        }

        public bool IsEqual(OptionForRescheduling optionForRescheduling)
        {
            if (!Option.Count().Equals(optionForRescheduling.Option.Count()))
                return false;

            foreach (MoveAppointment moveAppointment in Option)
            {
                bool found = false;
                foreach (MoveAppointment move in optionForRescheduling.Option)
                {
                    if (moveAppointment.Appointment.IDAppointment.Equals(move.Appointment.IDAppointment))
                        found = true;
                }

                if (!found)
                    return false;
            }

            return true;
        }

        public void SetTimeForRescheduling(Appointment newUrgentAppointment)
        {
            AppointmentStorage appointmentStorage = new AppointmentStorage();
            SetTimeForNewUrgentAppointment(newUrgentAppointment.DateTime);
            SortOption();

            foreach (var moveAppointment in Option)
            {
                moveAppointment.ToTime = newUrgentAppointment.DateTime.AddHours(newUrgentAppointment.DurationInHours);

                while (true)
                {
                    bool alreadyTaken = false;
                    DateTime testStartTime = moveAppointment.ToTime;
                    DateTime testEndTime = moveAppointment.ToTime.AddHours(moveAppointment.Appointment.DurationInHours);

                    if (appointmentStorage.CheckIfOverlap(testStartTime, testEndTime, moveAppointment.Doctor.PersonalID, Option))
                    {
                        moveAppointment.ToTime = moveAppointment.ToTime.AddMinutes(30);
                        continue;
                    }

                    foreach (var existingMoveappointment in Option)
                    {
                        if (existingMoveappointment.ToTime.Equals(new DateTime()) || moveAppointment.Equals(existingMoveappointment))
                            break;

                        DateTime startTime = existingMoveappointment.ToTime;
                        DateTime endTime = existingMoveappointment.ToTime.AddHours(existingMoveappointment.Appointment.DurationInHours);
                        if (testStartTime < endTime && startTime < testEndTime)
                        {
                            alreadyTaken = true;
                            break;
                        }
                    }

                    if (alreadyTaken)
                    {
                        moveAppointment.ToTime = moveAppointment.ToTime.AddMinutes(30);
                        continue;
                    }

                    break;
                }
            }
        }

        private void SetTimeForNewUrgentAppointment(DateTime dateTime)
        {
            this.NewUrgentAppointmentTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Kind);
        }

        private void SortOption()
        {
            List<MoveAppointment> sortedList = Option.OrderBy(o => o.Appointment.DateTime).ToList();
            Option.Clear();
            Option = new ObservableCollection<MoveAppointment>(sortedList);
        }
    }
}
