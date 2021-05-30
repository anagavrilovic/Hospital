using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class AppointmentService
    {
        IAppointmentRepository appointmentRepository;
        IDoctorRepository doctorRepository;
        IMedicalRecordRepository medicalRecordRepository;

        private RoomService roomService = new RoomService();
        private NotificationService notificationService = new NotificationService();

        public AppointmentService()
        {
            appointmentRepository = new AppointmentFileRepository();
            doctorRepository = new DoctorFileRepository();
        }

        public void DeleteAppointment(string id)
        {
            appointmentRepository.Delete(id);
        }

        public List<Appointment> GetAppointmentsByDoctor(Doctor doctor)
        {
            return appointmentRepository.GetByDoctorID(doctor.PersonalID);
        }

        public void Delete(string appointmentID)
        {
            appointmentRepository.Delete(appointmentID);
        }

        public string GenerateID()
        {
            List<int> existingIDs = appointmentRepository.GetExistingIDs();
            int newID = 1;
            while (true)
            {
                if (!existingIDs.Contains(newID))
                    return newID.ToString();
                newID += 1;
            }
        }

        public List<Appointment> GetAll()
        {
            return appointmentRepository.GetAll();
        }

        public void Save(Appointment appointment)
        {
            appointmentRepository.Save(appointment);
        }

        public bool IsDoctorAvaliableForAppointment(Appointment newAppointment)
        {
            List<Appointment> allAppointments = GetAll();

            foreach (Appointment appointment in allAppointments)
                if (!appointment.IsDoctorAvaliable(newAppointment))
                    return false;

            return true;
        }

        public bool IsPatientAvaliableForAppointment(Appointment newAppointment)
        {
            List<Appointment> allAppointments = GetAll();

            foreach (Appointment appointment in allAppointments)
                if (!appointment.IsPatientAvaliable(newAppointment))
                    return false;

            return true;
        }

        public string ScheduleAppointment(Appointment newAppointment)
        {
            if (!IsDoctorAvaliableForAppointment(newAppointment))
                return "Doktor je već zauzet u ovom terminu. Promenite trajanje ili odaberite drugi termin!";

            if (!IsPatientAvaliableForAppointment(newAppointment))
                return "Ovaj pacijent već ima zakazan pregled/operaciju u ovom terminu!";

            if (newAppointment.Room == null || newAppointment.Room.Equals(new Room()))
                return "Odaberite salu/ordinaciju!";

            appointmentRepository.Save(newAppointment);
            return "";
        }

        public Appointment ScheduleUrgentAppointmentWithRescheduling(Appointment newUrgentAppointment, OptionForRescheduling selectedOption)
        {
            DateTime timeForNewUrgentAppointment = selectedOption.NewUrgentAppointmentTime;
            newUrgentAppointment.DateTime = new DateTime(timeForNewUrgentAppointment.Year, timeForNewUrgentAppointment.Month, timeForNewUrgentAppointment.Day, 
                timeForNewUrgentAppointment.Hour, timeForNewUrgentAppointment.Minute, timeForNewUrgentAppointment.Second, timeForNewUrgentAppointment.Kind);

            newUrgentAppointment.IDDoctor = selectedOption.Option[0].Doctor.PersonalID;
            newUrgentAppointment.Doctor = doctorRepository.GetByID(newUrgentAppointment.IDDoctor);

            RescheduleAppointments(selectedOption);
            SetRoomForNewUrgentAppointment(newUrgentAppointment);
            appointmentRepository.Save(newUrgentAppointment);

            notificationService.NotifyDoctor(newUrgentAppointment);

            return newUrgentAppointment;
        }

        public void RescheduleAppointments(OptionForRescheduling selectedOption)
        {
            foreach (var moveAppointmnet in selectedOption.Option)
                RescheduleAppointment(moveAppointmnet.Appointment, moveAppointmnet.ToTime);
        }

        public void RescheduleAppointment(Appointment appointment, DateTime newTime)
        {
            notificationService.NotifyPatientAboutRescheduledAppointment(appointment, newTime);
            appointment.DateTime = newTime;
            appointmentRepository.Update(appointment);
        }

        public void ScheduleUrgentAppointment(Appointment newUrgentAppointment)
        {
            appointmentRepository.Save(newUrgentAppointment);
        }

        public List<Appointment> GetPatientsAppointments(MedicalRecord selectedPatient)
        {
            return appointmentRepository.GetByPatientID(selectedPatient.Patient.PersonalID);
        }

        public bool IsScheduledWithoutReschedulingAppointments(Appointment newUrgentAppointment, DoctorSpecialty doctorSpecialty)
        {
            SetDateTimeForNewUrgentAppointmentWithoutRescheduling(newUrgentAppointment);
            SetRoomForNewUrgentAppointment(newUrgentAppointment);
            List<Doctor> possibleDoctors = doctorRepository.GetBySpecialty(doctorSpecialty);

            for (int i = 0; i < 3; i++)
            {
                foreach (Doctor doctor in possibleDoctors)
                {
                    SetDoctorForNewAppointment(newUrgentAppointment, doctor);
                    if (SheduleIfDoctorIsAvaliable(newUrgentAppointment))
                        return true;
                }
                newUrgentAppointment.DateTime = newUrgentAppointment.DateTime.AddMinutes(30);
            }
            return false;
        }

        private bool SheduleIfDoctorIsAvaliable(Appointment newUrgentAppointment)
        {
            if (IsDoctorAvaliableForAppointment(newUrgentAppointment))
            {
                ScheduleUrgentAppointment(newUrgentAppointment);
                return true;
            }

            return false;
        }

        private void SetDoctorForNewAppointment(Appointment newUrgentAppointment, Doctor doctor)
        {
            newUrgentAppointment.IDDoctor = doctor.PersonalID;
            newUrgentAppointment.Doctor = doctor;
        }

        private void SetRoomForNewUrgentAppointment(Appointment newUrgentAppointment)
        {
            List<Room> avaliableRooms = roomService.GetAvaliableRoomsForNewAppointment(newUrgentAppointment);
            newUrgentAppointment.Room = avaliableRooms[0];
        }

        private void SetDateTimeForNewUrgentAppointmentWithoutRescheduling(Appointment newUrgentAppointment)
        {
            SetCurrentTime(newUrgentAppointment);

            if (newUrgentAppointment.DateTime.Minute >= 0 && newUrgentAppointment.DateTime.Minute <= 15)
                newUrgentAppointment.DateTime = newUrgentAppointment.DateTime.AddMinutes(-newUrgentAppointment.DateTime.Minute);
            else if (newUrgentAppointment.DateTime.Minute > 15 && newUrgentAppointment.DateTime.Minute < 30)
                newUrgentAppointment.DateTime = newUrgentAppointment.DateTime.AddMinutes(30 - newUrgentAppointment.DateTime.Minute);
            else if (newUrgentAppointment.DateTime.Minute >= 30 && newUrgentAppointment.DateTime.Minute <= 45)
                newUrgentAppointment.DateTime = newUrgentAppointment.DateTime.AddMinutes(-newUrgentAppointment.DateTime.Minute + 30);
            else
                newUrgentAppointment.DateTime = newUrgentAppointment.DateTime.AddMinutes(60 - newUrgentAppointment.DateTime.Minute);
        }

        private void SetCurrentTime(Appointment newUrgentAppointment)
        {
            var currentDateTime = DateTime.Now;
            newUrgentAppointment.DateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, currentDateTime.Hour,
                currentDateTime.Minute, currentDateTime.Second, currentDateTime.Kind);
            newUrgentAppointment.DateTime = newUrgentAppointment.DateTime.AddSeconds(-newUrgentAppointment.DateTime.Second);
        }

        public List<OptionForRescheduling> FindAllOptionsForRescheduling(Appointment newUrgentAppointment, DoctorSpecialty doctorsSpecialty)
        {
            List<OptionForRescheduling> options = new List<OptionForRescheduling>();
            List<Doctor> possibleDoctors = doctorRepository.GetBySpecialty(doctorsSpecialty);

            foreach (Doctor doctor in possibleDoctors)
            {
                SetDateTimeForNewUrgentAppointmentWithRecheduling(newUrgentAppointment);
                SetDoctorForNewAppointment(newUrgentAppointment, doctor);

                for (int i = 0; i < 3; i++)
                    CalculateNewOption(newUrgentAppointment, options);
            }

            return SortOptions(options);
        }

        private void CalculateNewOption(Appointment newUrgentAppointment, List<OptionForRescheduling> options)
        {
            List<Appointment> overlappingAppointments = GetOverlappingAppointments(newUrgentAppointment);
            if (HasAnyAppointmentStarted(overlappingAppointments))
            {
                newUrgentAppointment.DateTime = newUrgentAppointment.DateTime.AddMinutes(30);
                return;
            }

            OptionForRescheduling optionForRescheduling = new OptionForRescheduling();
            foreach (Appointment appointment in overlappingAppointments)
            {
                MoveAppointment moveAppointment = new MoveAppointment(appointment);
                optionForRescheduling.Option.Add(moveAppointment);
            }

            InsertIfOptionIsValid(optionForRescheduling, options, newUrgentAppointment);
            newUrgentAppointment.DateTime = newUrgentAppointment.DateTime.AddMinutes(30);
        }

        private List<OptionForRescheduling> SortOptions(List<OptionForRescheduling> options)
        {
            List<OptionForRescheduling> sortedList = options.OrderBy(o => o.NewUrgentAppointmentTime).ToList();
            return sortedList;
        }

        private void InsertIfOptionIsValid(OptionForRescheduling option, List<OptionForRescheduling> options, Appointment appointment)
        {
            if (!IsOptionValid(option, options))
                return;

            SetTimeForReschedulingAllAppointmentsInOption(option, appointment);
            options.Add(option);
        }

        private bool IsOptionValid(OptionForRescheduling option, List<OptionForRescheduling> options)
        {
            if (option.Option.Count() == 0)
                return false;

            foreach (OptionForRescheduling o in options)
                if (o.IsEqual(option))
                    return false;

            foreach (MoveAppointment moveAppointment in option.Option)
                if (moveAppointment.HasUrgentAppointment())
                    return false;

            return true;
        }

        private bool HasAnyAppointmentStarted(List<Appointment> overlappingAppointments)
        {
            foreach (Appointment appointment in overlappingAppointments)
                if (appointment.DateTime < DateTime.Now)
                    return true;

            return false;
        }

        public void SetTimeForReschedulingAllAppointmentsInOption(OptionForRescheduling option, Appointment newUrgentAppointment)
        {
            SetTimeForNewUrgentAppointmentInOption(option, newUrgentAppointment.DateTime);
            SortOption(option);

            foreach (MoveAppointment moveAppointment in option.Option)
                SetTimeForReschedulingAppointment(option, moveAppointment, newUrgentAppointment);
        }

        private void SetTimeForReschedulingAppointment(OptionForRescheduling option, MoveAppointment moveAppointment, Appointment newUrgentAppointment)
        {
            moveAppointment.ToTime = newUrgentAppointment.DateTime.AddHours(newUrgentAppointment.DurationInHours);

            while (true)
            {
                bool alreadyTaken = false;
                DateTime testStartTime = moveAppointment.ToTime;
                DateTime testEndTime = moveAppointment.ToTime.AddHours(moveAppointment.Appointment.DurationInHours);

                if (CheckIfOverlapWithAnyAppointment(testStartTime, testEndTime, moveAppointment.Doctor.PersonalID, option.Option))
                {
                    moveAppointment.ToTime = moveAppointment.ToTime.AddMinutes(30);
                    continue;
                }

                foreach (MoveAppointment existingMoveappointment in option.Option)
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

        private bool CheckIfOverlapWithAnyAppointment(DateTime startTime, DateTime endTime, string doctorID, ObservableCollection<MoveAppointment> option)
        {
            List<Appointment> appointments = GetAll();

            foreach (Appointment appointment in appointments)
            {
                bool exisits = false;
                if (appointment.IDDoctor.Equals(doctorID) && IsTimeOverlapping(appointment, startTime, endTime))
                {
                    foreach (MoveAppointment moveAppointment in option)
                    {
                        if (moveAppointment.Appointment.IDAppointment.Equals(appointment.IDAppointment))
                        {
                            exisits = true;
                            break;
                        }
                    }
                    if (exisits)
                        continue;
                    return true;
                }
            }

            return false;
        }

        private bool IsTimeOverlapping(Appointment appointment, DateTime startTime, DateTime endTime)
        {
            return appointment.DateTime < endTime && startTime < appointment.DateTime.AddHours(appointment.DurationInHours);
        }

        private void SortOption(OptionForRescheduling option)
        {
            List<MoveAppointment> sortedList = option.Option.OrderBy(o => o.Appointment.DateTime).ToList();
            option.Option = new ObservableCollection<MoveAppointment>(sortedList);
        }

        private void SetTimeForNewUrgentAppointmentInOption(OptionForRescheduling option, DateTime dateTime)
        {
            option.NewUrgentAppointmentTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Kind);
        }

        private List<Appointment> GetOverlappingAppointments(Appointment appointment)
        {
            List<Appointment> overlappingAppointments = new List<Appointment>();
            List<Appointment> allAppointmnets = GetAll();

            foreach (Appointment a in allAppointmnets)
                if (a.IDDoctor.Equals(appointment.IDDoctor))
                    if (a.IsOverlappingWith(appointment))
                        overlappingAppointments.Add(a);

            return overlappingAppointments;
        }

        private void SetDateTimeForNewUrgentAppointmentWithRecheduling(Appointment newUrgentAppointment)
        {
            SetCurrentTime(newUrgentAppointment);

            if (newUrgentAppointment.DateTime.Minute > 0 && newUrgentAppointment.DateTime.Minute < 30)
                newUrgentAppointment.DateTime = newUrgentAppointment.DateTime.AddMinutes(30 - newUrgentAppointment.DateTime.Minute);
            else if (newUrgentAppointment.DateTime.Minute > 30 && newUrgentAppointment.DateTime.Minute <= 59)
                newUrgentAppointment.DateTime = newUrgentAppointment.DateTime.AddMinutes(60 - newUrgentAppointment.DateTime.Minute);
        }

        public List<Appointment> GetPassedAppointmentsForPatient(String id)
        {
            return appointmentRepository.GetPassedAppointmentsForPatient(id);
        }

        public List<Appointment> GetByPatientID(string patientID)
        {
            return appointmentRepository.GetByPatientID(patientID);
        }

        public List<Appointment> SetAppointmentDataGrid(DoctorSpecialty doctorSpecialty)
        {
            doctorRepository = new DoctorFileRepository();
            medicalRecordRepository = new MedicalRecordFileRepository();
            List<Appointment> appointments = new List<Appointment>();

            foreach (Appointment appointmentToAdd in GetAll())
            {
                appointmentToAdd.Doctor = doctorRepository.GetByID(appointmentToAdd.IDDoctor);
                if (appointmentToAdd.Doctor.Specialty.Equals(doctorSpecialty))
                {
                    appointmentToAdd.PatientsRecord = medicalRecordRepository.GetByPatientID(appointmentToAdd.IDpatient);
                    appointments.Add(appointmentToAdd);
                }
            }
            return appointments;
        }

        public List<Appointment> SetParentAppointments(Appointment appointment)
        {
            doctorRepository = new DoctorFileRepository();
            medicalRecordRepository = new MedicalRecordFileRepository();
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment appointmentFromStorage in GetAll())
            {
                if (doctorRepository.GetByID(appointmentFromStorage.IDDoctor).Specialty.Equals(appointment.Doctor.Specialty))
                {
                    appointmentFromStorage.PatientsRecord = medicalRecordRepository.GetByPatientID(appointmentFromStorage.IDpatient);
                    appointments.Add(appointmentFromStorage);
                }
            }
            return appointments;
        }

        public List<Appointment> InitAppointments(string doctorId)
        {
            doctorRepository = new DoctorFileRepository();
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment a in GetAll())
            {
                if (a.IDDoctor.Equals(doctorId))
                {
                    a.Doctor = doctorRepository.GetByID(a.IDDoctor);
                    a.PatientsRecord = medicalRecordRepository.GetByPatientID(a.IDpatient);
                    appointments.Add(a);
                }
            }
            return appointments;
        }

    }
}
