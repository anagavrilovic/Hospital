﻿using Hospital.Factory;
using Hospital.Model;
using Hospital.Repositories;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View.Secretary
{
    public partial class UrgentExaminationRescheduling : Page
    {
        public Appointment NewUrgentAppointment { get; set; }
        public DoctorSpecialty DoctorsSpecialty { get; set; }
        public ObservableCollection<OptionForRescheduling> Options { get; set; }
        public OptionForRescheduling SelectedOption { get; set; }

        public AppointmentService AppointmentService { get; set; }
        public MedicalRecordService MedicalRecordService { get; set; }
        public DoctorService DoctorService { get; set; }
        public RoomService RoomService { get; set; }


        public UrgentExaminationRescheduling(Appointment newUrgentAppointment, DoctorSpecialty doctorsSpecialty)
        {
            InitializeComponent();
            this.DataContext = this;
            this.NewUrgentAppointment = newUrgentAppointment;
            this.DoctorsSpecialty = doctorsSpecialty;
            InitializeEmptyObjects();
            FindAllOptions();
        }

        private void InitializeEmptyObjects()
        {
            Options = new ObservableCollection<OptionForRescheduling>();
            AppointmentService = new AppointmentService();
            MedicalRecordService = new MedicalRecordService();
            DoctorService = new DoctorService(new DoctorFileRepository());
            RoomService = new RoomService(new RoomFileRepository(), new AppointmentFileRepository());
        }

        private void FindAllOptions()
        {
            Options = new ObservableCollection<OptionForRescheduling>(AppointmentService.
                FindAllOptionsForRescheduling(NewUrgentAppointment, DoctorsSpecialty));
        }

        private void BtnPotvrdiClick(object sender, RoutedEventArgs e)
        {
            if (SelectedOption == null)
            {
                InformationBox informationBox = new InformationBox("Selektujte opciju za pomeranje već postojećih termina!");
                informationBox.ShowDialog();
                return;
            }

            NewUrgentAppointment = AppointmentService.ScheduleUrgentAppointmentWithRescheduling(NewUrgentAppointment, SelectedOption);

            var urgentAppointmentDetails = new UrgentExaminationDetails(NewUrgentAppointment);
            urgentAppointmentDetails.Show();

            NavigationService.Navigate(new UrgentExamination(new ViewModels.Secretary.UrgentExaminationViewModel(this.NavigationService)));
        }

        private void BtnOdustaniClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UrgentExamination(new ViewModels.Secretary.UrgentExaminationViewModel(this.NavigationService)));
        }
    }
}
