using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Hospital.View.Secretary
{
    public partial class PocetnaStranica : Page
    {
        public PocetnaStranica()
        {
            InitializeComponent();
            this.DataContext = this;
            DrawChart();
            StartClock();
        }

        #region Grafikon
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private void DrawChart()
        {
            SeriesCollection = new SeriesCollection();
            SeriesCollection.Add(new LineSeries { Title = "Novi Sad", Values = new ChartValues<int> { 12, 7, 9, 11, 6, 8, 7 } });
            SeriesCollection.Add(new LineSeries { Title = "Beograd", Values = new ChartValues<int> { 10, 5, 6, 9, 4, 7, 6 } });
            SeriesCollection.Add(new LineSeries { Title = "Sarajevo", Values = new ChartValues<int> { 8, 7, 9, 2, 6, 5, 5 } });
            SeriesCollection.Add(new LineSeries { Title = "Zagreb", Values = new ChartValues<int> { 7, 9, 8, 3, 3, 6, 3 } });

            Labels = new[] { "opšta praksa", "ortopedija", "ginekologija", "pedijatrija", "kardiologija", "dermatologija", "psihijatrija" };
            Formatter = value => value.ToString("N");
        }

        #endregion

        #region Datum i vreme

        private void StartClock()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += tickevent;
            timer.Start();
        }

        private void tickevent(object sender, EventArgs e)
        {
            DateShow.Text = DateTime.Now.ToString("dd.MM.yyyy.");
            TimeShow.Text = DateTime.Now.ToString("HH:mm");
        }

        #endregion
    }
}
