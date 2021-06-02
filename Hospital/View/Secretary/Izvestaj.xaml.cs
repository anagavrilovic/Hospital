using Hospital.Services;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Tables;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using System.Drawing;
using System.Collections.Generic;
using Syncfusion.Pdf.Graphics;

namespace Hospital.View.Secretary
{
    public partial class Izvestaj : Page
    {
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public Model.Doctor SelectedDoctor { get; set; }
        public ObservableCollection<Model.Doctor> AllDoctors { get; set; }
        public ICollectionView DoctorsCollection { get; set; }

        public DoctorService DoctorService { get; set; }
        public AppointmentService AppointmentService { get; set; }
        public MedicalRecordService MedicalRecordService { get; set; }

        public Izvestaj()
        {
            InitializeComponent();
            this.DataContext = this;
            InitializeEmptyProperties();
            LoadDoctors();
        }

        private void InitializeEmptyProperties()
        {
            DateBegin = DateTime.Now;
            DateEnd = DateTime.Now;
            DoctorService = new DoctorService();
            AppointmentService = new AppointmentService();
            MedicalRecordService = new MedicalRecordService();
        }

        private void LoadDoctors()
        {
            AllDoctors = new ObservableCollection<Model.Doctor>(DoctorService.GetAll());
            DoctorsCollection = CollectionViewSource.GetDefaultView(AllDoctors);
            DoctorsCollection.Filter = CustomFilterDoctor;
        }

        private bool CustomFilterDoctor(object obj)
        {
            if (string.IsNullOrEmpty(DoctorsFilter.Text))
                return true;
            else
                return ((obj.ToString()).IndexOf(DoctorsFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void GenerateReportClick(object sender, RoutedEventArgs e)
        {
            if (!Validate())
                return;

            List<Appointment> appointments = AppointmentService.GetAppointmentsByDoctorInSelectedPeriod(SelectedDoctor, DateBegin, DateEnd);

            using (PdfDocument document = new PdfDocument())
            {
                PdfPage page = document.Pages.Add();

                PdfLightTable pdfLightTable = new PdfLightTable();
                DataTable table = new DataTable();

                //Include columns to the DataTable.
                table.Columns.Add("Datum i vreme");
                table.Columns.Add("Trajanje termina");
                table.Columns.Add("Pacijent");
                table.Columns.Add("Prostorija");
                table.Columns.Add("Tip");

                table.Rows.Add(new string[] { "Datum i vreme", "Trajanje termina", "Pacijent", "Prostorija", "Tip" });

                foreach (Appointment appointment in appointments)
                {
                    table.Rows.Add(new string[] { appointment.DateTime.ToString("dd.MM.yyyy. HH:mm"),
                                                  appointment.DurationInHours.ToString(),
                                                  MedicalRecordService.GetByPatientId(appointment.IDpatient).ToString(),
                                                  appointment.Room.Name,
                                                  appointment.Type.ToString()});
                }


                pdfLightTable.DataSource = table;
                pdfLightTable.Draw(page, new PointF(0, 0));

                document.Save("../../Reports/SecretaryReport.pdf");
                document.Close(true);
            }

            InformationBox informationBox = new InformationBox("Izveštaj se nalazi u folderu Reports.");
            informationBox.Show();
        }

        private bool Validate()
        {
            if(SelectedDoctor == null)
            {
                InformationBox informationBox = new InformationBox("Izaberite lekara!");
                informationBox.Show();
                return false;
            }

            return true;
        }

        private void CancelBtnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Analitika());
        }

        private void DoctorFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListBoxDoctors.ItemsSource).Refresh();
        }
    }
}
