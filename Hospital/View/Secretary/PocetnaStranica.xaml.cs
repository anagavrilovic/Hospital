using Hospital.ViewModels.Secretary;
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
        public PocetnaStranica(PocetnaStranicaViewModel pocetnaStranicaViewModel)
        {
            InitializeComponent();
            this.DataContext = pocetnaStranicaViewModel;
        }
    }
}
