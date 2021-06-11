using Hospital.Services;
using Syncfusion.Pdf.Graphics;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Hospital.ReportsPatterns
{
    public class MedicinesReport : ReportGenerator
    {
        public List<Medicine> Medicines { get; set; }

        public MedicinesReport()
        {
            MedicineService service = new Services.MedicineService();
            Medicines = service.GetAll();
            Medicines = Medicines.OrderBy(med => med.Name).ToList();
        }
        public override void GenerateTableContent()
        {
            Table.Columns.Add("Naziv");
            Table.Columns.Add("Doza u miligramima");
            Table.Columns.Add("Cena");
            Table.Columns.Add("Količina");

            Table.Rows.Add(new string[] { "Naziv", "Doza (u miligramima)", "Cena (RSD)", "Količina" });

            foreach (Medicine medicine in Medicines)
            {
                Table.Rows.Add(new string[] { medicine.Name, medicine.DosageInMg.ToString(), medicine.Price.ToString(), medicine.Quantity.ToString() });
            }
        }

        public override void GenerateTitle()
        {
            Font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
            Graphics.DrawString("Izveštaj o trenutnom stanju lekova", Font, PdfBrushes.Black, new PointF(120, 100));
        }

        public override void SavePdf()
        {
            Document.Save("../../Reports/MedicinesReport.pdf");
            Document.Close(true);
        }

        public override void GenerateConclusion() { }
    }
}
