using Hospital.Factory;
using Hospital.Services;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ReportsPatterns
{
    public class SecretaryReport : ReportGenerator
    {
        public List<Appointment> Appointments { get; set; }
        public MedicalRecordService MedicalRecordService { get; set; }

        private Model.Doctor SelectedDoctor;
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }

        private int examinations = 0;
        private int operations = 0;
        private int urgentExaminations = 0;
        private int urgentOperations = 0;
   

        public SecretaryReport(Model.Doctor doctor, List<Appointment> appointments, DateTime begin, DateTime end)
        {
            SelectedDoctor = doctor;
            Appointments = appointments;
            MedicalRecordService = new MedicalRecordService(new MedicalRecordFileFactory(), new AppointmentFileFactory(), new HospitalTreatmentFileFactory());
            DateBegin = begin;
            DateEnd = end;
        }

        public override void GenerateConclusion()
        {
            StringBuilder  stringBuilder = new StringBuilder("");
            stringBuilder.Append("U ovom vremenskom periodu, lekar ").Append(SelectedDoctor.ToString()).Append(" ima zakazano ").Append
                (examinations.ToString()).Append(" pregleda, ").Append(operations.ToString()).Append(" operacija, ").Append(urgentExaminations.ToString())
                .Append(" hitnih").Append(" pregleda");
            Graphics.DrawString(stringBuilder.ToString(), Font, PdfBrushes.Black, new PointF(30, 160));

            stringBuilder = new StringBuilder("");
            stringBuilder.Append(" i ").Append(urgentOperations.ToString()).Append(" hitnih operacija.");
            Graphics.DrawString(stringBuilder.ToString(), Font, PdfBrushes.Black, new PointF(0, 168));
        }

        public override void GenerateTableContent()
        {
            Table.Columns.Add("Datum i vreme");
            Table.Columns.Add("Trajanje termina");
            Table.Columns.Add("Pacijent");
            Table.Columns.Add("Prostorija");
            Table.Columns.Add("Tip");

            Table.Rows.Add(new string[] { "Datum i vreme", "Trajanje termina (u satima)", "Pacijent", "Prostorija", "Tip" });

            foreach (Appointment appointment in Appointments)
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

                Table.Rows.Add(new string[] { appointment.DateTime.ToString("dd.MM.yyyy. HH:mm"),
                                                  appointment.DurationInHours.ToString(),
                                                  patientName,
                                                  appointment.Room.Name,
                                                  type});
            }
        }

        public override void GenerateContent()
        {
            Font = new PdfStandardFont(PdfFontFamily.Helvetica, 9);
            StringBuilder stringBuilder = new StringBuilder("");
            stringBuilder.Append("Prikaz zakazanih termina kod lekara ").Append(SelectedDoctor.ToString()).Append(" za vremenski period od ")
                .Append(DateBegin.ToString("dd.MM.yyyy.")).Append(" do ").Append(DateEnd.ToString("dd.MM.yyyy."));
            Graphics.DrawString(stringBuilder.ToString(), Font, PdfBrushes.Black, new PointF(30, 140));
        }

        public override void GenerateTitle()
        {
            Font = new PdfStandardFont(PdfFontFamily.Helvetica, 17);
            Graphics.DrawString("Izveštaj o zauzetosti", Font, PdfBrushes.Black, new PointF(170, 100));
            Graphics.DrawString("lekara pojedinca za određeni vremenski period", Font, PdfBrushes.Black, new PointF(70, 120));
        }

        public override void SavePdf()
        {
            Document.Save("../../Reports/SecretaryReport.pdf");
            Document.Close(true);
        }
    }
}
