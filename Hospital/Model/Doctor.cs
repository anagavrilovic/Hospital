using System;

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

    }
}