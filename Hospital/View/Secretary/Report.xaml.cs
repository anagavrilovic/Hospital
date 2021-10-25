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
using Hospital.ViewModels.Secretary;

namespace Hospital.View.Secretary
{
    public partial class Report : Page
    {
        public Report(ReportViewModel izvestajViewModel)
        {
            InitializeComponent();
            this.DataContext = izvestajViewModel;
        }
    }
}
