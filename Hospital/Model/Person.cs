
using Hospital.Model;
using System;
using System.ComponentModel;

namespace Hospital
{
    public abstract class Person 
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
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
            }
        }
        public string PersonalID
        {
            get => personalID;
            set
            {
                personalID = value;
            }
        }
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
            }
        }
        public string Email
        {
            get => email;
            set
            {
                email = value;
            }
        }


        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                dateOfBirth = value;
            }
        }

        public string CardID
        {
            get => cardID;
            set
            {
                cardID = value;
            }
        }
        public MaritalStatus MaritalStatus
        {
            get => maritalStatus;
            set
            {
                maritalStatus = value;
            }
        }
        public Genders Gender
        {
            get => gender;
            set
            {
                gender = value;
            }
        }

        public Address Address
        {
            get => address;
            set
            {
                address = value;
            }
        }
    }
}