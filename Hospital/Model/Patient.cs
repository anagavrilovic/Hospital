
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Hospital
{
    public class Patient : User, INotifyPropertyChanged
    {
        private bool isGuest = false;
        private bool isBlocked = false;

        public Patient() {}

        public bool IsGuest
        {
            get => isGuest;
            set
            {
                isGuest = value;
                OnPropertyChanged("IsGuest");
            }
        }

        public bool IsBlocked
        {
            get { return isBlocked; }
            set 
            { 
                isBlocked = value;
                OnPropertyChanged("IsBlocked");
            }
        }

        override
        public string ToString()
        {
            return FirstName + " " + LastName + " " + PersonalID;
        }
    }
}