using Hospital.Services;
using Hospital.ViewModels.Secretary;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Hospital.View.Secretary
{
    public partial class Analitika : Page
    {
        public Analitika(AnalitikaViewModel analitikaViewModel)
        {
            InitializeComponent();
            this.DataContext = analitikaViewModel;
        }
    }
}
