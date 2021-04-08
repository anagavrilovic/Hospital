
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hospital
{
    public class Patient : User
    {
        private Boolean isGuest;

        public Patient() {}

        public Boolean IsGuest
        {
            get => isGuest;
            set
            {
                isGuest = value;
            }
        }
        public override string ToString()
        {
            return FirstName + " "+LastName + " " + PersonalID;
        }
    }
    class Pretraga : ObservableCollection<Patient> { }
}