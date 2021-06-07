
using System;

namespace Hospital
{
    public class Examination
    {
        public String diagnosis;
        public String anamnesis;
        public DateTime dateOfExamination;
        public Therapy therapy;
        public Appointment appointment;

        public Examination()
        {
            therapy = new Therapy();
            appointment = new Appointment();
        }
    }
}