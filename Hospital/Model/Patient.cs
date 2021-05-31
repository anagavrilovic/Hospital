
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Hospital
{
    public class Patient : User
    {
        private bool isGuest = false;
        private bool isBlocked = false;

        public Patient() {}

        public bool IsGuest { get; set; }

        public bool IsBlocked { get; set; }

        override
        public string ToString()
        {
            return FirstName + " " + LastName + " " + PersonalID;
        }
    }
}