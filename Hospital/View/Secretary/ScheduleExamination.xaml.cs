﻿using Hospital.Factory;
using Hospital.Repositories;
using Hospital.Services;
using Hospital.View.Secretary;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.View
{
    public partial class ScheduleExamination : Page
    {
        private const double minDurationForAppointment = 0.5;
        private const double maxDurationForAppointment = 24;
        private const double timeSlot = 0.5;

        public Appointment NewAppointment { get; set; }
        public ObservableCollection<Room> AvaliableRooms { get; set; }
        public ObservableCollection<string> PossibleDuration { get; set; }

        public AppointmentService AppointmentService { get; set; }
        public RoomService RoomService { get; set; }


        public ScheduleExamination(Hospital.Model.Doctor selectedDoctor, MedicalRecord selectedPatient, DateTime dateTimeForNewAppointment)
        {
            InitializeComponent();
            this.DataContext = this;
            InitializeAllProperties();
            ShowPossibleDuration();
            InitializeNewAppointment(selectedDoctor, selectedPatient, dateTimeForNewAppointment);
        }

        private void InitializeAllProperties()
        {
            NewAppointment = new Appointment();
            AvaliableRooms = new ObservableCollection<Room>();
            PossibleDuration = new ObservableCollection<string>();
            AppointmentService = new AppointmentService();
            RoomService = new RoomService(new RoomFileRepository(), new AppointmentFileRepository());
        }

        private void ShowPossibleDuration()
        {
            double duration = minDurationForAppointment;
            while (duration <= maxDurationForAppointment)
            {
                PossibleDuration.Add(duration.ToString());
                duration += timeSlot;
            }
        }

        private void InitializeNewAppointment(Model.Doctor selectedDoctor, MedicalRecord selectedPatient, DateTime dateTimeForNewAppointment)
        {
            SetPatientForNewAppointment(selectedPatient);
            SetDoctorForNewAppointment(selectedDoctor);
            NewAppointment.DateTime = dateTimeForNewAppointment;
            NewAppointment.IDAppointment = AppointmentService.GetNewID();
        }

        private void SetPatientForNewAppointment(MedicalRecord selectedPatient)
        {
            NewAppointment.PatientsRecord = selectedPatient;
            NewAppointment.IDpatient = selectedPatient.Patient.PersonalID;
        }

        private void SetDoctorForNewAppointment(Model.Doctor selectedDoctor)
        {
            NewAppointment.Doctor = selectedDoctor;
            NewAppointment.IDDoctor = selectedDoctor.PersonalID;
        }

        private void PotvrdiClick(object sender, RoutedEventArgs e)
        {
            if(ComboBoxTrajanje.SelectedIndex == -1 || ComboBoxType.SelectedIndex == -1 || ComboBoxRoom.SelectedIndex == -1)
            {
                InformationBox informationBox = new InformationBox("Popunite sve informacije o novom terminu!");
                informationBox.ShowDialog();
                return;
            }

            string message = AppointmentService.ScheduleAppointment(NewAppointment);

            InformationBox informationBox1 = new InformationBox(message);
            informationBox1.ShowDialog();

            if(!message.Equals("Termin uspešno zakazan!"))
                return;

            NavigationService.Navigate(new Timetable(NewAppointment.Doctor));
        }

        private void OdustaniClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Timetable(NewAppointment.Doctor));
        }

        private void DurationSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadAvaliableRooms();
        }
            
        private void TypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NewAppointment.Type = (AppointmentType)ComboBoxType.SelectedIndex;
            LoadAvaliableRooms();
        }

        private void LoadAvaliableRooms()
        {
            AvaliableRooms = new ObservableCollection<Room>(RoomService.GetAvaliableRoomsForNewAppointment(NewAppointment));
            ComboBoxRoom.ItemsSource = AvaliableRooms;
        }
        
    }
}
