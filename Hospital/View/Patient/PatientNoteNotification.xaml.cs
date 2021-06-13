﻿using Hospital.Model;
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

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for PatientNoteNotification.xaml
    /// </summary>
    public partial class PatientNoteNotification : Page
    {
        public PatientNoteNotification(PatientNotesNotification patientNotesNotification)
        {
            InitializeComponent();
            BackButton.Focus();
            SubjectLabel.Content = patientNotesNotification.Name;
            TextBlock.Text = patientNotesNotification.Text;
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PatientNotifications());
        }
    }
}
