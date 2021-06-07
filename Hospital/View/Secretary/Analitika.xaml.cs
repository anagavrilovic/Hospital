﻿using Hospital.Services;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Hospital.View.Secretary
{
    public partial class Analitika : Page
    {
        private DoctorService doctorService = new DoctorService();
        private AppointmentService appointmentService = new AppointmentService();

        public Analitika()
        {
            InitializeComponent();
            this.DataContext = this;
            DrawCharts();
        }

        private void DrawCharts()
        {
            SetPieChart();
            DrawWorkTimeChart();
        }

        #region Grafikon zakazanih pregleda lekara

        public SeriesCollection DoctorsSeriesCollection { get; set; }
        public string[] DoctorsLabels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private void DrawWorkTimeChart()
        {
            int[] periodsFirstMonth = getPeriodsFirstMonth();
            int[] periodsSecondMonth = getPeriodsSecondMonth();
            int[] periodsThirdMonth = getPeriosThirdMonth();

            DoctorsSeriesCollection = new SeriesCollection();
            DoctorsSeriesCollection.Add(new ColumnSeries { Title = "jun", Values = new ChartValues<int> { periodsFirstMonth[0], periodsFirstMonth[1], periodsFirstMonth[2] } });
            DoctorsSeriesCollection.Add(new ColumnSeries { Title = "jul", Values = new ChartValues<int> { periodsSecondMonth[0], periodsSecondMonth[1], periodsSecondMonth[2] } });
            DoctorsSeriesCollection.Add(new ColumnSeries { Title = "avgust", Values = new ChartValues<int> { periodsThirdMonth[0], periodsThirdMonth[1], periodsThirdMonth[2] } });

            List<Model.Doctor> doctors = doctorService.GetAll();
            DoctorsLabels = new string[doctors.Count];
            for (int i = 0; i < doctors.Count; i++)
                DoctorsLabels[i] = doctors[i].ToString();

            Formatter = value => value.ToString("N");
        }

        private int[] getPeriosThirdMonth()
        {
            int[] periods = { 0, 0, 0 };
            int i = 0;
            foreach(Model.Doctor d in doctorService.GetAll())
            {
                foreach(Appointment a in appointmentService.GetAll())
                    if(a.IDDoctor.Equals(d.PersonalID) && a.DateTime.Month.Equals(DateTime.Now.AddMonths(2).Month))
                        periods[i]++;
                i++;
            }
            return periods;
        }

        private int[] getPeriodsSecondMonth()
        {
            int[] periods = { 0, 0, 0 };
            int i = 0;
            foreach (Model.Doctor d in doctorService.GetAll())
            {
                foreach (Appointment a in appointmentService.GetAll())
                    if (a.IDDoctor.Equals(d.PersonalID) && a.DateTime.Month.Equals(DateTime.Now.AddMonths(1).Month))
                        periods[i]++;
                i++;
            }
            return periods;
        }

        private int[] getPeriodsFirstMonth()
        {
            int[] periods = { 0, 0, 0 };
            int i = 0;
            foreach (Model.Doctor d in doctorService.GetAll())
            {
                foreach (Appointment a in appointmentService.GetAll())
                    if (a.IDDoctor.Equals(d.PersonalID) && a.DateTime.Month.Equals(DateTime.Now.Month))
                        periods[i]++;

                i++;
            }
            return periods;
        }

        #endregion

        #region PieChart
        public Func<ChartPoint, string> PointLabel { get; set; }
        public void SetPieChart()
        {
            PointLabel = chartPoint => string.Format("{0}({1:P})", chartPoint.Y, chartPoint.Participation);
        }

        #endregion

        private void GenerateReportClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Izvestaj());
        }
    }
}
