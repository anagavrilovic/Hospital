using Hospital.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

    public partial class Doctor_Examination : Window, INotifyPropertyChanged
    {
        private Examination pregled;
        private Doctor doktor;
        public Doctor Doktor
        {
            get { return doktor; }
            set
            {
                doktor = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private DoctorStorage dStorage = new DoctorStorage();
        public Examination Pregled
        {
            get { return pregled; }
            set
            {
                pregled = value;
                OnPropertyChanged();
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
        public Doctor_Examination(string id)
        {
            pregled = new Examination();
            Doktor = new Doctor();
            Doktor = dStorage.GetOne(id);
            
            InitializeComponent();
            tab.SelectedIndex = 5;
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 3 / 4);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 3 / 4);
        }

    }
    }

