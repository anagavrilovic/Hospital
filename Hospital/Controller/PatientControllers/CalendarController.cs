using Hospital.DTO.PatientDTO;
using Hospital.Factory;
using Hospital.Model;
using Hospital.Repositories;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Controller.PatientControllers
{
    class CalendarController
    {
        private CalendarDTO calendarDTO;
        private const int NUMBER_OF_DAYS_IN_WEEK = 7;
        private DoctorService doctorService = new DoctorService(new DoctorFileRepository());
        private AppointmentService appointmentService = new AppointmentService();
        private PatientSettingsService patientSettingsService = new PatientSettingsService();
        private DoctorsShiftService doctorsShiftService = new DoctorsShiftService(new DoctorFileFactory());
        private RoomService roomService = new RoomService();
        private PatientSettings patientSettings;

        public CalendarController()
        {
            
        }

        public CalendarDTO SetCalendarDTO(CalendarDTO calendarDTO)
        {
            this.calendarDTO = calendarDTO;
            patientSettings = patientSettingsService.GetByID(MainWindow.IDnumber);
            SetAppointmentsForTerm();
            return this.calendarDTO;
        }
        private void SetAppointmentsForTerm()
        {
            for (int day = 0; day < NUMBER_OF_DAYS_IN_WEEK; day++)
            {
                if (HasAppointmentPassed(day)) continue;
                if (IsDoctorIrrelevant())
                {
                    SetAppointmentIfDoctorIsIrrelevant(day);
                }
                else
                {
                    SetAppointmentIfDoctorIsRelevant(day);
                }
            }
        }

        private void SetAppointmentIfDoctorIsRelevant(int day)
        {
            calendarDTO.Doctors[day] = "";
            if ((!appointmentService.ExistByTime(calendarDTO.TrackedDateTime, doctorService.GetIDByNameSurname(patientSettings.ChosenDoctor))) && doctorsShiftService.IsDoctorWorkingAtSelectedTime(doctorService.GetDoctorById(doctorService.GetIDByNameSurname(patientSettings.ChosenDoctor)), calendarDTO.TrackedDateTime))
            {
                calendarDTO.Doctors[day] = patientSettings.ChosenDoctor;
               calendarDTO.DoctorsID[day] = doctorService.GetIDByNameSurname(patientSettings.ChosenDoctor);
            }
            calendarDTO.Dates[day] = calendarDTO.TrackedDateTime;
            calendarDTO.TrackedDateTime = calendarDTO.TrackedDateTime.AddDays(1);
        }

        private void SetAppointmentIfDoctorIsIrrelevant(int day)
        {

            calendarDTO.Doctors[day] = "";
            SetAvailableDoctorForAppointment(day);
            calendarDTO.Dates[day] = calendarDTO.TrackedDateTime;
            calendarDTO.TrackedDateTime = calendarDTO.TrackedDateTime.AddDays(1);
        }

        private Boolean HasAppointmentPassed(int day)
        {
            if (calendarDTO.TrackedDateTime < DateTime.Now)
            {
                calendarDTO.Doctors[day] = "";
                calendarDTO.Dates[day] = calendarDTO.TrackedDateTime;
                calendarDTO.TrackedDateTime = calendarDTO.TrackedDateTime.AddDays(1);
                return true;
            }
            return false;
        }

        private Boolean IsDoctorIrrelevant()
        {
            return patientSettings.ChosenDoctor.Equals("Nije mi bitno");
        }

        private void SetAvailableDoctorForAppointment(int day)
        {
            List<Hospital.Model.Doctor> doctors = doctorService.GetAll();
            Boolean first = true;

            foreach (Hospital.Model.Doctor doctor in doctors)
            {
                if ((doctor.Specialty != DoctorSpecialty.general) || (appointmentService.ExistByTime(calendarDTO.TrackedDateTime, doctor.PersonalID)) ||(!doctorsShiftService.IsDoctorWorkingAtSelectedTime(doctor, calendarDTO.TrackedDateTime))) continue;
                if (first)
                {
                    SetDoctor(day, doctor);
                    first = false;
                }
                else
                {
                    if (appointmentService.GetNumberOfAppointmentsForDoctor(calendarDTO.DoctorsID[day]) > appointmentService.GetNumberOfAppointmentsForDoctor(doctor.PersonalID))
                    {
                        SetDoctor(day, doctor);
                    }
                }
            }
        }

        private void SetDoctor(int day, Doctor doctor)
        {
            calendarDTO.Doctors[day] = doctor.FirstName + " " + doctor.LastName;
            calendarDTO.DoctorsID[day] = doctor.PersonalID;
        }

        public Appointment GetAppointment(CalendarDTO patientCalendarDTO,int currentColumnIndex)
        {
            if (String.IsNullOrEmpty(patientCalendarDTO.Doctors[currentColumnIndex - 1]))
            {
                return null;
            }

            return new Appointment(patientCalendarDTO.Dates[currentColumnIndex - 1], AppointmentType.examination, MainWindow.IDnumber, patientCalendarDTO.DoctorsID[currentColumnIndex - 1], appointmentService.GetNewID(), roomService.GetById(doctorService.GetByID(patientCalendarDTO.DoctorsID[currentColumnIndex - 1]).RoomID));
        }
    }

}
