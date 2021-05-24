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

        private RoomService roomService = new RoomService();

        public AppointmentService()
        {
            appointmentRepository = new AppointmentFileRepository();
            doctorRepository = new DoctorFileRepository();
        }

        public void DeleteAppointment(string id)
        {
            appointmentRepository.Delete(id);
        }

        public ObservableCollection<Appointment> GetAppointmentsByDoctor(Doctor doctor)
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

        public ObservableCollection<Appointment> GetAll()
        {
            return appointmentRepository.GetAll();
        }

        public void Save(Appointment appointment)
        {
            appointmentRepository.Save(appointment);
        }

        public bool IsDoctorAvaliableForAppointment(Appointment newAppointment)
        {
            ObservableCollection<Appointment> allAppointments = GetAll();

            foreach (Appointment appointment in allAppointments)
                if (!appointment.IsDoctorAvaliable(newAppointment))
                    return false;

            return true;
        }

        public bool IsPatientAvaliableForAppointment(Appointment newAppointment)
        {
            ObservableCollection<Appointment> allAppointments = GetAll();

            foreach (Appointment appointment in allAppointments)
                if (!appointment.IsPatientAvaliable(newAppointment))
                    return false;

            return true;
        }

        public void ScheduleAppointment(Appointment newAppointment)
        {
            if (!IsDoctorAvaliableForAppointment(newAppointment))
                return;

            if (!IsPatientAvaliableForAppointment(newAppointment))
                return;

            appointmentRepository.Save(newAppointment);
        }

        public void ScheduleUrgentAppointment(Appointment newUrgentAppointment)
        {
            appointmentRepository.Save(newUrgentAppointment);
        }

        public ObservableCollection<Appointment> GetPatientsAppointments(MedicalRecord selectedPatient)
        {
            return appointmentRepository.GetByPatientID(selectedPatient.Patient.PersonalID);
        }

        public bool IsScheduledWithoutReschedulingAppointments(Appointment newUrgentAppointment, DoctorSpecialty doctorSpecialty)
        {
            SetDateTimeForNewUrgentAppointmentWithoutRescheduling(newUrgentAppointment);
            SetRoomForNewUrgentAppointment(newUrgentAppointment);
            ObservableCollection<Doctor> possibleDoctors = doctorRepository.GetBySpecialty(doctorSpecialty);

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
            ObservableCollection<Room> avaliableRooms = roomService.GetAvaliableRoomsForNewAppointment(newUrgentAppointment);
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

        public ObservableCollection<OptionForRescheduling> FindAllOptionsForRescheduling(Appointment newUrgentAppointment, DoctorSpecialty doctorsSpecialty)
        {
            ObservableCollection<OptionForRescheduling> options = new ObservableCollection<OptionForRescheduling>();
            ObservableCollection<Doctor> possibleDoctors = doctorRepository.GetBySpecialty(doctorsSpecialty);

            foreach (Doctor doctor in possibleDoctors)
            {
                SetDateTimeForNewUrgentAppointmentWithRecheduling(newUrgentAppointment);
                SetDoctorForNewAppointment(newUrgentAppointment, doctor);

                for (int i = 0; i < 3; i++)
                {
                    ObservableCollection<Appointment> overlappingAppointments = GetOverlappingAppointments(newUrgentAppointment);
                    if (HasAppointmentStarted(overlappingAppointments))
                    {
                        newUrgentAppointment.DateTime = newUrgentAppointment.DateTime.AddMinutes(30);
                        continue;
                    }

                    OptionForRescheduling optionForRescheduling = new OptionForRescheduling();
                    foreach (Appointment appointment in overlappingAppointments)
                    {
                        MoveAppointment moveAppointment = new MoveAppointment(appointment);
                        optionForRescheduling.Option.Add(moveAppointment);
                    }

                    InsertIfOptionDoesntExist(optionForRescheduling, options, newUrgentAppointment);
                    newUrgentAppointment.DateTime = newUrgentAppointment.DateTime.AddMinutes(30);
                }
            }

            return SortOptions(options);
        }

        private ObservableCollection<OptionForRescheduling> SortOptions(ObservableCollection<OptionForRescheduling> options)
        {
            List<OptionForRescheduling> sortedList = options.OrderBy(o => o.NewUrgentAppointmentTime).ToList();
            return new ObservableCollection<OptionForRescheduling>(sortedList);
        }

        private void InsertIfOptionDoesntExist(OptionForRescheduling option, ObservableCollection<OptionForRescheduling> options, Appointment appointment)
        {
            if (option.Option.Count() == 0)
                return;

            foreach (OptionForRescheduling o in options)
                if (o.IsEqual(option))
                    return;

            foreach (MoveAppointment moveAppointment in option.Option)
                if (moveAppointment.Appointment.Type.Equals(AppointmentType.urgentExamination) || moveAppointment.Appointment.Type.Equals(AppointmentType.urgentOperation))
                    return;

            option.SetTimeForRescheduling(appointment);
            options.Add(option);
        }

        private bool HasAppointmentStarted(ObservableCollection<Appointment> overlappingAppointments)
        {
            foreach (Appointment appointment in overlappingAppointments)
                if (appointment.DateTime < DateTime.Now)
                    return true;

            return false;
        }

        private ObservableCollection<Appointment> GetOverlappingAppointments(Appointment appointment)
        {
            ObservableCollection<Appointment> overlappingAppointments = new ObservableCollection<Appointment>();
            ObservableCollection<Appointment> allAppointmnets = GetAll();

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
    }
}
