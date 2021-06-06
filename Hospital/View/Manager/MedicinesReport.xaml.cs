using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Hospital.View.Manager
{
    public partial class MedicinesReport : Page
    {
        public List<Medicine> Medicines { get; set; }

        public MedicinesReport()
        {
            InitializeComponent();
            this.DataContext = this;
            Services.MedicineService medicineService = new Services.MedicineService();
            Medicines = medicineService.GetAll();
            datum.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }

        private void GenerateReport(object sender, RoutedEventArgs e)
        {
            using (PdfDocument document = new PdfDocument())
            {
                Medicines = Medicines.OrderBy(med => med.Name).ToList();

                PdfPage page = document.Pages.Add();

                PdfGraphics graphics = page.Graphics;
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 9);

                graphics.DrawString("Adresa: Hajduk Veljkova 5", font, PdfBrushes.Black, new PointF(0, 19));
                graphics.DrawString("21000 Novi Sad, Srbija", font, PdfBrushes.Black, new PointF(12, 27));
                graphics.DrawString("Kontakt telefon: 021568136", font, PdfBrushes.Black, new PointF(0, 36));
                graphics.DrawString("E-mail adresa: nszdravo@gmail.com", font, PdfBrushes.Black, new PointF(0, 44));

                PdfImage image = PdfImage.FromFile("../../Icon/logo.png");
                RectangleF bounds = new RectangleF(170, 12, 150, 50);
                page.Graphics.DrawImage(image, bounds);

                font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
                graphics.DrawString("Izveštaj o trenutnom stanju lekova", font, PdfBrushes.Black, new PointF(120, 100));

                PdfLightTable pdfLightTable = new PdfLightTable();
                DataTable table = new DataTable();

                table.Columns.Add("Naziv");
                table.Columns.Add("Doza u miligramima");
                table.Columns.Add("Cena");
                table.Columns.Add("Količina");

                table.Rows.Add(new string[] { "Naziv", "Doza (u miligramima)", "Cena (RSD)", "Količina" });
            
                foreach (Medicine medicine in Medicines)
                {              
                    table.Rows.Add(new string[] { medicine.Name,
                                                  medicine.DosageInMg.ToString(),
                                                  medicine.Price.ToString(),
                                                  medicine.Quantity.ToString()});
                }

                pdfLightTable.DataSource = table;
                pdfLightTable.Draw(page, new PointF(0, 170));
            
                document.Save("../../Reports/MedicinesReport.pdf");
                document.Close(true);

                MessageWindow message = new MessageWindow("Izveštaj možete pregledati u folderu Records");
                message.Show();
            }
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerMainPage());
        }
    }
}
