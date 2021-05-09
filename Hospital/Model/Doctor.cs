using System;
using System.Text;

namespace Hospital.Model
{
   public class Doctor : User
   {
        public DoctorSpecialty Specialty { get; set; }
        public string RoomID { get; set; }

        public bool IsEqualWith(Doctor doctorForComparing)
        {
            return this.PersonalID.Equals(doctorForComparing.PersonalID);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder("");
            stringBuilder.Append("dr ").Append(this.FirstName).Append(" ").Append(this.LastName);
   
            return stringBuilder.ToString();
        }
    }
}