using Hospital.Services;
using Hospital.ViewModels.Secretary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class HitanPregled : Page
    {
        public HitanPregled(HitanPregledViewModel hitanPregledViewModel)
        {
            InitializeComponent();
            this.DataContext = hitanPregledViewModel;
        }
    }
}
