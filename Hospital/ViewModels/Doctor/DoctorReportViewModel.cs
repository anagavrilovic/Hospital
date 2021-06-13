using Hospital.Commands.DoctorCommands;
using Hospital.Factory;
using Hospital.Repositories;
using Hospital.Services;
using Hospital.View.Doctor;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows;

namespace Hospital.ViewModels.Doctor
{
    public class DoctorReportViewModel :ViewModel
    {
        private MedicineService medicineService = new MedicineService(new MedicineFileRepository(), new MedicalRecordFileRepository());
        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged("StartDate");
            }
        }
        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged("EndDate");
            }
        }
        private RelayCommand reportCommand;
        public RelayCommand ReportCommand
        {
            get { return reportCommand; }
            set
            {
                reportCommand = value;
            }
        }

        public DoctorReportViewModel()
        {
            ReportCommand = new RelayCommand(Execute_Report, CanExecuteMethod);
            EndDate = DateTime.Now;
            StartDate = DateTime.Now;
            medicineService = new MedicineService(new MedicineFileRepository(), new MedicalRecordFileRepository());
        }
        private void Execute_Report(object obj)
        {
            Validate();
            List<Medicine> medicines = medicineService.GetConsumedMedicineInPeriod(StartDate, EndDate);
            using (PdfDocument document = new PdfDocument())
            {
                PdfPage page = document.Pages.Add();

                PdfGraphics graphics = page.Graphics;
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 7);

                PdfImage image = PdfImage.FromFile("../../Icon/Secretary/logo.png");
                RectangleF bounds = new RectangleF(170, 12, 150, 50);
                page.Graphics.DrawImage(image, bounds);

                font = new PdfStandardFont(PdfFontFamily.Helvetica, 17);
                graphics.DrawString("Izveštaj o potrošnji", font, PdfBrushes.Black, new PointF(170, 100));
                graphics.DrawString("lekova u određenom vremenskom periodu", font, PdfBrushes.Black, new PointF(70, 120));


                font = new PdfStandardFont(PdfFontFamily.Helvetica, 9);
                StringBuilder stringBuilder = new StringBuilder("");
                stringBuilder.Append("Prikaz potrošnje lekova u periodu od ")
                    .Append(StartDate.ToString("dd.MM.yyyy.")).Append(" do ").Append(EndDate.ToString("dd.MM.yyyy."));
                graphics.DrawString(stringBuilder.ToString(), font, PdfBrushes.Black, new PointF(30, 180));

                PdfLightTable pdfLightTable = new PdfLightTable();
                DataTable table = new DataTable();
                table.Columns.Add("Ime leka");
                table.Columns.Add("ID leka");
                table.Columns.Add("Doza u miligramima");
                table.Columns.Add("Količina");

                table.Rows.Add(new string[] { "Ime leka", "ID leka", "Doza u miligramima", "Količina"});
                foreach(Medicine medicine in medicineService.GetAll())
                {
                    int timesMedicineWasUsed = 0;
                    foreach(Medicine examinationMedicine in medicines)
                    {
                        if (examinationMedicine != null)
                        {
                            if (examinationMedicine.ID.Equals(medicine.ID))
                                timesMedicineWasUsed++;
                        }
                    }
                    if (timesMedicineWasUsed > 0)
                    {
                        table.Rows.Add(new string[] {medicine.Name,
                                                  medicine.ID,
                                                  medicine.DosageInMg.ToString(),
                                                  timesMedicineWasUsed.ToString()
                                                  });
                    }
                }
                pdfLightTable.DataSource = table;
                pdfLightTable.Draw(page, new PointF(0, 240));
                document.Save("../../Reports/DoctorReport.pdf");
                document.Close(true);
            }
            ErrorBox errorBox = new ErrorBox("Izveštaj uspešno kreiran.");
         }

        private void Validate()
        {
            if (EndDate < StartDate)
            {
                ErrorBox error = new ErrorBox("Neispravan izbor datuma");
                return;
            }
        }

        private bool CanExecuteMethod(object parameter)
        {
            return true;
        }

    }
}
