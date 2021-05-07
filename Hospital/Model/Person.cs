
using Hospital.Model;
using System;
using System.ComponentModel;

namespace Hospital
{
    public abstract class Person : INotifyPropertyChanged
    {
        private String firstName;
        private String lastName;
        private String personalID;
        private String phoneNumber;
        private String email;
        private DateTime dateOfBirth = DateTime.Today;
        private String cardID;
        private MaritalStatus maritalStatus;
        private Genders gender;
        private Address address = new Address();

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public string PersonalID
        {
            get => personalID;
            set
            {
                personalID = value;
                OnPropertyChanged("PersonalID");
            }
        }
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }


        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }

        public string CardID
        {
            get => cardID;
            set
            {
                cardID = value;
                OnPropertyChanged("CardID");
            }
        }
        public MaritalStatus MaritalStatus
        {
            get => maritalStatus;
            set
            {
                maritalStatus = value;
                OnPropertyChanged("MaritalStatus");
            }
        }
        public Genders Gender
        {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        public Address Address
        {
            get => address;
            set
            {
                address = value;
                OnPropertyChanged("Address");
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
    }
}