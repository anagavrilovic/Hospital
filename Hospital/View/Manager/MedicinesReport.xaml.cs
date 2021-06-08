using Hospital.ViewModels.Manager;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Hospital.View.Manager
{
    public partial class MedicinesReport : Page
    {
        public List<Medicine> Medicines { get; set; }

        public MedicinesReport(MedicinesReportViewModel medicinesReportViewModel)
        {
            InitializeComponent();
            datum.Text = DateTime.Now.ToString("dd.MM.yyyy");
            this.DataContext = medicinesReportViewModel;
        }
    }
}
