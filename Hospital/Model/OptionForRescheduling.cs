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
                    if (moveAppointment.Appointment.IDAppointment.Equals(move.Appointment.IDAppointment))
                        found = true;

                if (!found)
                    return false;
            }

            return true;
        }

    }
}
