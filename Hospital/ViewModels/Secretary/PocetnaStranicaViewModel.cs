using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Hospital.ViewModels.Secretary
{
    public class PocetnaStranicaViewModel : ViewModel
    {
        #region Properties

        private string dateText;
        public string DateText
        {
            get { return dateText; }
            set { dateText = value; OnPropertyChanged("DateText"); }
        }

        private string timeText;
        public string TimeText
        {
            get { return timeText; }
            set { timeText = value; OnPropertyChanged("TimeText"); }
        }

        public NavigationService NavigationService { get; set; }

        #endregion

        #region Konstruktor

        public PocetnaStranicaViewModel(NavigationService navigationService)
        {
            this.NavigationService = navigationService;
            DrawChart();
            StartClock();
        }

        #endregion

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
            DateText = DateTime.Now.ToString("dd.MM.yyyy.");
            TimeText = DateTime.Now.ToString("HH:mm");
        }

        #endregion
    }
}
