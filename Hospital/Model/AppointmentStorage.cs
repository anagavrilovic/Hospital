using Hospital.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace Hospital
{

    public class AppointmentStorage
    {

        private String fileName;

        public AppointmentStorage(String file = "appointments.json")
        {
            this.fileName = file;

        }

        public ObservableCollection<Appointment> GetAll()
        {
            ObservableCollection<Appointment> appointment;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                appointment = (ObservableCollection<Appointment>)serializer.Deserialize(file, typeof(ObservableCollection<Appointment>));
            }

            return appointment;
        }

        public void Save(Appointment parameter1)
        {
            ObservableCollection<Appointment> appointment = GetAll();

            if (appointment == null)
                appointment = new ObservableCollection<Appointment>();

            appointment.Add(parameter1);
            DoSerialization(appointment);
        }

        public bool IsDoctorAvaliableForAppointment(Appointment newAppointment)
        {
            ObservableCollection<Appointment> allAppointments = GetAll();
            foreach (Appointment appointment in allAppointments)
            {
                if (!appointment.IsDoctorAvaliable(newAppointment))
                    return false;
            }

            return true;
        }

        public bool IsPatientAvaliableForAppointment(Appointment newAppointment)
        {
            ObservableCollection<Appointment> allAppointments = GetAll();
            foreach (Appointment appointment in allAppointments)
            {
                if (!appointment.IsPatientAvaliable(newAppointment))
                    return false;
            }

            return true;
        }

        public bool CheckIfOverlap(DateTime startTime, DateTime endTime, string doctorID, ObservableCollection<MoveAppointment> option)
        {
            ObservableCollection<Appointment> appointments = GetAll();

            foreach (Appointment app in appointments)
            {
                bool exisits = false;
                if (app.IDDoctor.Equals(doctorID))
                    if (app.DateTime < endTime && startTime < app.DateTime.AddHours(app.DurationInHours))
                    {
                        foreach (var op in option)
                        {
                            if (op.Appointment.IDAppointment.Equals(app.IDAppointment))
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

        public ObservableCollection<Appointment> GetOverlappingAppointments(Appointment appointment)
        {
            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
            ObservableCollection<Appointment> allAppointmnets = GetAll();

            foreach (Appointment app in allAppointmnets)
            {
                if (app.IDDoctor.Equals(appointment.IDDoctor))
                    if (app.DateTime < appointment.DateTime.AddHours(appointment.DurationInHours) && appointment.DateTime < app.DateTime.AddHours(app.DurationInHours))
                    {
                        appointments.Add(app);
                    }
            }

            return appointments;
        }

        public Appointment GetByID(String id)
        {
            ObservableCollection<Appointment> appointments = GetAll();
            foreach (Appointment app in appointments)
            {
                if (app.IDAppointment.Equals(id))
                {
                    return app;
                }
            }

            return new Appointment();
        }

        public void RescheduleAppointments(ObservableCollection<MoveAppointment> option)
        {
            foreach (var moveAppointmnet in option)
            {
                RescheduleAppointment(moveAppointmnet.Appointment.IDAppointment, moveAppointmnet.ToTime);
            }
        }

        public void RescheduleAppointment(string idAppointment, DateTime newTime)
        {
            ObservableCollection<Appointment> allAppointments = GetAll();

            foreach (var appointment in allAppointments)
            {
                if (appointment.IDAppointment.Equals(idAppointment))
                {
                    GenerateNotification(appointment, newTime);
                    appointment.DateTime = newTime;
                    break;
                }
            }

            DoSerialization(allAppointments);
        }

        private void GenerateNotification(Appointment appointment, DateTime newTime)
        {
            StringBuilder stringBuilder = new StringBuilder("");
            string title = "";
            string content = "";
            
            if (appointment.Type.Equals(AppointmentType.urgentExamination) || appointment.Type.Equals(AppointmentType.examination))
            {
                title = "Promena zakazanog termina pregleda";

                stringBuilder.Append("Poštovani, ").AppendLine().AppendLine().Append("Obaveštavamo Vas da je Vaš pregled koji se trebao održati dana ").
                    Append(appointment.DateTime.ToString("dd.MM.yyyy.")).Append(" u ").Append(appointment.DateTime.ToString(appointment.DateTime.ToString("HH:mm"))).
                    Append(" časova, odložen na ").Append(newTime.ToString("dd.MM.yyyy.")).Append(" u ").Append(appointment.DateTime.ToString(newTime.ToString("HH:mm"))).
                    Append(" časova, jer se u vreme Vašeg termina dogodio hitan slučaj.").AppendLine().AppendLine().
                    Append("Za više informacija pozovite našeg sekretara na broj 06485625952 ili nam pišite e-mailom.").AppendLine().AppendLine().
                    Append("Srdačan pozdrav, Vaša ZDRAVO bolnica");
                content = stringBuilder.ToString();
            }
                
            else if (appointment.Type.Equals(AppointmentType.urgentOperation) || appointment.Type.Equals(AppointmentType.operation))
            {
                title = "Promena zakazanog termina operacije";

                stringBuilder.Append("Poštovani, ").AppendLine().AppendLine().Append("Obaveštavamo Vas da je Vaša operacija koja se trebala održati dana ").
                    Append(appointment.DateTime.ToString("dd.MM.yyyy.")).Append(" u ").Append(appointment.DateTime.ToString(appointment.DateTime.ToString("HH:mm"))).
                    Append(" časova, odložena na ").Append(newTime.ToString("dd.MM.yyyy.")).Append(" u ").Append(appointment.DateTime.ToString(newTime.ToString("HH:mm"))).
                    Append(" časova, jer se u vreme Vašeg termina dogodio hitan slučaj.").AppendLine().AppendLine().
                    Append("Za više informacija pozovite našeg sekretara na broj 06485625952 ili nam pišite e-mailom.").AppendLine().AppendLine().
                    Append("Srdačan pozdrav, Vaša ZDRAVO bolnica");
                content = stringBuilder.ToString();
            }

            Notification notification = new Notification(title, content);
            NotificationsUsers notificationsUsers = new NotificationsUsers(notification.Id, new MedicalRecordStorage().GetUsernameByIDPatient(appointment.IDpatient));
            NotificationStorage notificationStorage = new NotificationStorage();
            notificationStorage.SaveNotification(notification);
            notificationStorage.SaveNotificationUser(notificationsUsers);
        }

        public void NotifyDoctor(Appointment appointment)
        {
            StringBuilder stringBuilder = new StringBuilder("");
            string title = "";
            string content = "";

            if (appointment.Type.Equals(AppointmentType.urgentExamination) || appointment.Type.Equals(AppointmentType.examination))
            {
                title = "HITAN PREGLED";

                stringBuilder.Append("Imate hitan pregled u ").Append(appointment.DateTime.ToString("HH:mm")).Append(" , ordinacija \"").Append(appointment.Room.Name).Append("\".");
                content = stringBuilder.ToString();
            }

            else if (appointment.Type.Equals(AppointmentType.urgentOperation) || appointment.Type.Equals(AppointmentType.operation))
            {
                title = "HITNA OPERACIJA";

                stringBuilder.Append("Imate hitanu operaciju u ").Append(appointment.DateTime.ToString("HH:mm")).Append(" , sala \"").Append(appointment.Room.Name).Append("\".");
                content = stringBuilder.ToString();
            }

            Notification notification = new Notification(title, content);
            NotificationsUsers notificationsUsers = new NotificationsUsers(notification.Id, new DoctorStorage().GetUsernameByIDDoctor(appointment.IDDoctor));

            NotificationStorage notificationStorage = new NotificationStorage();
            notificationStorage.SaveNotification(notification);
            notificationStorage.SaveNotificationUser(notificationsUsers);
        }

        public ObservableCollection<Appointment> GetByPatient(String id)
        {
            ObservableCollection<Appointment> allAppointments = GetAll();
            ObservableCollection<Appointment> patientsApppointments = new ObservableCollection<Appointment>();
            
            foreach(Appointment appointment in allAppointments)
            {
                if (appointment.IDpatient.Equals(id))
                    patientsApppointments.Add(appointment);
            }

            return patientsApppointments;
        }

        public Boolean Delete(string id)
        {
            ObservableCollection<Appointment> appointments = GetAll();
            foreach (Appointment r in appointments)
            {
                if (r.IDAppointment.Equals(id))
                {
                    appointments.Remove(r);
                    DoSerialization(appointments);
                    return true;
                }
            }
            return false;
        }

        public Appointment GetOne(string id)
        {
            ObservableCollection<Appointment> appointment = GetAll();
            foreach (Appointment a in appointment)
            {
                if (a.IDAppointment.Equals(id))
                {
                    return a;
                }
            }

            return null;
        }

        public void DoSerialization(ObservableCollection<Appointment> appointment)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, appointment);
            }
        }

        public String GetNewID()
        {
            ObservableCollection<Appointment> apps;
            using (StreamReader sr = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                apps = GetAll();
                apps = JsonConvert.DeserializeObject<ObservableCollection<Appointment>>(sr.ReadToEnd());
            }

            int retVal = 1;
            if (apps == null || apps.Count == 0)
            {
                return retVal.ToString();
            }


            List<int> lista = new List<int>();
            foreach (Appointment app in apps)
            {
                int x = Int32.Parse(app.IDAppointment);
                lista.Add(x);
            }

            while (true)
            {
                if (!lista.Contains(retVal))
                {
                    break;
                }
                else
                {
                    retVal++;
                }
            }
            return retVal.ToString();
        }

        public ObservableCollection<Appointment> GetAppointmentsByDoctor(String requestedDoctorsID)
        {
            ObservableCollection<Appointment> allApointments = GetAll();
            ObservableCollection<Appointment> appointmentsOfRequestedDoctor = new ObservableCollection<Appointment>();

            foreach(Appointment a in allApointments)
                if (a.IsDoctorInAppointment(requestedDoctorsID))
                    appointmentsOfRequestedDoctor.Add(a);

            return appointmentsOfRequestedDoctor;
        }

        public Boolean ExistByTime(DateTime dt, String idDoctor)
        {
            ObservableCollection<Appointment> appointment = GetAll();
            if (appointment == null) return false;
            foreach (Appointment a in appointment)
            {
                if (a.DateTime == dt && a.IDDoctor.Equals(idDoctor))
                {
                    return true;
                }
            }

            return false;
        }

        public int GetNumberOfAppointmentsForDoctor(string id)
        {
            ObservableCollection<Appointment> appointment = GetAll();
            int number = 0;
            if (appointment == null) return 0;
            foreach (Appointment a in appointment)
            {
                if (a.IDDoctor.Equals(id))
                {
                    number++;
                }
            }

            return number;
        }


    }
}