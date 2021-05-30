using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DTO.PatientDTO
{
   public class CalendarDTO
    {
        public TimeSpan Term { get; set; }

        public List<String> Doctors { get; set; }
        public List<DateTime> Dates { get; set; }
        public List<String> DoctorsID { get; set; }
        public DateTime TrackedDateTime { get; set; }

        public CalendarDTO(int hour, int minute, DateTime mondayTime)
        {
            Doctors = new List<string>(new string[7]);
            Dates = new List<DateTime>(new DateTime[7]);
            DoctorsID = new List<string>(new string[7]);
            var term = hour.ToString() + ":" + minute.ToString() + ":" + "00";
            Term = TimeSpan.Parse(term);
            TrackedDateTime = mondayTime.Date + Term;
        }
    }
}
