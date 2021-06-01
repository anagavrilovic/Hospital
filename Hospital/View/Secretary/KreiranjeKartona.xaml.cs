using Hospital.Model;
using Hospital.Services;
using Hospital.ViewModels.Secretary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Shapes;

namespace Hospital.View
{
    public partial class KreiranjeKartona : Page
    {
        public KreiranjeKartona(KreiranjeKartonaViewModel kreiranjeKartonaViewModel)
        {
            InitializeComponent();
            this.DataContext = kreiranjeKartonaViewModel;
        }
    }
}
