using System;

namespace Hospital
{
   public class Doctor : User
   {
        private DoctorSpecialty specialty;

        public DoctorSpecialty Specialty
        {
            get { return specialty; }
            set { specialty = value; }
        }
   
   }
}