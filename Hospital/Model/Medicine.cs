// File:    Medicine.cs
// Author:  Ana Gavrilovic
// Created: Monday, April 5, 2021 6:57:36 PM
// Purpose: Definition of Class Medicine

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Hospital
{
    public class Medicine : INotifyPropertyChanged
    {
       private string id;
        public string ID
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }
        private int durationInDays;
        public int DurationInDays
        {
            get => durationInDays;
            set
            {
                durationInDays = value;
                OnPropertyChanged("durationInDays");
            }
        }
        private int timesPerDay;
        public int TimesPerDay
        {
            get => timesPerDay;
            set
            {
                timesPerDay = value;
                OnPropertyChanged("timesPerDay");
            }
        }
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        private double dosageInMg;
        public double DosageInMg
        {
            get => dosageInMg;
            set
            {
                dosageInMg = value;
                OnPropertyChanged("DosageInMg");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public override string ToString()
        {
            return Name + " " + ID;
        }
    }
    class PretragaM : ObservableCollection<Medicine> { }
}

