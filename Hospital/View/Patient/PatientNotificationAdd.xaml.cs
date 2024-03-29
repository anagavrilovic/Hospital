﻿using Hospital.Factory;
using Hospital.Model;
using Hospital.Services;
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
    /// Interaction logic for PatientNotificationAdd.xaml
    /// </summary>
    public partial class PatientNotificationAdd : Page
    {
        private PatientNotesNotificationService patientNotesNotificationService = new PatientNotesNotificationService(MainWindow.IDnumber);
        private Boolean[] days = new Boolean[7];
        private PatientNote patientNote;
        public PatientNotificationAdd(PatientNote patientNote)
        {
            InitializeComponent();
            this.patientNote = patientNote;
            SetDaysList();
            CheckBoxMonday.Focus();
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void AddNotification(object sender, RoutedEventArgs e)
        {
            FindPickedDays();
            TimeSpan ts = TimeSpan.Parse(ComboBoxHours.Text + ":" + ComboBoxMinutes.Text + ":00");
            PatientNotesNotification patientNotesNotification = new PatientNotesNotification(patientNote.Subject, patientNote.Text, patientNotesNotificationService.GetNewID(), MainWindow.IDnumber, days, ts);
            patientNotesNotificationService.Save(patientNotesNotification);
            this.NavigationService.Navigate(new PatientNotifications());
        }

        private void SetDaysList()
        {
            for (int i = 0; i < 7; i++)
            {
                days[i] = false;
            }
        }
        private void FindPickedDays()
        {
            if ((bool)CheckBoxMonday.IsChecked)
            {
                days[0] = true;
            }
            if ((bool)CheckBoxTuesday.IsChecked)
            {
                days[1] = true;
            }
            if ((bool)CheckBoxWednesday.IsChecked)
            {
                days[2] = true;
            }
            if ((bool)CheckBoxThursday.IsChecked)
            {
                days[3] = true;
            }
            if ((bool)CheckBoxFriday.IsChecked)
            {
                days[4] = true;
            }
            if ((bool)CheckBoxSaturday.IsChecked)
            {
                days[5] = true;
            }
            if ((bool)CheckBoxSunday.IsChecked)
            {
                days[6] = true;
            }
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Space) && ComboBoxHours.IsFocused)
            {
                Keyboard.ClearFocus();
                ComboBoxMinutes.Focus();
            }


            else if ((e.Key == Key.Space) && ComboBoxMinutes.IsFocused)
            {
                Keyboard.ClearFocus();
                AddButton.Focus();
            }
        }
    }
}
