﻿using System;
using System.Collections.Generic;
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

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for KartonDoktorStranica.xaml
    /// </summary>
    public partial class KartonDoktorStranica : Page, INotifyPropertyChanged
    {

        private MedicalRecord karton;
        private MedicalRecordStorage mStorage;
        public MedicalRecord Karton
        {
            get { return karton; }
            set
            {
                karton = value;
            }
        }

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public KartonDoktorStranica(string id)
        {
            InitializeComponent();
            this.DataContext = this;
            mStorage = new MedicalRecordStorage();
            Karton = mStorage.GetOne(id);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void sacuvaj_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
