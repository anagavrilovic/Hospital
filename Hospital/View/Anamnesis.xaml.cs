﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

    public partial class Anamnesis : Page, INotifyPropertyChanged
    {
        private string _test1;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Test1
        {
            get
            {
                return _test1;
            }
            set
            {
                if (value != _test1)
                {
                    _test1 = value;
                    OnPropertyChanged("Test1");
                }
            }
        }
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public Anamnesis()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Sacuvaj(object sender, System.Windows.RoutedEventArgs e)
        {
            BindingExpression binding = anamnesisBox.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
            ((Doctor_Examination)Window.GetWindow(this)).Pregled.anamnesis = Test1;
        }

        private void Odustani(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}