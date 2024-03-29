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

namespace Hospital.View.Doctor
{

    public partial class Anamnesis : Page, INotifyPropertyChanged
    {
        private string anamnesisDescription;
        private static int THERAPY_TAB = 3;
        private static int DIAGNOSIS_TAB = 4;
        public event PropertyChangedEventHandler PropertyChanged;

        public string AnamnesisDescription
        {
            get
            {
                return anamnesisDescription;
            }
            set
            {
                if (value != anamnesisDescription)
                {
                    anamnesisDescription = value;
                    OnPropertyChanged("AnamnesisDescription");
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

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            ((AppointmentWindow)Window.GetWindow(this)).Examintaion.anamnesis = AnamnesisDescription;
            ChangeUI();
        }

        private void ChangeUI()
        {
            ((AppointmentWindow)Window.GetWindow(this)).AnamnesisTab.IsEnabled = false;
            ConfirmBox confirmBox = new ConfirmBox("Da li je potrebna terapija?");
            if ((bool)confirmBox.ShowDialog())
            {
                ((AppointmentWindow)Window.GetWindow(this)).tab.SelectedIndex = THERAPY_TAB;
                ((AppointmentWindow)Window.GetWindow(this)).TherapyTab.IsEnabled = true;
                ((AppointmentWindow)Window.GetWindow(this)).TherapyLabel.Foreground = Brushes.White;
            }
            else
            {
                ((AppointmentWindow)Window.GetWindow(this)).tab.SelectedIndex = DIAGNOSIS_TAB;
                ((AppointmentWindow)Window.GetWindow(this)).DiagnosisTab.IsEnabled = true;
                ((AppointmentWindow)Window.GetWindow(this)).DiagnosisLabel.Foreground = Brushes.White;
            }
        }
    }
}
