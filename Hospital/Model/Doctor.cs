using System;
using System.Text;

namespace Hospital
{
   public class Doctor : User
   {
        private DoctorSpecialty specialty;
        private int roomID;
        public DoctorSpecialty Specialty
        {
            get { return specialty; }
            set { specialty = value; }
        }

        public int RoomID
        {
            get { return roomID; }
            set { roomID = value; }
        }

        override
        public string ToString()
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append("dr ");
            sb.Append(this.FirstName);
            sb.Append(" ");
            sb.Append(this.LastName);

            return sb.ToString();
        }
    }
}