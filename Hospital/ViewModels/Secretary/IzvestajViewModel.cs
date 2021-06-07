using Hospital.Commands.Secretary;
using Hospital.Services;
using Hospital.View.Secretary;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Navigation;

namespace Hospital.ViewModels.Secretary
{
    public class IzvestajViewModel : ViewModel
    {
        #region Properties

        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public Model.Doctor SelectedDoctor { get; set; }
        public ObservableCollection<Model.Doctor> AllDoctors { get; set; }
        public ICollectionView DoctorsCollection { get; set; }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set 
            { 
                searchText = value;
                OnPropertyChanged("SearchText");
                CollectionViewSource.GetDefaultView(AllDoctors).Refresh();
            }
        }


        public DoctorService DoctorService { get; set; }
        public AppointmentService AppointmentService { get; set; }
        public MedicalRecordService MedicalRecordService { get; set; }
        public NavigationService NavigationService { get; set; }

        #endregion

        #region Konstruktor
        public IzvestajViewModel(NavigationService navigationService)
        {
            this.NavigationService = navigationService;
            InitializeEmptyProperties();
            LoadDoctors();
            GenerateReportCommand = new RelayCommand(Execute_GenerateReportCommand, CanExecuteCommands);
        }
        #endregion

        #region Metode

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
            if (string.IsNullOrEmpty(SearchText))
                return true;
            else
                return ((obj.ToString()).IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private bool Validate()
        {
            if (SelectedDoctor == null)
            {
                InformationBox informationBox = new InformationBox("Izaberite lekara!");
                informationBox.Show();
                return false;
            }

            if (DateBegin > DateEnd)
            {
                InformationBox informationBox = new InformationBox("Početni dan ne može biti nakon krajnjeg!");
                informationBox.Show();
                return false;
            }

            return true;
        }

        #endregion

        #region Komande

        public RelayCommand GenerateReportCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        #endregion

        #region Akcije

        public void Execute_GenerateReportCommand(object obj)
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

        public bool CanExecuteCommands(object obj)
        {
            return true;
        }

        #endregion
    }
}
