using Hospital.Services;
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

namespace Hospital.View.Secretary
{
    public partial class Analitika : Page
    {
        private DoctorService doctorService = new DoctorService();

        public Analitika()
        {
            InitializeComponent();
            this.DataContext = this;
            DrawCharts();
        }

        private void DrawCharts()
        {
            SetPieChart();
            DrawWorkTimeChart();
        }

        #region Grafikon zakazanih pregleda lekara

        public SeriesCollection DoctorsSeriesCollection { get; set; }
        public string[] DoctorsLabels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private void DrawWorkTimeChart()
        {
            DoctorsSeriesCollection = new SeriesCollection();
            DoctorsSeriesCollection.Add(new ColumnSeries { Title = "jun", Values = new ChartValues<int> { 50, 67, 42 } });
            DoctorsSeriesCollection.Add(new ColumnSeries { Title = "jul", Values = new ChartValues<int> { 66, 98, 38 } });
            DoctorsSeriesCollection.Add(new ColumnSeries { Title = "avgust", Values = new ChartValues<int> { 30, 50, 74 } });

            List<Model.Doctor> doctors = doctorService.GetAll();
            DoctorsLabels = new string[doctors.Count];
            for (int i = 0; i < doctors.Count; i++)
                DoctorsLabels[i] = doctors[i].ToString();

            Formatter = value => value.ToString("N");
        }

        #endregion

        #region PieChart
        public Func<ChartPoint, string> PointLabel { get; set; }
        public void SetPieChart()
        {
            PointLabel = chartPoint => string.Format("{0}({1:P})", chartPoint.Y, chartPoint.Participation);
        }

        #endregion

        private void GenerateReportClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Izvestaj());
        }
    }
}
