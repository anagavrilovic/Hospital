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
using System.Linq;
using System.Text;

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
            appointments = appointments.OrderBy(a => a.DateTime).ToList();

            using (PdfDocument document = new PdfDocument())
            {
                PdfPage page = document.Pages.Add();

                PdfGraphics graphics = page.Graphics;
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 7);

                // HEADER
                graphics.DrawString("Adresa: Hajduk Veljkova 5", font, PdfBrushes.Black, new PointF(0, 19));
                graphics.DrawString("21000 Novi Sad, Srbija", font, PdfBrushes.Black, new PointF(0, 27));
                graphics.DrawString("Kontakt telefon: 021568136", font, PdfBrushes.Black, new PointF(0, 36));
                graphics.DrawString("E-mail adresa: nszdravo@gmail.com", font, PdfBrushes.Black, new PointF(0, 44));


                // SLIKA
                PdfImage image = PdfImage.FromFile("../../Icon/Secretary/logo.png");
                RectangleF bounds = new RectangleF(170, 12, 150, 50);
                page.Graphics.DrawImage(image, bounds);

                // NASLOV
                font = new PdfStandardFont(PdfFontFamily.Helvetica, 17);
                graphics.DrawString("Izveštaj o zauzetosti", font, PdfBrushes.Black, new PointF(170, 100));
                graphics.DrawString("lekara pojedinca za određeni vremenski period", font, PdfBrushes.Black, new PointF(70, 120));


                // SADRZAJ
                font = new PdfStandardFont(PdfFontFamily.Helvetica, 9);
                StringBuilder stringBuilder = new StringBuilder("");
                stringBuilder.Append("Prikaz zakazanih termina kod lekara ").Append(SelectedDoctor.ToString()).Append(" za vremenski period od ")
                    .Append(DateBegin.ToString("dd.MM.yyyy.")).Append(" do ").Append(DateEnd.ToString("dd.MM.yyyy."));
                graphics.DrawString(stringBuilder.ToString(), font, PdfBrushes.Black, new PointF(30, 180));


                // TABELA
                PdfLightTable pdfLightTable = new PdfLightTable();
                DataTable table = new DataTable();

                //Include columns to the DataTable.
                table.Columns.Add("Datum i vreme");
                table.Columns.Add("Trajanje termina");
                table.Columns.Add("Pacijent");
                table.Columns.Add("Prostorija");
                table.Columns.Add("Tip");

                table.Rows.Add(new string[] { "Datum i vreme", "Trajanje termina (u satima)", "Pacijent", "Prostorija", "Tip" });

                int examinations = 0;
                int operations = 0;
                int urgentExaminations = 0;
                int urgentOperations = 0;
                foreach (Appointment appointment in appointments)
                {
                    string type = "";
                    if (appointment.Type.Equals(AppointmentType.examination))
                    {
                        type = "pregled";
                        examinations++;
                    }                        
                    else if (appointment.Type.Equals(AppointmentType.urgentExamination))
                    {
                        type = "hitan pregled";
                        urgentExaminations++;
                    }            
                    else if (appointment.Type.Equals(AppointmentType.operation))
                    {
                        type = "operacija";
                        operations++;
                    }
                    else if (appointment.Type.Equals(AppointmentType.urgentOperation))
                    {
                        type = "hitna operacija";
                        urgentOperations++;
                    }    

                    string patientName = MedicalRecordService.GetByPatientId(appointment.IDpatient).Patient.FirstName + " " +
                        MedicalRecordService.GetByPatientId(appointment.IDpatient).Patient.LastName;

                    table.Rows.Add(new string[] { appointment.DateTime.ToString("dd.MM.yyyy. HH:mm"),
                                                  appointment.DurationInHours.ToString(),
                                                  patientName,
                                                  appointment.Room.Name,
                                                  type});
                }


                pdfLightTable.DataSource = table;
                pdfLightTable.Draw(page, new PointF(0, 240));

                stringBuilder = new StringBuilder("");
                stringBuilder.Append("U ovom vremenskom periodu, lekar ").Append(SelectedDoctor.ToString()).Append(" ima zakazano ").Append
                    (examinations.ToString()).Append(" pregleda, ").Append(operations.ToString()).Append(" operacija, ").Append(urgentExaminations.ToString())
                    .Append(" hitnih").Append(" pregleda");
                graphics.DrawString(stringBuilder.ToString(), font, PdfBrushes.Black, new PointF(30, 195));

                stringBuilder = new StringBuilder("");
                stringBuilder.Append(" i ").Append(urgentOperations.ToString()).Append(" hitnih operacija.");
                graphics.DrawString(stringBuilder.ToString(), font, PdfBrushes.Black, new PointF(0, 210));

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

            if(DateBegin > DateEnd)
            {
                InformationBox informationBox = new InformationBox("Početni dan ne može biti nakon krajnjeg!");
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
