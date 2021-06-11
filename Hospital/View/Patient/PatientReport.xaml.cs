using Hospital.Model;
using Hospital.Services;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using TextAlignment = iText.Layout.Properties.TextAlignment;
using System.Linq;
using iText.Kernel.Pdf.Canvas.Draw;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for PatientReport.xaml
    /// </summary>
    public partial class PatientReport : Page
    {
        private PatientTherapyNotificationService patientTherapyNotificationService = new PatientTherapyNotificationService();
        private List<PatientTherapyMedicineNotification> therapyNotifications;
        private DateTime MondayDay;
        public ObservableCollection<PatientTherapyMedicineNotification> Therapies
        {
            get;
            set;
        }
        public PatientReport()
        {
            InitializeComponent();
            this.DataContext = this;
            dataGridApp.Focus();
            MondayDay = SetDayToMonday(DateTime.Now);
            Therapies = new ObservableCollection<PatientTherapyMedicineNotification>();
            therapyNotifications = patientTherapyNotificationService.GetByPatientID();
            GetTherapies(0);
        }

        private void GetTherapies(int daysDifference)
        {
            Therapies.Clear();
            MondayDay = MondayDay.AddDays(daysDifference);
            label.Content = "Terapije za nedelju: " + MondayDay.ToShortDateString() + " - " + MondayDay.AddDays(6).ToShortDateString();
            foreach (PatientTherapyMedicineNotification therapy in therapyNotifications)
            {
                if (therapy.ToDate.Date>=MondayDay.Date && therapy.FromDate.Date<=MondayDay.Date.AddDays(7))
                {
                    Therapies.Add(therapy);
                }
            }
        }

        private DateTime SetDayToMonday(DateTime day)
        {
            int auxiliaryDay = (int)day.DayOfWeek;
            DateTime MondayDate;
            switch (auxiliaryDay)
            {
                case 2:
                    MondayDate = day.AddDays(-1);
                    break;
                case 3:
                    MondayDate = day.AddDays(-2);
                    break;
                case 4:
                    MondayDate = day.AddDays(-3);
                    break;
                case 5:
                    MondayDate = day.AddDays(-4);
                    break;
                case 6:
                    MondayDate = day.AddDays(-5);
                    break;
                case 0:
                    MondayDate = day.AddDays(-6);
                    break;
                default:
                    MondayDate = day;
                    break;
            }
            return MondayDate;
        }

        private void KeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                GenerateReport();
            }

            if (e.Key == Key.Escape)
            {
                this.NavigationService.GoBack();
            }

            if (e.Key == Key.LeftCtrl)
            {
                int daysDifference = -7;
                GetTherapies(daysDifference);
            }

            if (e.Key == Key.RightCtrl)
            {
                int daysDifference = 7;
                GetTherapies(daysDifference);
            }
        }

        private void GenerateReport()
        {
            PdfWriter writer = new PdfWriter("IzveštajPacijent.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            FontProgram fontProgram = FontProgramFactory.CreateFont();
            PdfFont font = PdfFontFactory.CreateFont(fontProgram, "Cp1250");
            document.SetFont(font);
            Paragraph header = new Paragraph(label.Content + "\n" + "\n").SetTextAlignment(TextAlignment.CENTER).SetFontSize(26);
            document.Add(header);
            foreach (PatientTherapyMedicineNotification therapy in Therapies)
            {
                Paragraph paragraph = new Paragraph().SetTextAlignment(TextAlignment.CENTER).SetFontSize(16);
                paragraph.Add("Naziv leka: " + therapy.Name + "\n");
                paragraph.Add("Vreme konzumiranja: " + therapy.Times + "\n");
                paragraph.Add("Trajanje terapije: " + therapy.Duration + "\n");
                paragraph.Add("Opis terapije: " + therapy.Text + "\n");
                document.Add(paragraph);
                document.Add(new LineSeparator(new SolidLine()));
            }
            document.Close();
        }
    }
}
