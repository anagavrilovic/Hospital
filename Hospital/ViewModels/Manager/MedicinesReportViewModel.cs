using Hospital.Commands.Manager;
using Hospital.View;
using Hospital.View.Manager;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Navigation;

namespace Hospital.ViewModels.Manager
{
    public class MedicinesReportViewModel : ViewModel
    {
        #region Properties
        public List<Medicine> Medicines { get; set; }
        public Services.MedicineService MedicineService { get; set; }
        public NavigationService NavigationService { get; set; }

        #endregion

        #region Constructor
        public MedicinesReportViewModel(NavigationService navigate)
        {
            MedicineService = new Services.MedicineService();
            Medicines = MedicineService.GetAll();
            GenerateReportCommand = new RelayCommand(Execute_GenerateReportCommand, CanExecuteCommands);
            BackCommand = new RelayCommand(Execute_BackCommand, CanExecuteCommands);
            NavigationService = navigate;          
        }
        #endregion

        #region Commands
        public RelayCommand GenerateReportCommand { get; set; }
        public RelayCommand BackCommand { get; set; }


        public void Execute_GenerateReportCommand(object obj)
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
                    table.Rows.Add(new string[] { medicine.Name, medicine.DosageInMg.ToString(), medicine.Price.ToString(), medicine.Quantity.ToString()});
                }

                pdfLightTable.DataSource = table;
                pdfLightTable.Draw(page, new PointF(0, 170));

                document.Save("../../Reports/MedicinesReport.pdf");
                document.Close(true);

                MessageWindow message = new MessageWindow("Izveštaj možete pregledati u folderu Records");
                message.Show();
            }
        }

        public void Execute_BackCommand(object obj)
        {
            NavigationService.Navigate(new ManagerMainPage());
        }

        public bool CanExecuteCommands(object obj)
        {
            return true;
        }

        #endregion
    }
}
